using AlghorithmsPerformanceCounter.Models.Algorithms.PerformancesCounting;
using AlghorithmsPerformanceCounter.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace AlghorithmsPerformanceCounter.Models.Algorithms
{
    public abstract class AbstractSortingAlgorithm
	{
		/// <summary>
		/// SortArray works on a copy of the provided array, to make all sorting algorithms work on the same arrays
		/// </summary>
		/// <param name="array"></param>
		/// <returns></returns>
		public abstract IAlgorithmPerformanceCounter SortArray(int[] array);

		public ObservableCollection<IAlgorithmPerformanceCounter> SortMultipleArrays(int[][] array)
		{
			ObservableCollection< IAlgorithmPerformanceCounter> listOffScores = new ObservableCollection<IAlgorithmPerformanceCounter>();
			foreach (int[] arrayToSort in array)
			{
				listOffScores.Add(SortArray(arrayToSort));
			}
			return listOffScores;
		}
		public abstract override string ToString();
	}
}