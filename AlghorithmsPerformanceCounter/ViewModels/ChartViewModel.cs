using AlghorithmsPerformanceCounter.Models.ArrayInitializers;
using AlghorithmsPerformanceCounter.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using AlghorithmsPerformanceCounter.Models.Algorithms.PerformancesCounting;
using LiveCharts;
using AlghorithmsPerformanceCounter.Models;

namespace AlghorithmsPerformanceCounter.ViewModels
{
    public class ChartViewModel
    {
		private int[][] arraySizes;
		public int[][] ArraySizes { get => arraySizes; set => arraySizes = value; }


		private MainViewModel mainWindowViewModel;
		public Task<ObservableCollection<ObservableCollection<IAlgorithmPerformanceCounter>>> SortingPerformanceForAllArraysAndAlgorithms { get; } // second array is for each array scan first is for algorithms used
		public  Task<List<IAlgorithmPerformanceRow>> AlgorithmPerformanceRows { get => GeneratePerformanceRowsAsync(); }
		ObservableCollection<string> algorithmsNames = new ObservableCollection<string>();

		public async Task SetAlgorithmsNamesAsync()
		{
			if (algorithmsNames.Count == 0)
			{
				var sortingPerformanceForAllArraysAndAlgorithms = await SortingPerformanceForAllArraysAndAlgorithms;
				foreach (var collection in sortingPerformanceForAllArraysAndAlgorithms)
				{
					algorithmsNames.Add(collection[0].AlgorithmName);
				}
			}
		}

		public ObservableCollection<string> AlgorithmsNames
		{
			get
			{
				_ = SetAlgorithmsNamesAsync();
				return algorithmsNames;
			}
		}
		IAllAlgorithmsPerformanceCounter multiAlgorithmsSorter => Factory.CreateCustomAlgorithmsSorter(mainWindowViewModel.AlgorithmSelections);
		public ChartViewModel(MainViewModel mainWindowViewModel)
		{
			this.mainWindowViewModel = mainWindowViewModel;
			ArraySizes = mainWindowViewModel.MultipleArrays;
			SortingPerformanceForAllArraysAndAlgorithms = multiAlgorithmsSorter.SortMultipleArrays(this.mainWindowViewModel.MultipleArrays);
			_ = SetAlgorithmsNamesAsync();
		}
		private async Task<List<IAlgorithmPerformanceRow>> GeneratePerformanceRowsAsync()
		{
			var sortingPerformance = await SortingPerformanceForAllArraysAndAlgorithms;
			var performanceRows = new List<IAlgorithmPerformanceRow>();

			for (int i = 0; i < sortingPerformance.Count; i++)
			{
				var row = Factory.CreateAlgorithmPerformanceRow;
				row.AlgorithmName = sortingPerformance[i][0].AlgorithmName;    // second array is for each array scan first is for algorithms used
				row.Actions = new List<long>();
				row.Time = new List<double>();

				for (int j = 0; j < sortingPerformance[i].Count; j++)
				{
					row.Actions.Add(sortingPerformance[i][j].ActionsTaken);
					row.Time.Add(sortingPerformance[i][j].Stopwatch.ElapsedTicks);
				}

				performanceRows.Add(row);
			}
			return performanceRows;
		}

	}
}
