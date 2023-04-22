using AlghorithmsPerformanceCounter.Services;
using System.Diagnostics;

namespace AlghorithmsPerformanceCounter.Models.Algorithms
{
	public abstract class AbstractSortingAlgorithm : IAlgorithmPerformanceCounter
	{
		protected long actionsTaken = 0;
		protected Stopwatch stopwatch = new Stopwatch();
		public long ActionsCounted => actionsTaken;
		public Stopwatch Stopwatch => stopwatch;
		public void ResetPerformance()
		{
			actionsTaken = 0;
			stopwatch.Reset();
		}
		/// <summary>
		/// SortArray works on a copy of the provided array, to make all sorting algorithms work on the same arrays
		/// </summary>
		/// <param name="array"></param>
		/// <returns></returns>
		public abstract int[] SortArray(int[] array);

		public int[][] SortMultipleArrays(int[][] array)
		{
			foreach (int[] arrayToSort in array)
			{
				SortArray(arrayToSort);
			}
			return array;
		}
		public abstract override string ToString();
	}
}