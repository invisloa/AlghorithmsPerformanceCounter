using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlghorithmsPerformanceCounter.Models.Algorithms.PerformancesCounting
{
    public class PerformancesCounter : IAlgorithmPerformanceCounter
    {
        long actionsTaken = 0;
        Stopwatch stopwatch = new Stopwatch();
        private readonly string algorithmName;

        public long ActionsTaken => actionsTaken;
        public Stopwatch Stopwatch => stopwatch;

        public string AlgorithmName => algorithmName;
        public void IncrementActionsTaken()
        {
            actionsTaken++;
        }
        public PerformancesCounter(string algorithmName)
        {
            this.algorithmName = algorithmName;
        }
    }
}
