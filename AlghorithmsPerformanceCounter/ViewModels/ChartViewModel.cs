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
		public ObservableCollection<ObservableCollection<IAlgorithmPerformanceCounter>> SortingPerformanceForAllArraysAndAlgorithms { get; } // second array is for each array scan first is for algorithms used
		public int[][] MultipleArrays;
		ObservableCollection<string> _algorithmsNames = new ObservableCollection<string>();
		ObservableCollection<string> _arraySizes = new ObservableCollection<string>();
		public ChartValues<long> TimeComplexity { get; set; }

		public ObservableCollection<string> AlgorithmsNames
		{
			get
			{
				if (_algorithmsNames.Count == 0)
				{
					foreach (ObservableCollection<IAlgorithmPerformanceCounter> collection in SortingPerformanceForAllArraysAndAlgorithms)
					{
						_algorithmsNames.Add(collection[0].AlgorithmName);
					}
				}
				return _algorithmsNames;
			}
		}
		public ObservableCollection<string> ArraySizes
		{
			get
			{
				if (_arraySizes.Count == 0)
				{
					for (int i = 0; i < MultipleArrays.Length; i++)
					{
						_arraySizes.Add(MultipleArrays[i].Length.ToString());
					}
				}
				return _arraySizes;
			}
		}
		public ChartViewModel(MainViewModel mainWindowViewModel)
		{
			_mainWindowViewModel = mainWindowViewModel;
			MultipleArrays = mainWindowViewModel.MultipleArrays;
			SortingPerformanceForAllArraysAndAlgorithms = multiAlgorithmsSorter.SortMultipleArrays(_mainWindowViewModel.MultipleArrays);
		}
	}
}
