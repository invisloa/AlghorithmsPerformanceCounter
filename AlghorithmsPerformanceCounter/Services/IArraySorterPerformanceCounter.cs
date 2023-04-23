using AlghorithmsPerformanceCounter.Models.Algorithms;
using AlghorithmsPerformanceCounter.Services;
using System.Collections.Generic;

namespace AlghorithmsPerformanceCounter.Services
{
	public interface IArraySorterPerformanceCounter
	{
		List<AbstractSortingAlgorithm> AllUsedAlgoritms { get; }
		void SortMultipleArrays(int[][] arraysToSort);
		public int TotalNumberOfArraysToSort { get; }
		public int TotalNumberOfValuesInArray { get; }

	}
}