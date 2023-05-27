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
    internal class Heapsort : AbstractSortingAlgorithm
	{
		public override string ToString() { return "Heap Sort"; }
		public override string Name => this.ToString();

		public async override  Task<IAlgorithmPerformanceCounter> SortArray(int[] array)
		{
			IAlgorithmPerformanceCounter performanceCounter = Factory.CreatePerformanceCounter(this.ToString());
			int[] copyArrayToSort = new int[array.Length];
			Array.Copy(array, copyArrayToSort, array.Length);
			await Task.Run(() =>
			{
				performanceCounter.Stopwatch.Start();
				Heap_sort(copyArrayToSort, performanceCounter);
				performanceCounter.Stopwatch.Stop();
			});
			return performanceCounter;
		}
		public void Heap_sort(int[] arr, IAlgorithmPerformanceCounter performanceCounter)
		{
			// Set n as the length of the array
			int n = arr.Length;

			// Build heap (rearrange array)
			// Starting from the last parent node. (n/2)-1 is the last parent node
			for (int i = n / 2 - 1; i >= 0; i--)
			{
				// Call heapify on each parent node (moving largest to the top)
				Heapify(arr, n, i, performanceCounter);
			}

			// Extract elements from heap one by one
			for (int i = n - 1; i >= 0; i--)
			{
				// Move the root node to the end of the array
				int temp = arr[0];
				arr[0] = arr[i];
				arr[i] = temp;

				// Heapify the reduced heap (i.e., heapify the root node and consider the heap size as i)
				Heapify(arr, i, 0, performanceCounter);
			}
		}

		public void Heapify(int[] arr, int n, int i, IAlgorithmPerformanceCounter performanceCounter)
		{
			performanceCounter.IncrementActionsTaken();         // Increment the action counter every time the Heapify function is called

			// Initialize largest as root
			int largest = i;

			// Indexes of left and right children
			int left = 2 * i + 1;				// Left child is 2i+1
			int right = 2 * i + 2;              // Right child is 2i+2

			// Check if left child is larger than root
			if (left < n && arr[left] > arr[largest])
			{
				largest = left;
			}
			// Check if right child is larger than the current largest node
			if (right < n && arr[right] > arr[largest])
			{
				largest = right;
			}
			// If the largest node is not the root node
			if (largest != i)
			{
				// Swap the root node with the largest node
				int swap = arr[i];
				arr[i] = arr[largest];
				arr[largest] = swap;

				// Recursively heapify the sub-tree affected by the swap operation
				Heapify(arr, n, largest, performanceCounter);
			}
		}

	}
}
