﻿using AlghorithmsPerformanceCounter.Models.Algorithms.PerformancesCounting;
using AlghorithmsPerformanceCounter.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlghorithmsPerformanceCounter.Models.Algorithms
{
    internal class LinqSort : AbstractSortingAlgorithm
	{
		public override IAlgorithmPerformanceCounter SortArray(int[] array)
		{
			IAlgorithmPerformanceCounter performanceCounter = Factory.CreatePerformanceCounter(this.ToString());

			int[] copyArrayToSort = new int[array.Length];
			Array.Copy(array, copyArrayToSort, array.Length);
			performanceCounter.Stopwatch.Start();
			copyArrayToSort = array.OrderBy(x => x).ToArray();
			performanceCounter.Stopwatch.Stop();
			return performanceCounter;
		}
		public override string ToString() { return "Linq Sort"; }
	}
}

