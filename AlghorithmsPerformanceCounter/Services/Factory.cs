using AlghorithmsPerformanceCounter.Models.Algorithms;
using AlghorithmsPerformanceCounter.Models.ArrayInitializers;
using System.Collections.Generic;

namespace AlghorithmsPerformanceCounter.Services
{
	public static class Factory
	{
		// List of sorting algorithms to be used in calculations
		public static List<IAlgorithmPerformanceCounter> AllSortingAlgorithmsList = new List<IAlgorithmPerformanceCounter>()
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
		public static IAlgorithmPerformanceCounter CreateQuicksort => new Quicksort();
		public static IAlgorithmPerformanceCounter CreateBubleSorter => new BubleSort();
		public static IAlgorithmPerformanceCounter CreateMergeSorter => new MergeSort();
		public static IAlgorithmPerformanceCounter CreateInsertionSorter => new InsertionSort();
		public static IAlgorithmPerformanceCounter CreateHeapSorter => new Heapsort();
		public static IAlgorithmPerformanceCounter CreateLinqSort => new LinqSort();


		#endregion

		public static IArraySorterPerformanceCounter CreateMultiAlgorithmsSorter => new ArraySorterPerformanceCounter(AllSortingAlgorithmsList);
//		public static ISortAlgorithmsScores CreateSortScoresBeforeWrite => new AlgorithmsScoresSorter();
	}
}
