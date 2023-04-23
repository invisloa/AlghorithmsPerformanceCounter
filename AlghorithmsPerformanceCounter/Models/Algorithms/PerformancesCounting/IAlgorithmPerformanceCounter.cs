using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlghorithmsPerformanceCounter.Models.Algorithms.PerformancesCounting
{
    public interface IAlgorithmPerformanceCounter
    {
        Stopwatch Stopwatch { get; }
        long ActionsTaken { get; }
        string AlgorithmName { get; }

        void IncrementActionsTaken();
    }
}
