using AlghorithmsPerformanceCounter.Models.Algorithms;
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
		public override int[] SortArray(int[] array)
		{
			int[] copyArrayToSort = new int[array.Length];
			Array.Copy(array, copyArrayToSort, array.Length);
			stopwatch.Start();
			InsSort(copyArrayToSort);
			stopwatch.Stop();
			return copyArrayToSort;
		}
		public override string ToString() { return "Insertion Sort"; }
		public void InsSort(int[] arr)
		{
			int n = arr.Length;
			for (int i = 1; i < n; ++i)
			{
				actionsTaken++;
				int key = arr[i];
				int j = i - 1;

				/* Move elements of arr[0..i-1], that are greater than key, to one position ahead
				   of their current position */
				while (j >= 0 && arr[j] > key)
				{
					arr[j + 1] = arr[j];
					j = j - 1;
				}
				arr[j + 1] = key;		// Set the key to the next array element
			}
		}
	}
}
