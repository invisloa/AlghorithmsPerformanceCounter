using AlghorithmsPerformanceCounter.Models.Algorithms;
using AlghorithmsPerformanceCounter.Models.Algorithms.PerformancesCounting;
using AlghorithmsPerformanceCounter.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AlghorithmsPerformanceCounter.Services
{
	public interface IAllAlgorithmsPerformanceCounter
	{
		List<AbstractSortingAlgorithm> AllUsedAlgoritms { get; }
		ObservableCollection<ObservableCollection<IAlgorithmPerformanceCounter>> SortMultipleArrays(int[][] arraysToSort);
		public int TotalNumberOfArraysToSort { get; }
		public int TotalNumberOfValuesInArray { get; }

	}
}