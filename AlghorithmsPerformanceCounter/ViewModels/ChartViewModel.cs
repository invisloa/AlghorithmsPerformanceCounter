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
		ObservableCollection<string> algorithmsNames = new ObservableCollection<string>();
		public  Task<List<AlgorithmPerformanceRow>> AlgorithmPerformanceRows { get => GeneratePerformanceRowsAsync(); }
		public async Task<ObservableCollection<string>> GetAlgorithmsNamesAsync()
		{
			if (algorithmsNames.Count == 0)
			{
				var sortingPerformanceForAllArraysAndAlgorithms = await SortingPerformanceForAllArraysAndAlgorithms;
				foreach (var collection in sortingPerformanceForAllArraysAndAlgorithms)
				{
					algorithmsNames.Add(collection[0].AlgorithmName);
				}
			}
			return algorithmsNames;
		}
		IAllAlgorithmsPerformanceCounter multiAlgorithmsSorter => Factory.CreateAllAlgorithmsSorter;
		public ChartViewModel(MainViewModel mainWindowViewModel)
		{
			this.mainWindowViewModel = mainWindowViewModel;
			ArraySizes = mainWindowViewModel.MultipleArrays;
			SortingPerformanceForAllArraysAndAlgorithms = multiAlgorithmsSorter.SortMultipleArrays(this.mainWindowViewModel.MultipleArrays);
		}
		private async Task<List<AlgorithmPerformanceRow>> GeneratePerformanceRowsAsync()
		{
			var sortingPerformance = await SortingPerformanceForAllArraysAndAlgorithms;
			var performanceRows = new List<AlgorithmPerformanceRow>();

			for (int i = 0; i < sortingPerformance.Count; i++)
			{
				var row = new AlgorithmPerformanceRow
				{
					AlgorithmName = sortingPerformance[i][0].AlgorithmName,    // second array is for each array scan first is for algorithms used
					Actions = new List<long>(),
					Time = new List<double>()
				};

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
