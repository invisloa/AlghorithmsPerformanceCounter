using AlghorithmsPerformanceCounter.Models.ArrayInitializers;
using AlghorithmsPerformanceCounter.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlghorithmsPerformanceCounter.ViewModels
{
    public class ChartViewModel
    {
		private MainViewModel _mainWindowViewModel;
		IArraySorterPerformanceCounter multiAlgorithmsSorter { get => Factory.CreateMultiAlgorithmsSorter; }

		public ChartViewModel(MainViewModel mainWindowViewModel)
		{
			_mainWindowViewModel = mainWindowViewModel;
			multiAlgorithmsSorter.SortMultipleArrays(_mainWindowViewModel.MultipleArrays);

		}


	}
}
