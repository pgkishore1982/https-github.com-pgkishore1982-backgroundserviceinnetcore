using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WorkerRepository.Interfaces;
using Quartz;
using Quartz.Impl;

namespace WorkerBackgroundService
{
   public class JobExecute:IJob
    {
        IWriteFile _writeFile;
        public JobExecute(IWriteFile writeFile)
        {
            _writeFile = writeFile;
        }

        public Task Execute(IJobExecutionContext context)
        {


            _writeFile.Write();


            return Task.CompletedTask;
        }
    }
}
