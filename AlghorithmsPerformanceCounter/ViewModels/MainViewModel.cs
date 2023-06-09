﻿
using AlghorithmsPerformanceCounter.Models;
using AlghorithmsPerformanceCounter.Models.Algorithms;
using AlghorithmsPerformanceCounter.Models.ArrayInitializers;
using AlghorithmsPerformanceCounter.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AlghorithmsPerformanceCounter.ViewModels
{
	public class MainViewModel : INotifyPropertyChanged
	{
		private const int MinValuesPerArray = 1;
		private const int MaxValuesPerArray = 1000000;
		private int _arrayIncementFactor = 2;
		private int _mainArraySize = 1000;
		private bool _isSpinnerActive = false;
		IArrayInitializer arrayInitializer;
		public event PropertyChangedEventHandler PropertyChanged;
		public int[][] MultipleArrays { get => arrayInitializer.InitializeMultipleArrays(_arrayIncementFactor, _mainArraySize); }
		public List<AlgorithmSelection> AlgorithmSelections { get; }

		public bool IsSpinnerActive 
		{
			get => _isSpinnerActive;
			set
			{
				_isSpinnerActive = value;
				SpinnerVisibility = value ? Visibility.Visible : Visibility.Collapsed;
				OnPropertyChanged(nameof(IsSpinnerActive));
			}
		}
		private Visibility _spinnerVisibility = Visibility.Collapsed;

		public Visibility SpinnerVisibility
		{
			get => _spinnerVisibility;
			set
			{
				_spinnerVisibility = value;
				OnPropertyChanged(nameof(SpinnerVisibility));
			}
		}
		public List<IArrayInitializer> ArrayInitializers { get; }
		public IArrayInitializer SelectedArrayInitializer
		{
			get => arrayInitializer;
			set
			{
				if (value != null)
				{
					arrayInitializer = value;
					OnPropertyChanged(nameof(SelectedArrayInitializer));
					OnPropertyChanged(nameof(MultipleArrays));
				}
			}
		}
		// RelayCommands
		#region Commands
		private ICommand _navigateToChartViewCommand;
		public ICommand NavigateToChartViewCommand => _navigateToChartViewCommand ??= new RelayCommand(_ => NavigateToChartView(), _ => true);

		private ICommand _navigateBackToMainViewCommand;
		public ICommand NavigateBackToMainViewCommand => _navigateBackToMainViewCommand ??= new RelayCommand(_ => NavigateBackToMainView(), _ => true);

		public Action NavigateToChartView { get; set; }
		public Action NavigateBackToMainView { get; set; }
		#endregion

		public int ArrayIncrementFactor
		{
			get { return _arrayIncementFactor; }
			set
			{
				if (_arrayIncementFactor != value)
				{
					_arrayIncementFactor = value;
					if(_arrayIncementFactor > MainArraySize)
					{
						MainArraySize = _arrayIncementFactor;
						OnPropertyChanged(nameof(MainArraySize));
					}
					OnPropertyChanged(nameof(ArrayIncrementFactor));
				}
			}
		}
		public int MainArraySize
		{
			get { return _mainArraySize; }
			set
			{
				if (_mainArraySize != value)
				{
					_mainArraySize = Math.Clamp(value, MinValuesPerArray, MaxValuesPerArray);
					if (value < _arrayIncementFactor)
					{
						_arrayIncementFactor = Math.Min(_mainArraySize, _arrayIncementFactor);
					}
					OnPropertyChanged(nameof(MainArraySize));
					OnPropertyChanged(nameof(ArrayIncrementFactor));
				}
			}
		}


		public MainViewModel()
		{
			AlgorithmSelections = Factory.AllSortingAlgorithmsList.Select(alg => new AlgorithmSelection { Algorithm = alg, IsSelected = true }).ToList();
			var efmssqlAlgorithmSelection = AlgorithmSelections.FirstOrDefault(alg => alg.Algorithm.Name == "EFMSSQL");
			if (efmssqlAlgorithmSelection != null)
			{
				efmssqlAlgorithmSelection.IsSelected = false;
			}
			var permutationAlgorithmSelection = AlgorithmSelections.FirstOrDefault(alg => alg.Algorithm.Name == "Permutation Sort");
			if (permutationAlgorithmSelection != null)
			{
				permutationAlgorithmSelection.IsSelected = false;
			}
			var linqAlgorithmSelection = AlgorithmSelections.FirstOrDefault(alg => alg.Algorithm.Name == "Linq Sort");
			if(linqAlgorithmSelection!= null)
			{
				linqAlgorithmSelection.IsSelected = false;
			}

				ArrayInitializers = new List<IArrayInitializer>
				{
					Factory.CreateArrayInitializerRandom,
					Factory.CreateArrayInitializerWorstCase,
					Factory.CreateArrayInitializerBestCase,
				};
				SelectedArrayInitializer = ArrayInitializers[0];
		}
		protected virtual void OnPropertyChanged(string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
