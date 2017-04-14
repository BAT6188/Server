using AllInOneContext;
using Infrastructure.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PAPS.Model;
using Resources.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PAPS.Services
{
    /// <summary>
    /// 查勤计划帮助类
    /// </summary>
    public static class DutyCheckPackageHelper
    {

        /// <summary>
        /// 根据组织机构ID获取查勤包计划
        /// </summary>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        public static DutyCheckPackageTimePlan GetDutyCheckPackageTimePlan(Guid organizationId)
        {
            DutyCheckPackageTimePlan plan = null;

            using (var db = new AllInOneContext.AllInOneContext())
            {
                plan = db.DutyCheckPackageTimePlan
                    .Include(t => t.Schedule).ThenInclude(t => t.ScheduleCycle).ThenInclude(t => t.DayPeriods).ThenInclude(t => t.TimePeriods)
                    .Include(t=>t.Schedule).ThenInclude(t => t.ScheduleCycle).ThenInclude(t=>t.CycleType)
                    .FirstOrDefault(p => p.Organization.OrganizationId.Equals(organizationId));
            }
            return plan;
        }

        ///// <summary>
        ///// 根据组织机构ID获取本机及直属下级的监控点
        ///// </summary>
        ///// <param name="organizationId"></param>
        ///// <returns></returns>
        //public static List<MonitorySite> GetAllMonitorySite(Guid organizationId)
        //{
        //    List<MonitorySite> list = new List<MonitorySite>();


        //    using (var db = new AllInOneContext.AllInOneContext())
        //    {
        //        Organization org = db.Organization
        //            .Include(t => t.Center)
        //            .FirstOrDefault(p => p.OrganizationId.Equals(organizationId));
        //        if (org == null || org.Center == null || org.Center.EndPoints == null || org.Center.EndPoints.Count == 0)
        //            return list;
        //        var orgs = db.Organization
        //            .Include(t => t.Center)
        //            .Where(p => p.OrganizationTypeId != null && p.OrganizationFullName.Contains(org.OrganizationFullName));
        //        if (orgs.ToList().Count() == 0)
        //            return list;
        //        foreach (Organization Query in orgs.ToList())
        //        {
        //            string url = string.Format("http://{0}:{1}/Resources/MonitorySite/organizationId={2}", org.Center.EndPoints[0].IPAddress, org.Center.EndPoints[0].Port, Query.OrganizationId);

        //            var handler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip };
        //            using (var http = new HttpClient(handler))
        //            {
        //                try
        //                {
        //                    http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //                    var rst = http.GetAsync(url).Result.Content.ReadAsStringAsync().Result;
        //                    var obj = JsonConvert.DeserializeObject<List<MonitorySite>>(rst);
        //                    if (obj.ToList().Count == 0)
        //                        continue;
        //                    //过滤查勤监控点
        //                    var data = obj.ToList().Where(p => p.IsDutycheckSite == true);
        //                    if (data.ToList().Count == 0)
        //                        continue;
        //                    list.AddRange(data.ToList());
        //                }
        //                catch (Exception ex)
        //                {
        //                    Console.WriteLine(ex.Message);
        //                }

        //            }
        //        }
        //    }

        //    return list;
        //}

        /// <summary>
        /// 根据组织机构ID获取本机及直属下级的监控点的总数
        /// </summary>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        public static int GetAllMonitorySite(Guid organizationId)
        {
            int allCount = 0;
            using (var db = new AllInOneContext.AllInOneContext())
            {
                Organization org = db.Organization
                    .FirstOrDefault(p => p.OrganizationId.Equals(organizationId));
                if (org == null)
                    return allCount;
                var orgs = db.Organization
                    .Where(p => p.OrganizationTypeId == null && p.OrganizationFullName.Contains(org.OrganizationFullName));
                if (orgs.ToList().Count() == 0)
                    return allCount;
                List<Organization> list = orgs.ToList();
                foreach (Organization Query in orgs.ToList())
                {
                    allCount = allCount + Query.DutycheckPoints;
                }
            }

            return allCount;
        }


        /// <summary>
        /// 获取随机抽查点列表
        /// </summary>
        /// <param name="randomRate">随机抽查率</param>
        /// <param name="all">所有监控点</param>
        /// <returns></returns>
        public static List<MonitorySite> GetRandomMonitorySite(double randomRate, List<MonitorySite> all)
        {
            List<MonitorySite> randoms = new List<MonitorySite>();
            int randomCount= (int)(randomRate * all.Count / 100.0D + 0.5D);
            Random r = new Random();
            for (int i = 0; i < randomCount; i++)
            {
                randoms.Add(all[r.Next(all.Count)]);
            }
            return randoms;
        }

        /// <summary>
        /// 划分查勤包
        /// </summary>
        public static void AllocationDutychekPackage(Guid organizationId, DateTime packStartTime)
        {
            #region Bak
            //DutyCheckPackageTimePlan plan = GetDutyCheckPackageTimePlan(organizationId);
            //List<MonitorySite> allMonitorySites = GetAllMonitorySite(organizationId);
            //if (plan == null || allMonitorySites.Count==0)
            //    return;
            //if (plan.RandomRate != 0)
            //{
            //    allMonitorySites.AddRange(GetRandomMonitorySite(plan.RandomRate, allMonitorySites));
            //}
            //int monitorPointNum = allMonitorySites.Count;
            //int scheduleDay = 1;
            //switch (plan.Schedule.ScheduleCycle.CycleType.SystemOptionCode)
            //{
            //    case "13700001": //每天
            //        scheduleDay = 1;
            //        break;
            //    case "13700002": //每周
            //        scheduleDay = 7;
            //        break;
            //}
            //int fixMonitorPointSize =GetAbsoultedPackageSize(plan.Schedule.ScheduleCycle.DayPeriods[0].TimePeriods);
            //int onedayCount = monitorPointNum / scheduleDay;
            //DateTime endTime = new DateTime(startTime.AddDays(scheduleDay).Year, startTime.AddDays(scheduleDay).Month, startTime.AddDays(scheduleDay).Day, 23, 59, 59);

            //List<DutyCheckPackage> DutyCheckPackages = new List<DutyCheckPackage>();
            //for (int i = 0; i < scheduleDay; i++)
            //{
            //    startTime = startTime.AddDays(i);
            //    startTime = new DateTime(startTime.Year, startTime.Month, startTime.Day, 0, 0, 0); 

            //    foreach (TimePeriod tp in plan.Schedule.ScheduleCycle.DayPeriods[0].TimePeriods)
            //    {

            //        //组装查勤包
            //        DutyCheckPackage dDutyCheckPackage = new DutyCheckPackage();
            //        dDutyCheckPackage.DutyCheckPackageId = Guid.NewGuid();
            //        dDutyCheckPackage.StartTime = new DateTime(startTime.Year, startTime.Month,startTime.Day,tp.StartTime.Hour,tp.StartTime.Minute,tp.StartTime.Second);
            //        dDutyCheckPackage.EndTime = new DateTime(startTime.Year, startTime.Month, startTime.Day, tp.EndTime.Hour, tp.EndTime.Minute, tp.EndTime.Second);
            //        dDutyCheckPackage.OrganizationId = plan.OrganizationId;

            //        List<DutyCheckPackageLog> logs = new List<DutyCheckPackageLog>();


            //        int packageSize = 0;
            //        if (tp.TimePeriodExtra != null && tp.TimePeriodExtra.ValueType == 0)
            //        {
            //            packageSize = tp.TimePeriodExtra.AbsoluteValue;
            //        }
            //        else if (tp.TimePeriodExtra != null && tp.TimePeriodExtra.ValueType == 1)
            //        {
            //            packageSize = (int)(onedayCount - fixMonitorPointSize * tp.TimePeriodExtra.PercentValue / 100.0D);
            //        }
            //        else
            //        {
            //            packageSize = onedayCount;
            //        }


            //        Random r = new Random();
            //        for (int k = 0; k < packageSize; k++)
            //        {
            //            if (monitorPointNum == 0)
            //            {
            //                break;
            //            }

            //            int index = r.Next(monitorPointNum);
            //            MonitorySite monitorPoint = allMonitorySites[index];

            //            allMonitorySites.RemoveAt(index);
            //            monitorPointNum--;

            //            //DutyCheckLog log = new DutyCheckLog
            //            //{
            //            //    DutyCheckLogId = Guid.NewGuid(),
            //            //    OrganizationId = plan.OrganizationId,
            //            //    RecordTypeId = new Guid("359A58FA-0BAB-45A3-ACAF-98EB73228B95"), //网络查勤
            //            //    //Schedule=plan.Schedule,
            //            //    StatusId = new Guid("361ADFE9-E58A-4C88-B191-B742CC212443"), //未开始
            //            //};
            //            if (monitorPoint == null)
            //            {
            //                break;
            //            }
            //            DutyCheckPackageLog dutycheckpackagelog = new DutyCheckPackageLog
            //            {
            //                DutyCheckPackageId = dDutyCheckPackage.DutyCheckPackageId,
            //                DutyCheckLog = GetSiteLog(plan.OrganizationId, tp, monitorPoint, plan.Schedule.ScheduleCycle.DayPeriods[0].DayPeriodId)
            //            };
            //            logs.Add(dutycheckpackagelog);
            //        }

            //        if (logs.Count == 0)
            //        {
            //            break;
            //        }
            //        dDutyCheckPackage.DutyCheckPackLogs = logs;
            //        DutyCheckPackages.Add(dDutyCheckPackage);
            //    }
            //}


            //using (var db = new AllInOneContext.AllInOneContext())
            //{
            //    try
            //    {
            //        db.DutyCheckPackage.AddRange(DutyCheckPackages);
            //        db.SaveChanges();
            //    }
            //    catch (Exception ex)
            //    {

            //    }
            //} 
            #endregion

            //不再预先取下级查勤点，改为按组织机构生成查勤包，查勤再向下级去查勤点数据（1.基于网络风险，可能导致取不到监控点，引起异常）
            DutyCheckPackageTimePlan plan = GetDutyCheckPackageTimePlan(organizationId);
            using (var db = new AllInOneContext.AllInOneContext())
            {
                Organization org = db.Organization.FirstOrDefault(p => p.OrganizationId.Equals(organizationId));
                if (org == null)
                    return;
                 
                //获取所有组织机构
                var orgs = db.Organization.Where(t => t.DutycheckPoints > 0).ToList();
                //查勤点
                List<Guid> dutycheckSiteOrganizationIdList = new List<Guid>();
                orgs.ForEach(t =>
                {
                    for (int i = 0; i < t.DutycheckPoints; i++)
                        dutycheckSiteOrganizationIdList.Add(t.OrganizationId);
                });
                dutycheckSiteOrganizationIdList.AddRange(GetRandomOrganizationList(plan.RandomRate, dutycheckSiteOrganizationIdList));
                //根据查勤包数量：时段*周期（总队默认一周覆盖一次，支队和中队是每天都要覆盖一次）
                int scheduleDay = 0;
                int dutycheckSiteSum = dutycheckSiteOrganizationIdList.Count;

                if (!org.OrganizationFullName.Contains(".") && org.OrganizationFullName.Contains("总队")) 
                {
                    scheduleDay = plan.Schedule.ScheduleCycle.DayPeriods[0].TimePeriods.Count() * plan.Schedule.ScheduleCycle.Days.Count();
                }
                else
                {
                    scheduleDay =1;
                }
                
                //

                int perDayDutycheckSiteCount = dutycheckSiteSum / scheduleDay;
                if (perDayDutycheckSiteCount * scheduleDay < dutycheckSiteSum)
                {
                    perDayDutycheckSiteCount = perDayDutycheckSiteCount + 1;
                }
                int fixDutycheckSiteCount = GetAbsoultedPackageSize(plan.Schedule.ScheduleCycle.DayPeriods[0].TimePeriods); //固定查勤数量
                DateTime endTime = new DateTime(packStartTime.AddDays(scheduleDay).Year, packStartTime.AddDays(scheduleDay).Month, packStartTime.AddDays(scheduleDay).Day, 23, 59, 59);

                List<DutyCheckPackage> dutycheckPackageList = new List<DutyCheckPackage>();
                for (int i = 0; i < scheduleDay; i++)
                {
                    DateTime startTime = packStartTime.AddDays(i).Date;
                    foreach (TimePeriod tp in plan.Schedule.ScheduleCycle.DayPeriods[0].TimePeriods)
                    {
                        //组装查勤包
                        DutyCheckPackage dutyCheckPackage = new DutyCheckPackage();
                        dutyCheckPackage.DutyCheckPackageId = Guid.NewGuid();
                        dutyCheckPackage.StartTime = new DateTime(startTime.Year, startTime.Month, startTime.Day, tp.StartTime.Hour, tp.StartTime.Minute, tp.StartTime.Second);
                        dutyCheckPackage.EndTime = new DateTime(startTime.Year, startTime.Month, startTime.Day, tp.EndTime.Hour, tp.EndTime.Minute, tp.EndTime.Second);
                        dutyCheckPackage.OrganizationId = plan.OrganizationId;
                        List<DutyCheckPackageLog> logs = new List<DutyCheckPackageLog>();
                        int packageSize =  perDayDutycheckSiteCount;
                        if (tp.TimePeriodExtra != null && tp.TimePeriodExtra.ValueType == 0)
                            packageSize = tp.TimePeriodExtra.AbsoluteValue;
                        else if (tp.TimePeriodExtra != null && tp.TimePeriodExtra.ValueType == 1)
                            packageSize = (int)(perDayDutycheckSiteCount - fixDutycheckSiteCount * tp.TimePeriodExtra.PercentValue / 100.0D);

                        Random r = new Random();
                        for (int k = 0; k < packageSize; k++)
                        {
                            if (dutycheckSiteSum == 0)
                                break;
                            int index = r.Next(dutycheckSiteSum);
                            Guid dutycheckPointOrganizationId = dutycheckSiteOrganizationIdList[index];
                            dutycheckSiteOrganizationIdList.RemoveAt(index);
                            dutycheckSiteSum--;

                            DutyCheckPackageLog dutycheckpackagelog = new DutyCheckPackageLog
                            {
                                DutyCheckPackageId = dutyCheckPackage.DutyCheckPackageId,
                                DutyCheckLog = PreCreateDutycheckLog(plan.OrganizationId, tp, dutycheckPointOrganizationId, plan.Schedule.ScheduleCycle.DayPeriods[0].DayPeriodId)
                            };
                            logs.Add(dutycheckpackagelog);
                        }

                        if (logs.Count > 0)
                        {
                            dutyCheckPackage.DutyCheckPackLogs = logs;
                            dutyCheckPackage.PackageStatusId = new Guid("361ADFE9-E58A-4C88-B191-B742CC212443"); //未开始
                            dutycheckPackageList.Add(dutyCheckPackage);
                        }
                    }
                }
                db.DutyCheckPackage.AddRange(dutycheckPackageList);
                db.SaveChanges();
            }
        }

        private static int GetAbsoultedPackageSize(List<TimePeriod> list)
        {
            int sum = 0;
            foreach(TimePeriod tp in list)
            {
                if (tp.TimePeriodExtra != null && tp.TimePeriodExtra.ValueType == 0)
                {
                    sum += tp.TimePeriodExtra.AbsoluteValue;
                }
            }
            return sum;
        }

        /// <summary>
        /// 更新尚未完成查勤记录状态
        /// </summary>
        /// <param name="organizationId">组织机构ID</param>
        public static void DutyCheckPackageTimePlanOnChange(Guid organizationId)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    var Query = db.DutyCheckLog
                        .Include(t => t.Status)
                        .Where(p => p.OrganizationId.Equals(organizationId) && p.Status.SystemOptionCode != "16000003" && p.RecordTypeId.Equals(new Guid("a0002016-e009-b019-e001-abcd18000001")));

                    if (Query.Count() == 0)
                        return;

                    //作废尚未完成的查勤记录
                    List<DutyCheckLog> updatList = new List<DutyCheckLog>();

                    foreach (DutyCheckLog log in Query.ToList())
                    {
                        log.StatusId = new Guid("124A8562-EAC8-4C09-8758-A6E312974552");//已作废

                        updatList.Add(log);
                    }
                    db.DutyCheckLog.UpdateRange(updatList);
                    db.SaveChanges();

                    //作废未完成的查勤包
                    var packages = db.DutyCheckPackage.Where(p => p.PackageStatusId != new Guid("24AC9875-C463-47B6-8147-5845874C3CAF"));
                    List<DutyCheckPackage> cancellist = new List<DutyCheckPackage>();
                    foreach (DutyCheckPackage dcp in packages)
                    {
                        dcp.PackageStatusId= new Guid("124A8562-EAC8-4C09-8758-A6E312974552");//已作废
                        cancellist.Add(dcp);
                    }
                    db.DutyCheckPackage.UpdateRange(cancellist);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("DutyCheckPackageTimePlanOnChange is Error:"+ex.Message);
            }
        }


        /// <summary>
        /// 获取查勤点日志
        /// </summary>
        private static DutyCheckLog GetSiteLog(Guid organizationId, TimePeriod tp, MonitorySite monitorPoint, Guid dayPeriodId)
        {
            return new DutyCheckLog
            {
                DutyCheckLogId = Guid.NewGuid(),
                TimePeriod = tp,
                //DutyCheckSiteSchedule = dss,
                //PlanDate = time,
                //DutyCheckStaffId = dss.CheckManId,
                OrganizationId = organizationId,
                StatusId = new Guid("361ADFE9-E58A-4C88-B191-B742CC212443"), //未开始
                RecordTypeId = new Guid("359A58FA-0BAB-45A3-ACAF-98EB73228B95"),  //网络查勤
                DutyCheckSiteSchedule=new DutyCheckSiteSchedule
                {
                    CheckDutySiteId= monitorPoint.MonitorySiteId,
                },
                DayPeriodId= dayPeriodId
            };
        }


        /// <summary>
        /// 检查是否需要生成查勤包
        /// </summary>
        /// <returns></returns>
        public static bool CheckDutyPackageExist(Guid organizationId)
        {
            bool isExist = true;

            using (var db = new AllInOneContext.AllInOneContext())
            {

                var package = db.DutyCheckPackage
                            .Where(p => p.StartTime <= DateTime.Now
                             && p.EndTime >= DateTime.Now
                             && p.OrganizationId.Equals(organizationId));
                if (package.Count() > 0)
                {
                    isExist = false;
                }
            }
            return isExist;
        }

        /// <summary>
        /// 同步哨位记录至勤务排班记录
        /// </summary>
        /// <param name="punchLog"></param>
        public static void Synchronization(PunchLog punchLog)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    //取当前记录
                    DutyCheckLog log = db.DutyCheckLog
                                     .Include(t => t.DutyCheckSiteSchedule).ThenInclude(t => t.CheckMan).ThenInclude(t => t.Organization)
                                     .Include(t => t.DutyCheckSiteSchedule).ThenInclude(t => t.CheckMan).ThenInclude(t => t.PositionType)
                                     .Include(t => t.DayPeriod).ThenInclude(t => t.TimePeriods)
                                     .FirstOrDefault(p => p.DutyCheckSiteSchedule != null && p.DutyCheckSiteScheduleId.Equals(punchLog.OnDutyStaff.StaffId)
                                     && new DateTime(p.PlanDate.Value.Year, p.PlanDate.Value.Month, p.PlanDate.Value.Day, p.TimePeriod.StartTime.Hour, p.TimePeriod.StartTime.Minute, p.TimePeriod.StartTime.Second) <= punchLog.LogTime
                                     && new DateTime(p.PlanDate.Value.Year, p.PlanDate.Value.Month, p.PlanDate.Value.Day, p.TimePeriod.EndTime.Hour, p.TimePeriod.EndTime.Minute, p.TimePeriod.EndTime.Second) >= punchLog.LogTime);

                    //
                    if (log == null)
                        return;
                    //同步记录
                    log.MainAppriseId = log.MainAppriseId;
                    log.RecordTime = punchLog.LogTime;
                    log.StatusId = new Guid("24AC9875-C463-47B6-8147-5845874C3CAF");

                    db.DutyCheckLog.Update(log);
                    db.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Synchronization is Exception:"+ex.Message);
            }
        }


        /// <summary>
        /// 获取随机抽查点列表
        /// </summary>
        /// <param name="randomRate">随机抽查率</param>
        /// <param name="dutycheckPointOrganizations">查勤点组织机构列表</param>
        /// <returns></returns>
        public static List<Guid> GetRandomOrganizationList(double randomRate, List<Guid> dutycheckPointOrganizations)
        {
            List<Guid> randoms = new List<Guid>();
            int dutycheckPointsCount = dutycheckPointOrganizations.Count;
            int randomCount = (int)(randomRate * dutycheckPointOrganizations.Count / 100.0D + 0.5D);
            Random r = new Random();
            for (int i = 0; i < randomCount; i++)
            {
                var index = r.Next(dutycheckPointsCount);
                randoms.Add(dutycheckPointOrganizations[index]);
            }
            return randoms;
        }

        /// <summary>
        /// 预生成查勤日志
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="tp"></param>
        /// <param name="dutycheckPointOrganizationId"></param>
        /// <param name="dayPeriodId"></param>
        /// <returns></returns>
        private static DutyCheckLog PreCreateDutycheckLog(Guid organizationId, TimePeriod tp, Guid dutycheckPointOrganizationId , Guid dayPeriodId)
        {
            return new DutyCheckLog
            {
                DutyCheckLogId = Guid.NewGuid(),
                TimePeriod = tp,
                OrganizationId = organizationId,
                StatusId = new Guid("361ADFE9-E58A-4C88-B191-B742CC212443"), //未开始
                RecordTypeId = new Guid("a0002016-e009-b019-e001-abcd18000001"),  //网络查勤
                DutyCheckSiteSchedule = new DutyCheckSiteSchedule
                {
                    SiteOrganizationId = dutycheckPointOrganizationId
                },
                DayPeriodId = dayPeriodId
            };
        }


    }
}
