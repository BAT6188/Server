using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Model
{
    public class ScheduleUtility
    {
        #region  排程运行时间计算
        /// <summary>
        /// 计算排程下一运行时间
        /// </summary>
        /// <param name="cycle"></param>
        /// <param name="startTime"></param>
        /// <returns></returns>
        public static DateTime? GetExecuteTime(Schedule schedule, DateTime startTime)
        {
            DateTime? executeTime = null;
            if ((startTime - schedule.EffectiveTime).Seconds >= 0 &&
                  (schedule.ExpirationTime == null || (startTime - schedule.ExpirationTime.Value).Seconds <= 0))
            {
                ScheduleCycle cycle = schedule.ScheduleCycle;
                if (cycle != null && cycle.DayPeriods != null && cycle.DayPeriods.Count > 0)
                {
                    //每天
                    if (cycle.CycleType == null || cycle.CycleType.SystemOptionCode.Equals("13700001"))
                    {
                        executeTime = GetDayCycleExecuteTime(cycle, startTime);
                    }
                    //周
                    else if (cycle.CycleType.SystemOptionCode.Equals("13700002"))
                    {
                        executeTime = GetWeekCycleExecuteTime(cycle, startTime);
                    }
                    //月
                    else if (cycle.CycleType.SystemOptionCode.Equals("13700003"))
                    {
                        executeTime = GetMonthCycleExecuteTime(cycle, startTime);
                    }
                }
            }
            return executeTime;
        }

        /// <summary>
        /// 计算周排程下一执行时间
        /// </summary>
        /// <param name="cycle"></param>
        /// <param name="startTime"></param>
        /// <returns></returns>
        static DateTime? GetMonthCycleExecuteTime(ScheduleCycle cycle, DateTime startTime)
        {
            //if (cycle.Days == null || cycle.Days.Length == 0 || cycle.Months == null || cycle.Months.Length == 0)
            //    return null;
            ////当前月份是否有满足条件的，没有则从下一个月开始查找
            //bool hasFound = false;
            //DateTime executeTime = GetDayCycleExecuteTime(cycle, startTime).Value;
            //DateTime firstTimePeriod = cycle.TimePeriods.OrderBy(t => t.StartTime).First().StartTime;
            //int matchMonth = executeTime.Month;
            //if (cycle.Months.Contains(matchMonth))
            //{
            //    int matchDay = executeTime.Day;
            //    int day = cycle.Days.FirstOrDefault(t => t >= matchDay);
            //    if (matchDay == day)
            //    {
            //        hasFound = true;
            //    }
            //    else if (day > matchDay)
            //    {
            //        executeTime = new DateTime(executeTime.Year, matchMonth, day,
            //            firstTimePeriod.Hour,
            //            firstTimePeriod.Minute,
            //            firstTimePeriod.Second);
            //        hasFound = true;
            //    }
            //}
            //if (!hasFound)
            //{
            //    int month = cycle.Months.FirstOrDefault(t => t > matchMonth);
            //    if (month == 0) //next year
            //    {
            //        executeTime = new DateTime(executeTime.Year + 1, cycle.Months[0], cycle.Days[0],
            //            firstTimePeriod.Hour, firstTimePeriod.Minute, firstTimePeriod.Second);
            //    }
            //    else // 
            //    {
            //        executeTime = new DateTime(executeTime.Year, month, cycle.Days[0],
            //            firstTimePeriod.Hour, firstTimePeriod.Minute, firstTimePeriod.Second);
            //    }
            //}
            //return executeTime;

            if (cycle.Days == null || cycle.Days.Length == 0 || cycle.Months == null || cycle.Months.Length == 0)
                return null;
            DateTime? executeTime = null;

            int matchMonth = startTime.Month;
            if (cycle.Months.Contains(matchMonth))
            {
                //从startTime当天开始找
                int matchDay = startTime.Day;
                DayPeriod dayPeriod = cycle.DayPeriods.FirstOrDefault(t => t.Day == matchDay);
                if (dayPeriod != null)
                    executeTime = GetDayCycleExecuteTime(dayPeriod, startTime);

                //没有找到符合条件的，从一天开始找
                if (executeTime == null)
                {
                    //找到
                    dayPeriod = cycle.DayPeriods.FirstOrDefault(t => t.Day > matchDay);
                    if (dayPeriod != null)
                    {
                        DateTime firstTime = dayPeriod.TimePeriods.OrderBy(t => t.StartTime).First().StartTime;
                        executeTime = startTime.Date.AddDays(dayPeriod.Day - matchDay).AddHours(firstTime.Hour).AddMinutes(firstTime.Minute).AddSeconds(firstTime.Second);
                    }
                }
            }

            //从下一个月开始找
            if (executeTime == null)
            {
                int year = 0;
                int month = cycle.Months.FirstOrDefault(t => t > matchMonth);
                if (month == 0)
                {
                    year = 1;
                    month = cycle.Months.OrderBy(t => t).First();
                }
                int day = cycle.Days.OrderBy(t => t).First();
                DateTime firstTime = cycle.DayPeriods.FirstOrDefault(t => t.Day == day).TimePeriods.OrderBy(t => t.StartTime).First().StartTime;
                executeTime = new DateTime(startTime.Year + year, month, day, firstTime.Hour, firstTime.Minute, firstTime.Second);
            }
            return executeTime;
        }

        /// <summary>
        /// 计算周排程下一执行时间
        /// </summary>
        /// <param name="cycle"></param>
        /// <param name="startTime"></param>
        /// <returns></returns>
        static DateTime? GetWeekCycleExecuteTime(ScheduleCycle cycle, DateTime startTime)
        {
            //if (cycle.WeekDays == null || cycle.WeekDays.Length == 0)
            //    return null;
            //DateTime executeTime = GetDayCycleExecuteTime(cycle, startTime).Value;
            //int matchDayOfWeek = (int)executeTime.DayOfWeek;
            //int dayOfWeek = cycle.WeekDays.FirstOrDefault(t => t >= matchDayOfWeek);
            //DateTime firstTimePeriod = cycle.TimePeriods.OrderBy(t => t.StartTime).First().StartTime;
            //if (matchDayOfWeek == dayOfWeek)
            //{
            //}
            //else if (matchDayOfWeek < dayOfWeek)
            //{
            //    executeTime = executeTime.AddDays(dayOfWeek - matchDayOfWeek).Date.
            //        AddHours(firstTimePeriod.Hour).AddMinutes(firstTimePeriod.Minute).AddSeconds(firstTimePeriod.Second);
            //}
            //else //next week
            //{
            //    int firstDayOfWeek = cycle.WeekDays.First();
            //    executeTime = executeTime.AddDays(7 + firstDayOfWeek - (int)executeTime.DayOfWeek).Date.
            //        AddHours(firstTimePeriod.Hour).AddMinutes(firstTimePeriod.Minute).AddSeconds(firstTimePeriod.Second);
            //}
            //return executeTime;

            if (cycle.WeekDays == null || cycle.WeekDays.Length == 0)
                return null;

            int matchDayOfWeek = (int)startTime.DayOfWeek;
            DateTime? executeTime = null;
            DayPeriod dayPeriod = cycle.DayPeriods.FirstOrDefault(t => t.DayOfWeek == matchDayOfWeek);
            if (dayPeriod != null)
                executeTime = GetDayCycleExecuteTime(dayPeriod, startTime);

            //当天的时间不满足，获取下一天
            if (executeTime == null)
            {
                //从下一天开始找
                List<DayPeriod> dayPeriods = cycle.DayPeriods.OrderBy(t => t.DayOfWeek).ToList();
                dayPeriod = dayPeriods.FirstOrDefault(t => t.DayOfWeek > matchDayOfWeek);
                int addDays = 0;
                if (dayPeriod != null)
                {
                    addDays = dayPeriod.DayOfWeek - matchDayOfWeek;
                }
                else //滚到下一周
                {
                    dayPeriod = dayPeriods.First();
                    addDays = 7 + dayPeriod.DayOfWeek - matchDayOfWeek;
                }

                DateTime firstTime = dayPeriod.TimePeriods.OrderBy(t => t.StartTime).First().StartTime;
                executeTime = startTime.Date.AddDays(addDays).AddHours(firstTime.Hour).AddMinutes(firstTime.Millisecond).AddSeconds(firstTime.Second);
            }
            return executeTime;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="cycle"></param>
        /// <returns></returns>
        static DateTime? GetDayCycleExecuteTime(ScheduleCycle cycle, DateTime startTime)
        {
            DateTime? executeTime =  GetDayCycleExecuteTime(cycle.DayPeriods.First(), startTime);
            if (executeTime == null)
            {
                List<TimePeriod> orderTimePeriods = cycle.DayPeriods.First().TimePeriods.OrderBy(t => t.StartTime).ToList();
                DateTime firstTimePeriod = orderTimePeriods.First().StartTime;
                executeTime = startTime.AddDays(1).Date.AddHours(firstTimePeriod.Hour).AddMinutes(firstTimePeriod.Minute).AddSeconds(firstTimePeriod.Second);
            }
            return executeTime;

            //string timeFormat = "{0:HH:mm:ss}";
            //string strStartTime = string.Format(timeFormat, startTime);
            ////排程第一个时间段
            //cycle.TimePeriods = cycle.TimePeriods.OrderBy(t => t.StartTime).ToList(); //避免计算时间错误
            //DateTime firstTimePeriod = cycle.TimePeriods.First().StartTime;

            //foreach (TimePeriod tm in cycle.TimePeriods)
            //{
            //    if (strStartTime.CompareTo(string.Format(timeFormat, tm.StartTime)) >= 0
            //        && strStartTime.CompareTo(string.Format(timeFormat, tm.EndTime)) <= 0)
            //    {
            //        return startTime;
            //    }
            //    else if (strStartTime.CompareTo(string.Format(timeFormat, tm.StartTime)) < 0)  //判断的前提是时间段已升序排序 
            //    {
            //        return startTime.Date.AddHours(tm.StartTime.Hour).AddMinutes(tm.StartTime.Minute).AddSeconds(tm.StartTime.Second);
            //    }
            //}
            ////没有找到满足条件的，从下一天
            //return startTime.AddDays(1).Date.AddHours(firstTimePeriod.Hour).AddMinutes(firstTimePeriod.Minute).AddSeconds(firstTimePeriod.Second);
        }
        #endregion


        /// <summary>
        /// 验证时间是否在排程中
        /// </summary>
        /// <param name="schedule"></param>
        /// <param name="checkTime"></param>
        /// <returns></returns>
        public static bool IsValidSchedule(Schedule schedule, DateTime checkTime)
        {
            //if (schedule != null)
            //{
            //    if ((checkTime - schedule.EffectiveTime).Seconds >= 0 &&
            //        (schedule.ExpirationTime == null || (checkTime - schedule.ExpirationTime.Value).Seconds <= 0))
            //    {
            //        ScheduleCycle cycle = schedule.ScheduleCycle;
            //        if (cycle != null && cycle.TimePeriods != null)
            //        {
            //            if (cycle.CycleType != null && cycle.CycleType.SystemOptionCode.Equals("13700002"))
            //            {
            //                int dayOfWeek = (int)checkTime.DayOfWeek;
            //                if (!cycle.WeekDays.Contains(dayOfWeek))
            //                {
            //                    Console.WriteLine("now not in schedule of week cycle,return false");
            //                    return false;
            //                }
            //            }
            //            else if (cycle.CycleType != null && cycle.CycleType.SystemOptionCode.Equals("13700003"))
            //            {
            //                int month = checkTime.Month;
            //                int day = checkTime.Day;
            //                if (!cycle.Months.Contains(month) || !cycle.Days.Contains(day))
            //                {
            //                    Console.WriteLine("now not in schedule of week cycle return false");
            //                    return false;
            //                }
            //            }
            //            string timeFormat = "{0:HH:mm}";
            //            string strCheckTime = string.Format(timeFormat, checkTime);
            //            foreach (TimePeriod tm in schedule.ScheduleCycle.TimePeriods)
            //            {
            //                if (strCheckTime.CompareTo(string.Format(timeFormat, tm.StartTime)) >= 0 &&
            //                    strCheckTime.CompareTo(string.Format(timeFormat, tm.EndTime)) <= 0)
            //                {
            //                    Console.WriteLine("符合排程条件，return true");
            //                    return true;
            //                }
            //            }
            //        }
            //    }
            //}
            //Console.WriteLine("Can not find the schedule....return false");
            //return false;

            if (schedule != null)
            {
                if ((checkTime - schedule.EffectiveTime).Seconds >= 0 &&
                    (schedule.ExpirationTime == null || (checkTime - schedule.ExpirationTime.Value).Seconds <= 0))
                {
                    ScheduleCycle cycle = schedule.ScheduleCycle;
                    List<TimePeriod> timePeriods = new List<TimePeriod>();
                    if (cycle != null && cycle.DayPeriods != null)
                    {
                        //周
                        if (cycle.CycleType != null && cycle.CycleType.SystemOptionCode.Equals("13700002"))
                        {
                            int dayOfWeek = (int)checkTime.DayOfWeek;
                            if (!cycle.WeekDays.Contains(dayOfWeek))
                            {
                                Console.WriteLine("now not in schedule of week cycle,return false");
                                return false;
                            }
                            timePeriods = cycle.DayPeriods.First(t => t.DayOfWeek == dayOfWeek).TimePeriods;
                        }
                        //月
                        else if (cycle.CycleType != null && cycle.CycleType.SystemOptionCode.Equals("13700003"))
                        {
                            int month = checkTime.Month;
                            int day = checkTime.Day;
                            if (!cycle.Months.Contains(month) || !cycle.Days.Contains(day))
                            {
                                Console.WriteLine("now not in schedule of week cycle return false");
                                return false;
                            }
                            timePeriods = cycle.DayPeriods.First(t => t.Day == day).TimePeriods;
                        }
                        //天
                        else
                        {
                            timePeriods = cycle.DayPeriods.First().TimePeriods;
                        }
                        string timeFormat = "{0:HH:mm}";
                        string strCheckTime = string.Format(timeFormat, checkTime);
                        foreach (TimePeriod tm in timePeriods)
                        {
                            if (strCheckTime.CompareTo(string.Format(timeFormat, tm.StartTime)) >= 0 &&
                                strCheckTime.CompareTo(string.Format(timeFormat, tm.EndTime)) <= 0)
                            {
                                Console.WriteLine("符合排程条件，return true");
                                return true;
                            }
                        }
                    }
                }
            }
            Console.WriteLine("Can not find the schedule....return false");
            return false;
        }

        /// <summary>
        /// 根据当前时间计算排程执行时间
        /// </summary>
        /// <param name="dayPeriod"></param>
        /// <param name="cycle"></param>
        /// <returns></returns>
        static DateTime? GetDayCycleExecuteTime(DayPeriod dayPeriod, DateTime startTime)
        {
            string timeFormat = "{0:HH:mm:ss}";
            string strStartTime = string.Format(timeFormat, startTime);
            //排程第一个时间段
            //Time排序
            List<TimePeriod> orderTimePeriods = dayPeriod.TimePeriods.OrderBy(t => t.StartTime).ToList();
            DateTime firstTimePeriod = orderTimePeriods.First().StartTime;

            foreach (TimePeriod tm in orderTimePeriods)
            {
                if (strStartTime.CompareTo(string.Format(timeFormat, tm.StartTime)) >= 0
                    && strStartTime.CompareTo(string.Format(timeFormat, tm.EndTime)) <= 0)
                {
                    return startTime;
                }
                else if (strStartTime.CompareTo(string.Format(timeFormat, tm.StartTime)) < 0)  //判断的前提是时间段已升序排序 
                {
                    return startTime.Date.AddHours(tm.StartTime.Hour).AddMinutes(tm.StartTime.Minute).AddSeconds(tm.StartTime.Second);
                }
            }
            //没有找到满足条件的，从下一天

            return null;
            //return startTime.AddDays(1).Date.AddHours(firstTimePeriod.Hour).AddMinutes(firstTimePeriod.Minute).AddSeconds(firstTimePeriod.Second);
        }

        /// <summary>
        /// 获取预案任务运行的时间段
        /// </summary>
        /// <param name="schedule"></param>
        /// <param name="executeTime">执行时间</param>
        /// <returns></returns>
        public static TimePeriod GetPlanTaskTimePeriod(Schedule schedule, DateTime executeTime)
        {
            //if (schedule != null)
            //{
            //    string timeFormat = "{0:HH:mm:ss}";
            //    string strCheckTime = string.Format(timeFormat, executeTime);
            //    var timePeriods = schedule.ScheduleCycle.TimePeriods.OrderBy(t => t.StartTime).ToList();
            //    foreach (TimePeriod tm in timePeriods)
            //    {
            //        if (strCheckTime.CompareTo(string.Format(timeFormat, tm.StartTime)) >= 0 &&
            //            strCheckTime.CompareTo(string.Format(timeFormat, tm.EndTime)) <= 0)
            //        {
            //            TimePeriod tp = new TimePeriod()
            //            {
            //                StartTime = executeTime,
            //                EndTime = executeTime.Date.AddHours(tm.EndTime.Hour).AddMinutes(tm.EndTime.Minute).AddSeconds(tm.EndTime.Second)
            //            };
            //            Console.WriteLine("找到预案运行的时间段...");
            //            return tp;
            //        }
            //    }
            //}
            //Console.WriteLine("没有找到预案任务运行的时间段...");
            //return null;

            if (schedule != null)
            {
                if ((executeTime - schedule.EffectiveTime).Seconds >= 0 &&
                    (schedule.ExpirationTime == null || (executeTime - schedule.ExpirationTime.Value).Seconds <= 0))
                {
                    ScheduleCycle cycle = schedule.ScheduleCycle;
                    List<TimePeriod> timePeriods = new List<TimePeriod>();
                    if (cycle != null && cycle.DayPeriods != null)
                    {
                        //周
                        if (cycle.CycleType != null && cycle.CycleType.SystemOptionCode.Equals("13700002"))
                        {
                            int dayOfWeek = (int)executeTime.DayOfWeek;
                            if (cycle.WeekDays.Contains(dayOfWeek))
                                timePeriods = cycle.DayPeriods.First(t => t.DayOfWeek == dayOfWeek).TimePeriods;
                        }
                        //月
                        else if (cycle.CycleType != null && cycle.CycleType.SystemOptionCode.Equals("13700003"))
                        {
                            int month = executeTime.Month;
                            int day = executeTime.Day;
                            if (cycle.Months.Contains(month) && cycle.Days.Contains(day))
                                timePeriods = cycle.DayPeriods.First(t => t.Day == day).TimePeriods;
                        }
                        //天
                        else
                        {
                            timePeriods = cycle.DayPeriods.First().TimePeriods;
                        }
                        string timeFormat = "{0:HH:mm}";
                        string strCheckTime = string.Format(timeFormat, executeTime);
                        foreach (TimePeriod tm in timePeriods)
                        {
                            if (strCheckTime.CompareTo(string.Format(timeFormat, tm.StartTime)) >= 0 &&
                                strCheckTime.CompareTo(string.Format(timeFormat, tm.EndTime)) <= 0)
                            {
                                TimePeriod tp = new TimePeriod()
                                {
                                    StartTime = executeTime,
                                    EndTime = executeTime.Date.AddHours(tm.EndTime.Hour).AddMinutes(tm.EndTime.Minute).AddSeconds(tm.EndTime.Second)
                                };
                                Console.WriteLine("找到预案运行的时间段...");
                                return tp;
                            }
                        }
                    }
                }
            }
            Console.WriteLine("没有找到预案任务运行的时间段...");
            return null;
        }

    }
}
