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
		public override IAlgorithmPerformanceCounter SortArray(int[] array)
		{
			IAlgorithmPerformanceCounter performanceCounter = Factory.CreatePerformanceCounter(this.ToString());
			int[] copyArrayToSort = new int[array.Length];
			Array.Copy(array, copyArrayToSort, array.Length);
			performanceCounter.Stopwatch.Start();
			Heap_sort(copyArrayToSort, performanceCounter);
			performanceCounter.Stopwatch.Stop();
			return performanceCounter;
		}
		public void Heap_sort(int[] arr, IAlgorithmPerformanceCounter performanceCounter)
		{
			int n = arr.Length;
			// Build heap (rearrange array)
			for (int i = n / 2 - 1; i >= 0; i--)
				Heapify(arr, n, i, performanceCounter);
			// One by one extract an element from heap
			for (int i = n - 1; i >= 0; i--)
			{
				// Move current root to end
				int temp = arr[0];
				arr[0] = arr[i];
				arr[i] = temp;
				// call max heapify on the reduced heap
				Heapify(arr, i, 0, performanceCounter);
			}
		}

		public void Heapify(int[] arr, int n, int i, IAlgorithmPerformanceCounter performanceCounter)
		{
			performanceCounter.IncrementActionsTaken();   // every Heapify in a heapsort is a meaningfull action
			int largest = i; // Initialize largest as root
			int left = 2 * i + 1;
			int right = 2 * i + 2;
			// If left child is larger than root
			if (left < n && arr[left] > arr[largest])
				largest = left;
			// If right child is larger than largest so far
			if (right < n && arr[right] > arr[largest])
				largest = right;
			// If largest is not root
			if (largest != i)
			{
				int swap = arr[i];
				arr[i] = arr[largest];
				arr[largest] = swap;
				// Recursively heapify the affected sub-tree
				Heapify(arr, n, largest, performanceCounter);
			}
		}

	}
}
