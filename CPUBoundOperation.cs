using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Reflection;

namespace ParallelExtensions
{
    class CPUBoundOperation
    {
        HashSet<int> threadIds = new HashSet<int>();
        const int startRange = 33;
        const int endRange = 52;
        const int count = 20;
        List<int> values = Enumerable.Range(startRange, count).ToList();
        public void DoWorkByCreatingThreads()
        {
            using (StopwatchMeasure stopwatchMeasure = new StopwatchMeasure(MethodInfo.GetCurrentMethod().Name))
            {
                Console.WriteLine(MethodInfo.GetCurrentMethod().Name);
                List<Thread> threads = new List<Thread>();
                foreach (var value in values)
                {
                    threads.Add(new Thread(new ThreadStart(delegate { CalculateFactorial(value); })));
                }

                foreach (var thread in threads)
                {
                    thread.Start();
                }
                foreach (var thread in threads)
                {
                    thread.Join();
                }    
            }
            
            this.IterateThreadIds();
        }

        public void DoWorkByThreadPool()
        {
            Console.WriteLine(MethodInfo.GetCurrentMethod().Name);
            foreach (var value in values)
            {
                ThreadPool.QueueUserWorkItem(delegate { CalculateFactorial(value); });    
            }
                      
            Thread.Sleep(5000);
            this.IterateThreadIds();
        }

        public void DoWorkByTasks()
        {
            Console.WriteLine(MethodInfo.GetCurrentMethod().Name);
            List<Task> tasks = new List<Task>();
            foreach (var value in values)
            {
                tasks.Add(Task.Factory.StartNew(() => CalculateFactorial(value)));
            }
            Task.WaitAll(tasks.ToArray());
            this.IterateThreadIds();
        }

        public void DoWorkByParallelForEach()
        {
            Console.WriteLine(MethodInfo.GetCurrentMethod().Name);
            Parallel.ForEach(values, i=>CalculateFactorial(i));
            this.IterateThreadIds();
        }

        private void CalculateFactorial(int number)
        {
            long factorial = 1;
            for (int i = 1; i <= number; i++)
            {
                factorial *= i;
            }
            //Thread.Sleep(3000);
            lock (this)
            {
                threadIds.Add(Thread.CurrentThread.ManagedThreadId);
            }
            //Console.WriteLine("Thread Id {0} -  Factorial of number {1} is {2}", Thread.CurrentThread.ManagedThreadId.ToString(), number.ToString(), factorial.ToString());
        }

        public void IterateThreadIds()
        {
            Console.WriteLine("Total number of threads used :- {0}", threadIds.Count.ToString());
            threadIds.ToList().Sort();
            Console.WriteLine("Thread Ids :- "+ string.Join(",",threadIds.ToList()));
            if (threadIds.Count > 0)
            {
                threadIds.Clear();
            }
        }
    }
}
