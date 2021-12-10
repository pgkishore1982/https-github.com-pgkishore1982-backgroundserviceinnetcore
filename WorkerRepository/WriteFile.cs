using System;
using WorkerRepository.Models;
using WorkerRepository.Interfaces;
using System.IO;
namespace WorkerRepository
{
    public class WriteFile: IWriteFile
    {
        private readonly AppSettings _appSettings;

        public WriteFile(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }
        public void Write()
        {
            //using (StreamWriter sw = new StreamWriter(@"C:\SampleWorkerServiceLog\log.txt"))
            using (StreamWriter sw = new StreamWriter(_appSettings.LocalFolder))
            {

                sw.WriteLine("Local filepath:" + DateTime.Now);

            }
        }
    }
}
