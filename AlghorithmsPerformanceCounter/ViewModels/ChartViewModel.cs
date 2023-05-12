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
using System.Windows.Input;
using AlghorithmsPerformanceCounter.Models.Algorithms;

namespace AlghorithmsPerformanceCounter.ViewModels
{
    public class ChartViewModel
    {
		private int[][] arraySizes;
		public int[][] ArraySizes { get => arraySizes; set => arraySizes = value; }

		private MainViewModel mainWindowViewModel;
		public Task<ObservableCollection<ObservableCollection<IAlgorithmPerformanceCounter>>> SortingPerformanceForAllArraysAndAlgorithms { get; } // second array is for each array perfomances score first is for algorithm used
		public  Task<List<IAlgorithmPerformanceRow>> AlgorithmPerformanceRows { get => GeneratePerformanceRowsAsync(); }
		ObservableCollection<string> algorithmsNames = new ObservableCollection<string>();
		public Action NavigateBackToMainView { get; set; }
		public ICommand NavigateBackToMainViewCommand { get; set; }

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

			var EFMSSQLAlgorithmSelection = mainWindowViewModel.AlgorithmSelections.FirstOrDefault(a => a.Algorithm.ToString() == "EFMSSQL");
			if (EFMSSQLAlgorithmSelection?.IsSelected == true)
			{
				StartFreshDatabase();
			}
			// sort all arrays by all algorithms
			SortingPerformanceForAllArraysAndAlgorithms = multiAlgorithmsSorter.SortAllAlgorithmsPerformances(this.mainWindowViewModel.MultipleArrays);
			_ = SetAlgorithmsNamesAsync();
		}
		private async Task<List<IAlgorithmPerformanceRow>> GeneratePerformanceRowsAsync()
		{
			var sortingPerformance = await SortingPerformanceForAllArraysAndAlgorithms;
			var performanceRows = new List<IAlgorithmPerformanceRow>();

			for (int i = 0; i < sortingPerformance.Count; i++)
			{
				var row = Factory.CreateAlgorithmPerformanceRow;
				row.AlgorithmName = sortingPerformance[i][0].AlgorithmName;    // second array is for each array perfomances first is for algorithm used
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
		private void StartFreshDatabase()
		{
			// CREATE Fresh DATABASE AND DELETE Old DATABASE
			using (var db = new NumberDbContext())
			{
				// Delete the existing database and create a new one
				db.Database.EnsureDeleted();
				db.Database.EnsureCreated();
				for (int i = 0; i < ArraySizes.Length; i++)
				{
					foreach (var num in ArraySizes[i])
					{
						db.Numbers.Add(new Number { Value = num, ArrayId = i + 1 });  // ArrayId is i+1
					}
				}
				db.SaveChanges();  // Saves all changes to the database
			}
		}

	}
}
