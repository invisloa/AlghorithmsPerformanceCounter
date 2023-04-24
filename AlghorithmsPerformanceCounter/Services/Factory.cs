using AlghorithmsPerformanceCounter.Models.Algorithms;
using AlghorithmsPerformanceCounter.Models.Algorithms.PerformancesCounting;
using AlghorithmsPerformanceCounter.Models.ArrayInitializers;
using System;
using System.Collections.Generic;

namespace AlghorithmsPerformanceCounter.Services
{
    public static class Factory
	{
		// List of sorting algorithms to be used in calculations
		public static List<AbstractSortingAlgorithm> AllSortingAlgorithmsList = new List<AbstractSortingAlgorithm>()
		{
			CreateMergeSorter,
			CreateQuicksort,
			CreateBubleSorter,
			CreateInsertionSorter,
			CreateHeapSorter,
			CreateLinqSort
		};
		public static IArrayInitializer CreateArrayInitializer => new ArrayInitializer();


		//Creators for all available algorithms
		#region Sorting algorithms
		public static AbstractSortingAlgorithm CreateQuicksort => new Quicksort();
		public static AbstractSortingAlgorithm CreateBubleSorter => new BubleSort();
		public static AbstractSortingAlgorithm CreateMergeSorter => new MergeSort();
		public static AbstractSortingAlgorithm CreateInsertionSorter => new InsertionSort();
		public static AbstractSortingAlgorithm CreateHeapSorter => new Heapsort();
		public static AbstractSortingAlgorithm CreateLinqSort => new LinqSort();

		#endregion

		public static IAllAlgorithmsPerformanceCounter CreateAllAlgorithmsSorter => new AllAlgorithmsPerformance(AllSortingAlgorithmsList);
		public static IAlgorithmPerformanceCounter CreatePerformanceCounter(string algorithmName)
		{
			return new PerformancesCounter(algorithmName);
		}
	}
}
