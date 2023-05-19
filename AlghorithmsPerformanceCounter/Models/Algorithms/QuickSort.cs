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
    public class Quicksort : AbstractSortingAlgorithm
	{
		public override string ToString() { return "Quick Sort"; }
		public override string Name => this.ToString();

		public async override Task<IAlgorithmPerformanceCounter> SortArray(int[] array) 
		{
			IAlgorithmPerformanceCounter performanceCounter = Factory.CreatePerformanceCounter(this.ToString());
			int[] copyArrayToSort = new int[array.Length];
			Array.Copy(array, copyArrayToSort, array.Length);
			await Task.Run(() =>
						{
							performanceCounter.Stopwatch.Start();
							QSort(copyArrayToSort, 0, copyArrayToSort.Length - 1, performanceCounter);
							performanceCounter.Stopwatch.Stop();
						});
			return performanceCounter;
		}

		public void QSort(int[] array, int left, int right, IAlgorithmPerformanceCounter performanceCounter)
		{
			performanceCounter.IncrementActionsTaken();
			if (left < right) 
			{
				int pivotIndex = Partition(array, left, right, performanceCounter);
				QSort(array, left, pivotIndex - 1, performanceCounter);
				QSort(array, pivotIndex + 1, right, performanceCounter);
			}
		}

		int Partition(int[] array, int left, int right, IAlgorithmPerformanceCounter performanceCounter)
		{ 

			int pivot = array[right];
			int i = left - 1;
			for (int j = left; j < right; j++)
			{
				performanceCounter.IncrementActionsTaken();
				if (array[j] <= pivot)
				{
					i++;
					Swap(array, i, j);
				}
			}
			Swap(array, i + 1, right);
			return i + 1;
		}

		void Swap(int[] array, int i, int j)
		{
			int temp = array[i];
			array[i] = array[j];
			array[j] = temp;
		}
	}
}