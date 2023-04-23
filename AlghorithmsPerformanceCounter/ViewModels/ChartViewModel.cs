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

namespace AlghorithmsPerformanceCounter.ViewModels
{
    public class ChartViewModel
    {
		private MainViewModel _mainWindowViewModel;
		IAllAlgorithmsPerformanceCounter multiAlgorithmsSorter { get => Factory.CreateMultiAlgorithmsSorter; }
		ObservableCollection<ObservableCollection<IAlgorithmPerformanceCounter>> sortingPerformance;

		public ChartViewModel(MainViewModel mainWindowViewModel)
		{
			_mainWindowViewModel = mainWindowViewModel;

			sortingPerformance = multiAlgorithmsSorter.SortMultipleArrays(_mainWindowViewModel.MultipleArrays);
		}
	}
}
