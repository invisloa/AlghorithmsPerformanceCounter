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

		public ArraySorterPerformanceCounter(List<IAlgorithmPerformanceCounter> sortingAlgorithmsList)
		{
			this.sortingAlgorithmsList = sortingAlgorithmsList;

		}
		List<IAlgorithmPerformanceCounter> sortingAlgorithmsList;
		public List<IAlgorithmPerformanceCounter> AllUsedAlgoritms => sortingAlgorithmsList;


		public void SortMultipleArrays(int[][] arraysToSort)
		{
			numberOfArrays = arraysToSort.Length;
			numberOfValuesInArray = arraysToSort[0].Length;
			foreach (IAlgorithmPerformanceCounter item in sortingAlgorithmsList)
			{
				foreach (int[] array in arraysToSort)
				{
					item.ResetPerformance();
					item.SortArray(array);
				}
			}
		}
	}
}
