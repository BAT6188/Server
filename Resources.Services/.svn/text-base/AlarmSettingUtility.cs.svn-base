using AlarmAndPlan.Model;
using Infrastructure.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resources.Services
{
    /// <summary>
    /// 主要实现报警配置删除
    /// </summary>
    public class AlarmSettingUtility
    {
        /// <summary>
        /// 移除报警配置的预案
        /// </summary>
        private static void RemoveAlarmSettingPlan(AllInOneContext.AllInOneContext dbContext, List<Guid> planIds)
        {
            var plans = dbContext.Plan.Include(t => t.Actions).ThenInclude(t => t.PlanActions).ThenInclude(t => t.Action).
                       Include(t => t.Actions).ThenInclude(t => t.PlanDevice).ThenInclude(t => t.DeviceType).Where(t => planIds.Contains(t.PlanId)).ToList();
            foreach (var plan in plans)
            {
                plan.Actions.ForEach(t => dbContext.Set<PredefinedAction>().RemoveRange(t.PlanActions));
                dbContext.Set<PlanAction>().RemoveRange(plan.Actions);
                dbContext.Plan.Remove(plan);
            }
            dbContext.SaveChanges();
        }

        /// <summary>
        /// 移除报警关联的排程
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="planIds"></param>
        private static void RemoveAlarmSettingSchedule(AllInOneContext.AllInOneContext dbContext, List<Guid> scheduleIds)
        {
            var shcedules = dbContext.Schedule
                                        .Include(t => t.ScheduleCycle).ThenInclude(t => t.DayPeriods).ThenInclude(t => t.TimePeriods)
                                        .Where(t => scheduleIds.Contains(t.ScheduleId)).ToList();
            shcedules.ForEach(t => {
                dbContext.Schedule.Remove(t);
                if (t.ScheduleCycle != null)
                {
                    dbContext.Set<ScheduleCycle>().Remove(t.ScheduleCycle);
                    if (t.ScheduleCycle.DayPeriods != null && t.ScheduleCycle.DayPeriods.Count > 0)
                    {
                        dbContext.Set<DayPeriod>().RemoveRange(t.ScheduleCycle.DayPeriods);
                        t.ScheduleCycle.DayPeriods.ForEach(f => dbContext.Set<TimePeriod>().RemoveRange(f.TimePeriods));
                    }
                }
            });
            dbContext.SaveChanges();
        }

        /// <summary>
        /// 移除报警设置
        /// </summary>
        /// <param name="db"></param>
        /// <param name="alarmSourceIds"></param>
        public static void RemoveAlarmSetting(AllInOneContext.AllInOneContext db, List<Guid> alarmSourceIds)
        {
            //删除报警配置
            var alarmsettings = db.AlarmSetting.Where(t => alarmSourceIds.Contains(t.AlarmSourceId)).ToList();
            if (alarmsettings != null && alarmsettings.Count > 0)
            {
                List<Guid> removePlanIds = new List<Guid>();
                List<Guid> removeScheduleIds = new List<Guid>();
                alarmsettings.ForEach(t =>
                {
                    if (t.BeforePlanId != null)
                        removePlanIds.Add(t.BeforePlanId.Value);
                    if (t.EmergencyPlanId != null)
                        removePlanIds.Add(t.EmergencyPlanId.Value);
                    removeScheduleIds.Add(t.ScheduleId);
                });
                db.AlarmSetting.RemoveRange(alarmsettings);
                AlarmSettingUtility.RemoveAlarmSettingPlan(db, removePlanIds);
                AlarmSettingUtility.RemoveAlarmSettingSchedule(db, removeScheduleIds);
                db.SaveChanges();
            }
        }
    }
}
