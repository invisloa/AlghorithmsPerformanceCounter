using AlghorithmsPerformanceCounter.Models.Algorithms.PerformancesCounting;
using AlghorithmsPerformanceCounter.Services;
using System;
using System.Diagnostics;

namespace AlghorithmsPerformanceCounter.Models.Algorithms
{
    internal class BubleSort : AbstractSortingAlgorithm
	{
		public override string ToString() { return "Buble Sort"; }
		public override IAlgorithmPerformanceCounter SortArray(int[] array)
		{
			IAlgorithmPerformanceCounter performanceCounter = Factory.CreatePerformanceCounter(this.ToString());
			int[] copyArrayToSort = new int[array.Length];
			Array.Copy(array, copyArrayToSort, array.Length);
			performanceCounter.Stopwatch.Start();
			{
				int n = copyArrayToSort.Length;
				for (int i = 0; i < n - 1; i++)
				{
					for (int j = 0; j < n - i - 1; j++)
					{
						if (copyArrayToSort[j] > copyArrayToSort[j + 1])
						{
							performanceCounter.IncrementActionsTaken(); 
							int temp = copyArrayToSort[j];
							copyArrayToSort[j] = copyArrayToSort[j + 1];
							copyArrayToSort[j + 1] = temp;
						}
					}
				}
			}
			performanceCounter.Stopwatch.Stop();
			return performanceCounter;
		}
	}
}
