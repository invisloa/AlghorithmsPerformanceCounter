using AlghorithmsPerformanceCounter.Models.Algorithms;
using AlghorithmsPerformanceCounter.Models.Algorithms.PerformancesCounting;
using AlghorithmsPerformanceCounter.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace AlghorithmsPerformanceCounter.Services
{
	public interface IAllAlgorithmsPerformanceCounter
	{
		List<AbstractSortingAlgorithm> AllUsedAlgoritms { get; }
		Task<ObservableCollection<ObservableCollection<IAlgorithmPerformanceCounter>>> SortAllAlgorithmsPerformancesAsync(int[][] arraysToSort);
		public int TotalNumberOfArraysToSort { get; }
		public int TotalNumberOfValuesInArray { get; }
	}
}