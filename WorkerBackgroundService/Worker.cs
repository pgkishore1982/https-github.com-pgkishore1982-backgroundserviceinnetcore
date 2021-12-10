using System;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using WorkerRepository.Models;
using WorkerRepository.Interfaces;
using WorkerApplication.Interfaces;
using System.IO;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;


namespace WorkerBackgroundService
{
    public class Worker:BackgroundService
    {

        //private readonly AppSettings _appSettings;
        //private readonly ISchedulerFactory _schedulerFactory;
        //private readonly IJobFactory _jobFactory;
        private readonly ISetJobExecuteTime _setJob;


        //public Worker(AppSettings appSettings, ISchedulerFactory schedulerFactory, IJobFactory jobFactory)
        //{

        //    _appSettings = appSettings;
        //    _schedulerFactory = schedulerFactory;
        //    _jobFactory = jobFactory;
        //}

        public Worker(ISetJobExecuteTime setJob)
        {

            _setJob = setJob;
        }
        //public IScheduler Scheduler { get; set; }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _setJob.SetExecuteTime(stoppingToken);

            //Scheduler = await _schedulerFactory.GetScheduler(stoppingToken);
            //Scheduler.JobFactory = _jobFactory;


            //var job = CreateJob();
            //var trigger = CreateTrigger();

            //await Scheduler.ScheduleJob(job, trigger, stoppingToken);


            //await Scheduler.Start(stoppingToken);
         


            //while (!stoppingToken.IsCancellationRequested)
            //{


            //    await Task.Delay(1000, stoppingToken);
            //}
        }

        //private ITrigger CreateTrigger()
        //{
        //    return TriggerBuilder
        //        .Create()
        //        .WithCronSchedule(_appSettings.CronTime)
        //        .WithDescription(_appSettings.CronTime)
        //        .Build();
        //}

        //private IJobDetail CreateJob()
        //{

        //    return JobBuilder
        //        .Create<JobExecute>()
        //        .Build();
        //}
    }
}
