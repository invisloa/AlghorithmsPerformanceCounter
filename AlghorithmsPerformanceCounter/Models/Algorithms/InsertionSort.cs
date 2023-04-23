using AlghorithmsPerformanceCounter.Models.Algorithms.PerformancesCounting;
using AlghorithmsPerformanceCounter.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlghorithmsPerformanceCounter.Models.Algorithms
{
    internal class InsertionSort : AbstractSortingAlgorithm
	{
		public override IAlgorithmPerformanceCounter SortArray(int[] array)
		{
			IAlgorithmPerformanceCounter performanceCounter = Factory.CreatePerformanceCounter(this.ToString());
			int[] copyArrayToSort = new int[array.Length];
			Array.Copy(array, copyArrayToSort, array.Length);
			performanceCounter.Stopwatch.Start();
			InsSort(copyArrayToSort, performanceCounter);
			performanceCounter.Stopwatch.Stop();
			return performanceCounter;
		}
		public override string ToString() { return "Insertion Sort"; }
		public void InsSort(int[] arr, IAlgorithmPerformanceCounter performanceCounter)
		{
			int n = arr.Length;
			for (int i = 1; i < n; ++i)
			{
				performanceCounter.IncrementActionsTaken(); 
				int key = arr[i];
				int j = i - 1;
				while (j >= 0 && arr[j] > key)
				{
					arr[j + 1] = arr[j];
					j = j - 1;
				}
				arr[j + 1] = key;
			}
		}
	}
}
