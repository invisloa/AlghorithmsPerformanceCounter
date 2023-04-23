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
		private PerformancesCounter performanceCounter;

		public PerformancesCounter PerformanceCounter { get => performanceCounter; }

		public Quicksort()
		{
			performanceCounter = Factory.CreatePerformanceCounter(this.ToString());
		}

		public override string ToString() { return "Quick Sort"; }
		public override IAlgorithmPerformanceCounter SortArray(int[] array) // override the SortArray method to sort an single array
		{
			int[] copyArrayToSort = new int[array.Length]; // create a new integer array with the same length as the input array
			Array.Copy(array, copyArrayToSort, array.Length); // copy the input array to the new array
			performanceCounter.Stopwatch.Start();
			QSort(copyArrayToSort, 0, copyArrayToSort.Length - 1); // call the QSort method to sort the new array
			performanceCounter.Stopwatch.Stop(); // stop the stopwatch
			return performanceCounter; // return the sorted array
		}

		public void QSort(int[] array, int left, int right) // define a method called QSort that takes an integer array and the left and right indices of the sub-array to sort
		{
			performanceCounter.IncrementActionsTaken(); // increment the actionsTaken counter on every recursive call to QSort

			if (left < right) // if the sub-array has more than one element
			{
				int pivotIndex = Partition(array, left, right); // partition the sub-array around a pivot element
				QSort(array, left, pivotIndex - 1); // recursively sort the left sub-array
				QSort(array, pivotIndex + 1, right); // recursively sort the right sub-array
			}
		}

		int Partition(int[] array, int left, int right) // define a method called Partition that takes an integer array and the left and right indices of the sub-array to partition
		{
			int pivot = array[right]; // select the last element of the sub-array as the pivot element
			int i = left - 1; // initialize the index of the smaller element
			for (int j = left; j < right; j++) // loop through the sub-array from left to right
			{
				if (array[j] <= pivot) // if the current element is less than or equal to the pivot
				{
					i++; // increment the index of the smaller element
					Swap(array, i, j); // swap the current element with the element at the smaller index
				}
			}
			Swap(array, i + 1, right); // swap the pivot element with the element at the smaller index + 1
			return i + 1; // return the index of the pivot element
		}

		void Swap(int[] array, int i, int j) // define a method called Swap that takes an integer array and the indices of two elements to swap
		{
			int temp = array[i]; // store the value of the first element in a temporary variable
			array[i] = array[j]; // assign the value of the second element to the first element
			array[j] = temp; // assign the value of the temporary variable to the second element
		}

	}
}