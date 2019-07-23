using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwait
{
    class Program
    {
        static void Main(string[] args)
        {
            //Creating a task to finish
            Task t = LogMainAsync();

            //This line will be called even if LogMain is still processing
            LogToEventViewer();

            //Wait for task t to finish
            t.Wait();

            //Print message once t is done working
            Console.WriteLine("Finished");
            Console.Read();
        }

        //Log Main is to encapsulate a function that will run asynchronously
        private async static Task LogMainAsync()
        {
            //Exception should be handled at aysnchronous funciton level
            try
            {
                await Task.Run(() => LogToDB());
            }
            catch
            {
            }
        }

        //Actual function that will run async
        private static void LogToDB()
        {
            Console.WriteLine("Logging to database");
            Thread.Sleep(5000);
            Console.WriteLine("out of sleep");
        }

        //Synchronous call
        private static void LogToEventViewer()
        {
            Console.WriteLine("Logging to EventViewer");
        }
    }
}
