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
		public override string ToString() { return "Insertion Sort"; }
		public override string Name => this.ToString();

		public async override Task<IAlgorithmPerformanceCounter> SortArray(int[] array)
		{
			IAlgorithmPerformanceCounter performanceCounter = Factory.CreatePerformanceCounter(this.ToString());
			int[] copyArrayToSort = new int[array.Length];
			Array.Copy(array, copyArrayToSort, array.Length);
			await Task.Run(() =>
			{
				performanceCounter.Stopwatch.Start();
				InsSort(copyArrayToSort, performanceCounter);
				performanceCounter.Stopwatch.Stop();
			});
			return performanceCounter;
		}

		public void InsSort(int[] arr, IAlgorithmPerformanceCounter performanceCounter)
		{
			// Set n as the length of the array
			int n = arr.Length;

			// Start from the second element (index 1). The first element (index 0) is already sorted by itself.
			for (int i = 1; i < n; ++i)
			{
				performanceCounter.IncrementActionsTaken();	// Increment the action counter every time a new element is considered for insertion

				// The element at index i is the key to be inserted into the sorted sequence [0..i-1]
				int key = arr[i];

				// j is the last index of the sorted sequence [0..i-1]
				int j = i - 1;

				// move elements of the sorted sequence [0..i-1] that are greater than the key
				while (j >= 0 && arr[j] > key)
				{
					performanceCounter.IncrementActionsTaken();	// Increment the action counter every time an element is moved in the array

					// Move the element at index j to index j+1
					arr[j + 1] = arr[j];

					// Decrement j to move to the previous element in the sorted sequence
					j = j - 1;
				}

				// Insert the key at the correct position in the sorted sequence
				arr[j + 1] = key;
			}
		}
	}
}
