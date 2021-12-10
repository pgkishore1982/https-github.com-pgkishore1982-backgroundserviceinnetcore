using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.Extensions.Options;
using Serilog;
using WorkerRepository.Models;
using WorkerRepository.Interfaces;
using WorkerRepository;
using WorkerBackgroundService;
using WorkerApplication;
using WorkerApplication.Interfaces;
using Quartz;
using Quartz.Spi;
using Quartz.Impl;
using Quartz.NetCore.DependencyInjection;




namespace WorkerServices
{
    public class Program
    {
      
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
           .WriteTo.File(@"C:\SampleWorkerServiceLog\seriallog.txt")
           .CreateLogger();
            CreateHostBuilder(args).Build().Run();
        }

       
        public static IHostBuilder CreateHostBuilder(string[] args) =>



            Host.CreateDefaultBuilder(args)
                .UseWindowsService()                
                .ConfigureAppConfiguration((hostContext, config) =>
                {

                    config.SetBasePath(hostContext.HostingEnvironment.ContentRootPath);
                    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                    config.AddEnvironmentVariables();
                    config.Build();

                })
                .ConfigureServices((hostContext, services) =>
                {
                    


                    services.Configure<AppSettings>(hostContext.Configuration.GetSection(nameof(AppSettings)));
                    services.AddTransient<AppSettings>(_ => _.GetRequiredService<IOptions<AppSettings>>().Value);
                    services.AddHostedService<Worker>();
                    services.AddScoped<IWriteFile, WriteFile>();
                    services.AddSingleton<ISetJobExecuteTime, SetJobExecuteTime>();
                    services.AddSingleton<IJobFactory, JobFactory>();
                    services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
                    services.AddSingleton<JobExecute>();

                   
                });



    }
}
