using Infrastructure.Model;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PAPS.Model;
using Resources.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PAPS.Services
{
    /// <summary>
    /// 分队勤务分组安排表帮助类
    /// </summary>
    public static class DutyGroupScheduleHelper
    {

        /// <summary>
        /// 根据安排表生成实地查勤
        /// </summary>
        /// <param name="dgs"></param>
        public static bool GetAbsoultedDutyCheckLog(DutyGroupSchedule dgs)
        {
            if (dgs == null)
                return false;
            bool isOK = false;
            //补全数据
            DutyGroupSchedule model = CompletionData(dgs);

            //组装网络查勤员数据
            Queue<Staff> checkMans = new Queue<Staff>(); //网络查勤员队列
            foreach (DutyGroupScheduleDetail detail in model.DutyGroupScheduleDetails)
            {
                checkMans.Enqueue(detail.CheckMan);
            }

            //取所有哨位信息
            Dictionary<Guid, Queue<DutyCheckSiteSchedule>> groupStaff = new Dictionary<Guid, Queue<DutyCheckSiteSchedule>>();
            Dictionary<Guid, Queue<DutyCheckSiteSchedule>> allStaffs = new Dictionary<Guid, Queue<DutyCheckSiteSchedule>>();

            //
            foreach (DutyCheckSiteSchedule dss in model.DutyGroupScheduleDetails[0].CheckDutySiteSchedule)
            {
                //groupStaff.Add(dss.CheckDutySite.MonitorySiteId, new Queue<DutyCheckSiteSchedule>());
                //allStaffs.Add(dss.CheckDutySite.MonitorySiteId, new Queue<DutyCheckSiteSchedule>());
                if (dss.CheckDutySiteId != null)
                {
                    groupStaff.Add(dss.CheckDutySiteId.Value, new Queue<DutyCheckSiteSchedule>());
                    allStaffs.Add(dss.CheckDutySiteId.Value, new Queue<DutyCheckSiteSchedule>());
                }
            }

            //取哨位对应的组员信息
            foreach (DutyGroupScheduleDetail detail in model.DutyGroupScheduleDetails)
            {
                foreach (DutyCheckSiteSchedule dss in detail.CheckDutySiteSchedule)
                {
                    if (dss.CheckDutySiteId != null)
                    {
                        if (groupStaff.ContainsKey(dss.CheckDutySiteId.Value))
                        {
                            groupStaff[dss.CheckDutySiteId.Value].Enqueue(dss);
                        }

                        if (allStaffs.ContainsKey(dss.CheckDutySiteId.Value))
                        {
                            allStaffs[dss.CheckDutySiteId.Value].Enqueue(dss);
                        }
                    }
                }
            }
            //过滤过期的时间
            if (model.EndDate <= DateTime.Now)
                return false;
            //计算有效天数
            int days = (int)(model.EndDate.Date - model.StartDate.Date).TotalDays + 1;
            if (days <= 0)
            {
                Console.WriteLine("生成分组查勤表无效，时间已过期！");
                return false;
            }
            DateTime validtime = DateTime.Now;
            if (model.StartDate <= DateTime.Now && model.EndDate >= DateTime.Now)
            {
                for (int i = 0; i < days; i++)
                {
                    if (model.StartDate.AddDays(i) >= DateTime.Now.Date)
                    {
                        validtime = model.StartDate.AddDays(i);
                        break;
                    }
                }
                validtime = new DateTime(validtime.Year, validtime.Month, validtime.Day, 0, 0, 0);
            }
            else if (model.StartDate > DateTime.Now)
            {
                validtime = new DateTime(model.StartDate.Year, model.StartDate.Month, model.StartDate.Day, 0, 0, 0);
            }

            days = (int)(model.EndDate.Date - validtime.Date).TotalDays + 1;

            if (days == 0)
                return false;
            //实际人员轮值总数
            int allCount = days * model.Schedule.ScheduleCycle.DayPeriods[0].TimePeriods.Count() - 1;
            //计算出实际人员的循环情况

            for (int i = 0; i < allCount; i++)
            {
                foreach (var data in groupStaff)
                {
                    DutyCheckSiteSchedule staff = groupStaff[data.Key].Dequeue();
                    //循环进队
                    allStaffs[data.Key].Enqueue(staff);
                    //重新进队
                    groupStaff[data.Key].Enqueue(staff);
                }
            }

            //根据时段循环分配组员
            List<DutyCheckLog> logs = new List<DutyCheckLog>();
            for (int i = 0; i < days; i++)
            {
                try
                {
                    DateTime time = validtime.AddDays(i);
                    foreach (TimePeriod tp in model.Schedule.ScheduleCycle.DayPeriods[0].TimePeriods)
                    {
                        //判断当天的数据，已经失效的时段不再生成记录
                        if (time == DateTime.Now.Date)
                        {
                            DateTime startTime = new DateTime(time.Year, time.Month, time.Day, tp.StartTime.Hour, tp.StartTime.Minute, tp.StartTime.Second);
                            DateTime endTime = new DateTime(time.Year, time.Month, time.Day, tp.EndTime.Hour, tp.EndTime.Minute, tp.EndTime.Second);

                            if (startTime > DateTime.Now || endTime < DateTime.Now)
                            {
                                continue;
                            }
                        }
                        //组装网络查勤员数据
                        Staff staff = checkMans.Dequeue();
                        DutyCheckLog log = GetCheckManLog(model.OrganizationId, time, tp, staff.StaffId);

                        logs.Add(log);
                        checkMans.Enqueue(staff);

                        foreach (var data in allStaffs)
                        {
                            DutyCheckSiteSchedule dss = allStaffs[data.Key].Dequeue();
                            DutyCheckLog sitelog = GetSiteLog(model, time, tp, dss, model.Schedule.ScheduleCycle.DayPeriods[0].DayPeriodId);

                            logs.Add(sitelog);
                        }
                    }
                }
                catch (Exception ex)
                {
                    isOK = false;
                    return isOK;
                }
            }

            //实际保存记录
            using (var db = new AllInOneContext.AllInOneContext())
            {
                //建立事务
                using (var tran = db.Database.BeginTransaction())
                {

                    try
                    {
                        //先移除已排班但尚未执行的记录
                        var del = db.DutyCheckLog
                            .Where(p => p.PlanDate >= model.StartDate
                            && p.PlanDate <= model.EndDate
                            && p.StatusId == new Guid("361ADFE9-E58A-4C88-B191-B742CC212443"));

                        if (del != null && del.Count() != 0)
                        {
                            db.DutyCheckLog.RemoveRange(del.ToList());
                            db.SaveChanges();
                        }

                        //新增
                        foreach (DutyCheckLog log in logs)
                        {
                            string txt = JsonConvert.SerializeObject(log);
                            db.DutyCheckLog.Add(log);
                        }

                        db.SaveChanges();

                        tran.Commit();
                        isOK = true;
                    }
                    catch (Exception ex)
                    {
                        isOK = false;
                    }
                }
            }
            return isOK;
        }


        /// <summary>
        /// 补全数据
        /// </summary>
        /// <param name="dgs"></param>
        /// <returns></returns>
        public static DutyGroupSchedule CompletionData(DutyGroupSchedule dgs)
        {
            DutyGroupSchedule model;
            using (var db = new AllInOneContext.AllInOneContext())
            {
                model = db.DutyGroupSchedule
                        .Include(t => t.Lister)
                        .Include(t => t.Organization)
                        .Include(t => t.DutyGroupScheduleDetails).ThenInclude(t => t.CheckMan)
                        .Include(t => t.DutyGroupScheduleDetails).ThenInclude(t => t.CheckDutySiteSchedule).ThenInclude(t => t.CheckMan)
                        .Include(t => t.DutyGroupScheduleDetails).ThenInclude(t => t.CheckDutySiteSchedule).ThenInclude(t => t.SiteOrganization)
                        .Include(t => t.DutyGroupScheduleDetails).ThenInclude(t => t.CheckDutySiteSchedule)/*.ThenInclude(t => t.CheckDutySite)*/
                        .Include(t=>t.Schedule).ThenInclude(t=>t.ScheduleCycle).ThenInclude(t=>t.DayPeriods).ThenInclude(t=>t.TimePeriods)
                        .FirstOrDefault(p => p.DutyGroupScheduleId.Equals(dgs.DutyGroupScheduleId));
            }

            return model;
        }

        /// <summary>
        /// 获取查勤点日志
        /// </summary>
        /// <param name="model">计划实体</param>
        /// <param name="time">计划时间</param>
        /// <param name="tp">时间段</param>
        /// <param name="dss">检查点人员安排明细</param>
        /// <returns></returns>
        private static DutyCheckLog GetSiteLog(DutyGroupSchedule model, DateTime time, TimePeriod tp, DutyCheckSiteSchedule dss,Guid dayPeriod)
        {
            return new DutyCheckLog
            {
                DutyCheckLogId = Guid.NewGuid(),
                TimePeriod = tp,
                DutyCheckSiteScheduleId= dss.DutyCheckSiteScheduleId,
                PlanDate = time,
                DutyCheckStaffId= dss.CheckManId,
                OrganizationId = model.OrganizationId,
                StatusId = new Guid("361ADFE9-E58A-4C88-B191-B742CC212443"), //未开始
                RecordTypeId= new Guid("a0002016-e009-b019-e001-abcd18000002"),  //实地查勤
                DayPeriodId= dayPeriod,
            };
        }

        /// <summary>
        /// 获取网络查勤员数据
        /// </summary>
        /// <param name="model">计划实体</param>
        /// <param name="time">计划时间</param>
        /// <param name="tp">时间段</param>
        /// <param name="staff">人员</param>
        /// <returns></returns>
        private static DutyCheckLog GetCheckManLog(Guid organizationId, DateTime time, TimePeriod tp, Guid staffId)
        {
            DutyCheckLog log = new DutyCheckLog
            {
                DutyCheckLogId = Guid.NewGuid(),
                TimePeriod = tp,
                RecordTypeId = new Guid("a0002016-e009-b019-e001-abcd18000002"),  //实地查勤 new Guid("359A58FA-0BAB-45A3-ACAF-98EB73228B95"), //网络查勤
                PlanDate = time,
                DutyCheckStaffId= staffId,
                OrganizationId=organizationId,
                StatusId= new Guid("361ADFE9-E58A-4C88-B191-B742CC212443"), //未开始

            };
            return log;
        }


        /// <summary>
        /// 根据组织机构获取查勤点
        /// </summary>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        public static MonitorySite GetMonitorySiteByOrganizationId(Guid organizationId, AllInOneContext.AllInOneContext db)
        {
            MonitorySite monitorySite= db.MonitorySite
                             .FirstOrDefault(p => p.OrganizationId.Equals(organizationId)
                             && p.IsDutycheckSite == true);
            return monitorySite;
        }


        public static bool IsExist(DateTime startTime,DateTime endTime,Guid organizationId)
        {
            bool isExist = false;
            using (var db = new AllInOneContext.AllInOneContext())
            {
                //var data = from p in db.DutyCheckLog
                //    .Include(t => t.Apprises).ThenInclude(t => t.DutyCheckAppraise).ThenInclude(t => t.AppraiseICO)
                //    .Include(t => t.Apprises).ThenInclude(t => t.DutyCheckAppraise).ThenInclude(t => t.AppraiseType)
                //    .Include(t => t.DutyCheckOperation).ThenInclude(t => t.Attachments).ThenInclude(t => t.Attachment)
                //    .Include(t => t.DutyCheckOperation).ThenInclude(t => t.Attachments).ThenInclude(t => t.AttachmentType)
                //    .Include(t => t.DutyCheckSiteSchedule).ThenInclude(t => t.CheckDutySite)
                //    .Include(t => t.DutyCheckSiteSchedule).ThenInclude(t => t.SiteOrganization)
                //    .Include(t => t.DutyCheckSiteSchedule).ThenInclude(t => t.CheckMan)
                //    .Include(t => t.DutyCheckStaff)
                //    .Include(t => t.Organization)
                //    .Include(t => t.RecordType)
                //    .Include(t => t.Status)

                //           where p.PlanDate>=startTime && p.PlanDate<=endTime
                //           && p.OrganizationId.Equals(organizationId)
                //           select p;

                var data = from p in db.DutyGroupSchedule
                           where p.StartDate <= startTime.Date && p.EndDate >= endTime.Date
                                  && p.OrganizationId.Equals(organizationId)
                                  && !p.IsCancel
                                  select p;

                if (data.Count() > 0)
                {
                    isExist = true;
                }
            }

            return isExist;
        }


        /// <summary>
        /// 当前勤务编组是否已在执行
        /// </summary>
        /// <returns></returns>
        public static int IsInWork(DateTime startTime, DateTime endTime, Guid organizationId)
        {
            int count = 0;
            using (var db = new AllInOneContext.AllInOneContext())
            {
                var dowork = db.DutyCheckLog
                            .Where(p => p.PlanDate >= startTime.Date
                            && p.PlanDate <= endTime.Date
                            && p.StatusId == new Guid("124A8562-EAC8-4C09-8758-A6E312974552"));
                if(dowork!=null && dowork.Count()>0)
                {
                    count = dowork.Count();
                }
            }

            return count;
        }

    }


    


    
}
