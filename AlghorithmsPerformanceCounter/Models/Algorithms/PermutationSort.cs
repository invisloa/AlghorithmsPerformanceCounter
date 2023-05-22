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
	// algorithm only for testing purposes -> complexity of n!
	internal class PermutationSort : AbstractSortingAlgorithm
	{
		public override string ToString() { return "Permutation Sort"; }
		public override string Name => this.ToString();

		public async override Task<IAlgorithmPerformanceCounter> SortArray(int[] array)
		{
			IAlgorithmPerformanceCounter performanceCounter = Factory.CreatePerformanceCounter(this.ToString());
			int[] copyArrayToSort = new int[array.Length];
			Array.Copy(array, copyArrayToSort, array.Length);

			await Task.Run(() =>
			{
				performanceCounter.Stopwatch.Start();
				Permutation(copyArrayToSort, 0, copyArrayToSort.Length - 1, performanceCounter);
				performanceCounter.Stopwatch.Stop();
			});
			return performanceCounter;
		}

		void Permutation(int[] array, int start, int end, IAlgorithmPerformanceCounter performanceCounter)
		{
			if (start == end)
			{
				if (IsSorted(array))
				{
					return;
				}
			}
			else
			{
				for (int i = start; i <= end; i++)
				{
					performanceCounter.IncrementActionsTaken();
					Swap(ref array[start], ref array[i]);
					Permutation(array, start + 1, end, performanceCounter);
					Swap(ref array[start], ref array[i]); // Backtrack to restore the original order of elements
				}
			}
		}

		bool IsSorted(int[] array)
		{
			for (int i = 1; i < array.Length; i++)
			{
				if (array[i - 1] > array[i])
					return false;
			}
			return true;
		}

		static void Swap(ref int a, ref int b)
		{
			int temp = a;
			a = b;
			b = temp;
		}
	}
}
