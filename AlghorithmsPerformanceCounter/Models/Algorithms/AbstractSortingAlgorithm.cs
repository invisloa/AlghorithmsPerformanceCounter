using AlghorithmsPerformanceCounter.Models.Algorithms.PerformancesCounting;
using AlghorithmsPerformanceCounter.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AlghorithmsPerformanceCounter.Models.Algorithms
{
	public abstract class AbstractSortingAlgorithm
	{
		/// <summary>
		/// SortArray works on a copy of the provided array, to make all sorting algorithms work on the same arrays
		/// </summary>
		/// <param name="array"></param>
		/// <returns></returns>
		public abstract Task<IAlgorithmPerformanceCounter> SortArray(int[] array);
		public abstract string Name { get; }

		public async Task<ObservableCollection<IAlgorithmPerformanceCounter>> SortAndListPerformancesAsync(int[][] arrays)
		{
			ObservableCollection<IAlgorithmPerformanceCounter> listOfPerformancesCounters = new ObservableCollection<IAlgorithmPerformanceCounter>();
			foreach (int[] arrayToSort in arrays)                                       // Sorting each array in all arrays
			{
				listOfPerformancesCounters.Add(await SortArray(arrayToSort));			// Sorting by one of the algorithms
			}
			return listOfPerformancesCounters;
		}

		public abstract override string ToString();
	}
}