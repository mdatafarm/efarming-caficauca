using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EFarming.Web.Util
{
    public class JobScheduler
    {
        public static void Start()
        {
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();

            IJobDetail job = JobBuilder.Create<UpdateInvoicesJob>().Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                // .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(17,47))
                .WithSchedule(CronScheduleBuilder.WeeklyOnDayAndHourAndMinute(DayOfWeek.Wednesday, 08, 00))
                .Build();
            scheduler.ScheduleJob(job, trigger);
        }

        public static void UpdateProductivity()
        {
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();

            IJobDetail job = JobBuilder.Create<UpdateProductivityJob>().Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("trigger2", "trigger2")
               //.WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(11,10))
               .WithSchedule(CronScheduleBuilder.WeeklyOnDayAndHourAndMinute(DayOfWeek.Thursday, 11, 55))
                .Build();
            scheduler.ScheduleJob(job, trigger);

        }
    }
}