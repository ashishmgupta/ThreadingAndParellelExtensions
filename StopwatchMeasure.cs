using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ParallelExtensions
{
    public class StopwatchMeasure : IDisposable
    {
        private readonly Stopwatch stopwatch;
        private readonly string methodName;
        public StopwatchMeasure(string methodName)
        {
            this.stopwatch = Stopwatch.StartNew();
            this.methodName = methodName;
        }
        public void Measure(MethodBase method)
        {
            stopwatch.Start();
        }

        #region IDisposable Members

        public void Dispose()
        {
            stopwatch.Stop();
            Console.WriteLine("Total time taken by method '{0}' was {1} milliseconds.",methodName,stopwatch.ElapsedMilliseconds.ToString());
        }

        #endregion
    }
}
