using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Text;
using ZDFixService.Utility.Jobs;

namespace ZDFixService.Utility
{
    public class Scheduler
    {
        //static Scheduler()
        // {

        // }

        public static async void Init()
        {


            ISchedulerFactory sf = new StdSchedulerFactory();
            IScheduler sched = await sf.GetScheduler();


            // jobs can be scheduled before sched.start() has been called

            // job 1 will run every 20 seconds
            var pSHKTradeServiceJobCron = Configurations.Configuration["ZDFixService:Jobs:PSHKTradeServiceJob"].ToString();
            IJobDetail pSHKTradeServiceJob = JobBuilder.Create<PSHKTradeServiceJob>()
                .WithIdentity("PSHKTradeServiceJob", "group1")
                .Build();

            ICronTrigger triggerPSHKTradeServiceJob = (ICronTrigger)TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                .WithCronSchedule(pSHKTradeServiceJobCron)
                .Build();

            sched.ScheduleJob(pSHKTradeServiceJob, triggerPSHKTradeServiceJob);

            var testJobCron = Configurations.Configuration["ZDFixService:Jobs:TestJob"].ToString();

            // job 2 will run every other minute (at 15 seconds past the minute)
            var testJob = JobBuilder.Create<TestJob>()
                  .WithIdentity("TestJob", "group1")
                  .Build();

            var triggerTestJob = (ICronTrigger)TriggerBuilder.Create()
                 .WithIdentity("trigger2", "group1")
                 .WithCronSchedule(testJobCron)
                 .Build();

            sched.ScheduleJob(testJob, triggerTestJob);
            sched.Start();
        }
    }
}
