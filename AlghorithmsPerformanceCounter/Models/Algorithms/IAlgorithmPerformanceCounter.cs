using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlghorithmsPerformanceCounter.Models.Algorithms
{
	public interface IAlgorithmPerformanceCounter
	{
		/// <summary>
		/// This method retunrs an array for testing purposes.
		/// </summary>
		/// <param name="array">The array of integers to be sorted.</param>
		/// <returns>The sorted array of integers.</returns>
		int[] SortArray(int[] array);
		int[][] SortMultipleArrays(int[][] array);

		Stopwatch Stopwatch { get; }
		public long ActionsCounted {  get; }
		public void ResetPerformance();
	}
}
