using AlghorithmsPerformanceCounter.Models.Algorithms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlghorithmsPerformanceCounter.Models.Algorithms
{
	internal class LinqSort : AbstractSortingAlgorithm
	{
		public override int[] SortArray(int[] array)
		{
			int[] copyArrayToSort = new int[array.Length];
			Array.Copy(array, copyArrayToSort, array.Length);
			stopwatch.Start();
			copyArrayToSort = array.OrderBy(x => x).ToArray();
			stopwatch.Stop();
			return copyArrayToSort;
		}


		public override string ToString() { return "Linq Sort"; }
	}
}

