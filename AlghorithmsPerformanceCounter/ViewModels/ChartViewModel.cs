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

namespace AlghorithmsPerformanceCounter.ViewModels
{
    public class ChartViewModel
    {
		private MainViewModel _mainWindowViewModel;
		IAllAlgorithmsPerformanceCounter multiAlgorithmsSorter  => Factory.CreateAllAlgorithmsSorter;
		public Task<ObservableCollection<ObservableCollection<IAlgorithmPerformanceCounter>>> SortingPerformanceForAllArraysAndAlgorithms { get; } // second array is for each array scan first is for algorithms used
		public int[][] ArraySizes;
		ObservableCollection<string> _algorithmsNames = new ObservableCollection<string>();
		public async Task<ObservableCollection<string>> GetAlgorithmsNamesAsync()
		{
			if (_algorithmsNames.Count == 0)
			{
				var sortingPerformanceForAllArraysAndAlgorithms = await SortingPerformanceForAllArraysAndAlgorithms;
				foreach (var collection in sortingPerformanceForAllArraysAndAlgorithms)
				{
					_algorithmsNames.Add(collection[0].AlgorithmName);
				}
			}
			return _algorithmsNames;
		}
		public ChartViewModel(MainViewModel mainWindowViewModel)
		{
			_mainWindowViewModel = mainWindowViewModel;
			ArraySizes = mainWindowViewModel.MultipleArrays;
			SortingPerformanceForAllArraysAndAlgorithms = multiAlgorithmsSorter.SortMultipleArrays(_mainWindowViewModel.MultipleArrays);
		}
	}
}
