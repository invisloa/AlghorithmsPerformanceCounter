using AlghorithmsPerformanceCounter.Models.Algorithms.PerformancesCounting;
using AlghorithmsPerformanceCounter.Services;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AlghorithmsPerformanceCounter.Models.Algorithms
{
	internal class BubleSort : AbstractSortingAlgorithm
	{
		public override string ToString() { return "Buble Sort"; }
		public override string Name => this.ToString();

		public async override Task<IAlgorithmPerformanceCounter> SortArray(int[] array)
		{
			IAlgorithmPerformanceCounter performanceCounter = Factory.CreatePerformanceCounter(this.ToString());
			int[] copyArrayToSort = new int[array.Length];
			Array.Copy(array, copyArrayToSort, array.Length);

			await Task.Run(() =>
			{
				performanceCounter.Stopwatch.Start();
				int n = copyArrayToSort.Length;
				for (int i = 0; i < n - 1; i++)
				{
					performanceCounter.IncrementActionsTaken();

					for (int j = 0; j < n - i - 1; j++)							// for every item in array
					{
						performanceCounter.IncrementActionsTaken();

						if (copyArrayToSort[j] > copyArrayToSort[j + 1])		// check if next item of array is >
						{
							int temp = copyArrayToSort[j];						
							copyArrayToSort[j] = copyArrayToSort[j + 1];
							copyArrayToSort[j + 1] = temp;						// if next element is > switch their places
						}
					}
				}
				performanceCounter.Stopwatch.Stop();
			});

			return performanceCounter;
		}
	}
}
