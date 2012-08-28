using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelExtensions
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Main thread Id {0} " , Thread.CurrentThread.ManagedThreadId.ToString());
            CPUBoundOperation cpuBound = new CPUBoundOperation();
            cpuBound.DoWorkByCreatingThreads();
            //puBound.DoWorkByThreadPool();
            //cpuBound.DoWorkByTasks();
            //cpuBound.DoWorkByParallelForEach();
            Console.ReadLine();
        }
    }
}
