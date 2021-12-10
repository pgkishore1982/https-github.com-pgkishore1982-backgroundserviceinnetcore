using System;
using System.Collections.Generic;
using System.Text;
using WorkerRepository.Models;
using WorkerRepository.Interfaces;
using WorkerApplication.Interfaces;
using System.IO;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Quartz.NetCore.DependencyInjection;


namespace WorkerApplication
{
   public class SetJobExecuteTime: ISetJobExecuteTime
    {
        private readonly AppSettings _appSettings;
        private readonly ISchedulerFactory _schedulerFactory;
        private readonly IJobFactory _jobFactory;
       
        public SetJobExecuteTime(AppSettings appSettings, ISchedulerFactory schedulerFactory, IJobFactory jobFactory)
        {

            _appSettings = appSettings;
            _schedulerFactory = schedulerFactory;
            _jobFactory = jobFactory;
            

        }
        public IScheduler Scheduler { get; set; }


        public  async Task SetExecuteTime(CancellationToken stoppingToken)
        {


            Scheduler = await _schedulerFactory.GetScheduler(stoppingToken);
            Scheduler.JobFactory = _jobFactory;


            var job = CreateJob();
            var trigger = CreateTrigger();

            await Scheduler.ScheduleJob(job, trigger, stoppingToken);


            await Scheduler.Start(stoppingToken);



            
        }
        private ITrigger CreateTrigger()
        {
            return TriggerBuilder
                .Create()
                .WithCronSchedule(_appSettings.CronTime)
                .WithDescription(_appSettings.CronTime)
                .Build();
        }

        private IJobDetail CreateJob()
        {

            return JobBuilder
                .Create<JobExecute>()
                .Build();
        }
    }
}
