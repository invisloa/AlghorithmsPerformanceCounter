using AlghorithmsPerformanceCounter.Models.Algorithms;
using AlghorithmsPerformanceCounter.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlghorithmsPerformanceCounter.Services
{
	public class ArraySorterPerformanceCounter : IArraySorterPerformanceCounter
	{
		int numberOfArrays;
		int numberOfValuesInArray;
		public int TotalNumberOfArraysToSort => numberOfArrays;
		public int TotalNumberOfValuesInArray => numberOfValuesInArray;

		public ArraySorterPerformanceCounter(List<AbstractSortingAlgorithm> sortingAlgorithmsList)
		{
			this.sortingAlgorithmsList = sortingAlgorithmsList;

		}
		List<AbstractSortingAlgorithm> sortingAlgorithmsList;
		public List<AbstractSortingAlgorithm> AllUsedAlgoritms => sortingAlgorithmsList;


		public void SortMultipleArrays(int[][] arraysToSort)
		{
			numberOfArrays = arraysToSort.Length;
			numberOfValuesInArray = arraysToSort[0].Length;
			foreach (AbstractSortingAlgorithm item in sortingAlgorithmsList)
			{
				foreach (int[] array in arraysToSort)
				{
					item.SortArray(array);
				}
			}
		}
	}
}
