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
		IAllAlgorithmsPerformanceCounter multiAlgorithmsSorter  => Factory.CreateAllAlgorithmsSorter;
		public ObservableCollection<ObservableCollection<IAlgorithmPerformanceCounter>> SortingPerformance { get; }
		public int[][] MultipleArrays;



		public ChartViewModel(MainViewModel mainWindowViewModel)
		{
			_mainWindowViewModel = mainWindowViewModel;
			MultipleArrays = mainWindowViewModel.MultipleArrays;
			SortingPerformance = multiAlgorithmsSorter.SortMultipleArrays(_mainWindowViewModel.MultipleArrays);
		}
	}
}
