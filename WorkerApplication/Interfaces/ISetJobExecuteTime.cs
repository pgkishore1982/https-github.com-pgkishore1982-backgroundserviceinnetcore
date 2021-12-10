using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WorkerApplication.Interfaces
{
    public interface ISetJobExecuteTime
    {
        Task SetExecuteTime(CancellationToken stoppingToken);
    }
}
