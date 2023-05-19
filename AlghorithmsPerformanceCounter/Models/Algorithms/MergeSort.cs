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
    internal class MergeSort : AbstractSortingAlgorithm
	{
		public override string ToString() { return "Merge Sort"; }
		public override string Name => this.ToString();

		public async override Task<IAlgorithmPerformanceCounter> SortArray(int[] array)
		{
			IAlgorithmPerformanceCounter performanceCounter = Factory.CreatePerformanceCounter(this.ToString());
			int[] copyArrayToSort = new int[array.Length];
			Array.Copy(array, copyArrayToSort, array.Length);
			await Task.Run(() =>
				{
					performanceCounter.Stopwatch.Start();
					MSort(copyArrayToSort, performanceCounter);
					performanceCounter.Stopwatch.Stop();
				});
			return performanceCounter;
		}
		void MSort(int[] arr, IAlgorithmPerformanceCounter performanceCounter)
		{
			performanceCounter.IncrementActionsTaken();

			if (arr.Length < 2)
			{
				return;
			}

			int mid = arr.Length / 2;
			int[] left = new int[mid];
			int[] right = new int[arr.Length - mid];

			Array.Copy(arr, 0, left, 0, mid);
			Array.Copy(arr, mid, right, 0, arr.Length - mid);

			MSort(left, performanceCounter);
			MSort(right, performanceCounter);

			Merge(arr, left, right, performanceCounter);
		}

		void Merge(int[] arr, int[] left, int[] right, IAlgorithmPerformanceCounter performanceCounter)
		{
			performanceCounter.IncrementActionsTaken(); // merge is a meaningful operation (consulted)

			int i = 0;
			int j = 0;
			int k = 0;

			while (i < left.Length && j < right.Length)
			{
				if (left[i] <= right[j])
				{
					arr[k++] = left[i++];
				}
				else
				{
					arr[k++] = right[j++];
				}
			}

			while (i < left.Length)
			{
				arr[k++] = left[i++];
			}

			while (j < right.Length)
			{
				arr[k++] = right[j++];
			}
		}
	}
}
