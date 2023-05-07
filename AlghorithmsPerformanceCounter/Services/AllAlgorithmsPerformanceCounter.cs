using AlghorithmsPerformanceCounter.Models.Algorithms;
using AlghorithmsPerformanceCounter.Models.Algorithms.PerformancesCounting;
using AlghorithmsPerformanceCounter.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlghorithmsPerformanceCounter.Services
{
	public class AllAlgorithmsPerformance : IAllAlgorithmsPerformanceCounter
	{
		int numberOfArrays;
		int numberOfValuesInArray;
	
		public int TotalNumberOfArraysToSort => numberOfArrays;
		public int TotalNumberOfValuesInArray => numberOfValuesInArray;

		public AllAlgorithmsPerformance(List<AbstractSortingAlgorithm> sortingAlgorithmsList)
		{
			this.sortingAlgorithmsList = sortingAlgorithmsList;
		}
		List<AbstractSortingAlgorithm> sortingAlgorithmsList;
		public List<AbstractSortingAlgorithm> AllUsedAlgoritms => sortingAlgorithmsList;


		public async Task<ObservableCollection<ObservableCollection<IAlgorithmPerformanceCounter>>> SortMultipleArrays(int[][] arraysToSort)
		{
			var allAlgorithmsScores = new ObservableCollection<ObservableCollection<IAlgorithmPerformanceCounter>>();
			numberOfArrays = arraysToSort.Length;
			numberOfValuesInArray = arraysToSort[0].Length;
			foreach (AbstractSortingAlgorithm item in sortingAlgorithmsList)
			{
				allAlgorithmsScores.Add(await item.SortMultipleArrays(arraysToSort));
			}
			return allAlgorithmsScores;
		}
	}
}
