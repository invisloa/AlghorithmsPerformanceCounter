using AlghorithmsPerformanceCounter.Models;
using AlghorithmsPerformanceCounter.Models.Algorithms;
using AlghorithmsPerformanceCounter.Models.Algorithms.PerformancesCounting;
using AlghorithmsPerformanceCounter.Models.ArrayInitializers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlghorithmsPerformanceCounter.Services
{
    public static class Factory
	{
		// List of sorting algorithms to be used in calculations
		public static List<AbstractSortingAlgorithm> AllSortingAlgorithmsList = new List<AbstractSortingAlgorithm>()
		{
			CreateQuicksort,
			CreateBubleSorter,
			CreateInsertionSorter,
			CreateHeapSorter,
			CreateMergeSorter,
			CreateLinqSort,
			CreateEFMSSQL
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
		public static AbstractSortingAlgorithm CreateEFMSSQL => new EFMSSQL();

		#endregion

		public static IAllAlgorithmsPerformanceCounter CreateAllAlgorithmsSorter => new AllAlgorithmsPerformanceCounter(AllSortingAlgorithmsList);
		public static IAllAlgorithmsPerformanceCounter CreateCustomAlgorithmsSorter(List<AlgorithmSelection> algorithmSelections)
		{
			var selectedAlgorithms = algorithmSelections.Where(x => x.IsSelected).Select(x => x.Algorithm).ToList();
			return new AllAlgorithmsPerformanceCounter(selectedAlgorithms);
		}
		public static IAlgorithmPerformanceCounter CreatePerformanceCounter(string algorithmName)
		{
			return new PerformancesCounter(algorithmName);
		}
		public static IAlgorithmPerformanceRow CreateAlgorithmPerformanceRow => new AlgorithmPerformanceRow();
	}
}
