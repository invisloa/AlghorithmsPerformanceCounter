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
			performanceCounter.IncrementActionsTaken();         // Increment the action counter every time the MSort function is called

			// If the array has less than two elements, it is already sorted
			if (arr.Length < 2)
			{
				return;
			}

			// Calculate the mid-point of the array for splitting
			int mid = arr.Length / 2;
			// Create two new arrays to hold the two halves of the original array
			int[] left = new int[mid];
			int[] right = new int[arr.Length - mid];

			// Copy the first half of the original array to the left array
			Array.Copy(arr, 0, left, 0, mid);
			// Copy the second half of the original array to the right array
			Array.Copy(arr, mid, right, 0, arr.Length - mid);

			// Recursively sort the left half
			MSort(left, performanceCounter);
			// Recursively sort the right half
			MSort(right, performanceCounter);

			// Merge the two sorted halves back together
			Merge(arr, left, right, performanceCounter);
		}

		void Merge(int[] arr, int[] left, int[] right, IAlgorithmPerformanceCounter performanceCounter)
		{
			int i = 0; // Index for left array
			int j = 0; // Index for right array
			int k = 0; // Index for merged array

			// Merge left and right arrays while there are elements in both
			while (i < left.Length && j < right.Length)
			{
				// Increment the action counter every time a comparison is made
				performanceCounter.IncrementActionsTaken();

				// If the current element in the left array is smaller or equal
				if (left[i] <= right[j])
				{
					// Put it in the merged array
					arr[k++] = left[i++];
				}
				else
				{
					// Otherwise, put the current element in the right array in the merged array
					arr[k++] = right[j++];
				}
			}

			// If there are remaining elements in the left array, copy them to the merged array
			while (i < left.Length)
			{
				arr[k++] = left[i++];
			}

			// If there are remaining elements in the right array, copy them to the merged array
			while (j < right.Length)
			{
				arr[k++] = right[j++];
			}
		}
	}
}
