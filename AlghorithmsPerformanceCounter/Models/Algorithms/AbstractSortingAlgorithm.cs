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

		public async Task<ObservableCollection<IAlgorithmPerformanceCounter>> SortMultipleArrays(int[][] arrays)
		{
			ObservableCollection<IAlgorithmPerformanceCounter> listOfCounters = new ObservableCollection<IAlgorithmPerformanceCounter>();
			foreach (int[] arrayToSort in arrays)
			{
				listOfCounters.Add(await SortArray(arrayToSort));
			}
			return listOfCounters;
		}

		public abstract override string ToString();
	}
}