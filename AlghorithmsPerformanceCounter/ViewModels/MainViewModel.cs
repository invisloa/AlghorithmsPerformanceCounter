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
		private int _numberOfValuesPerArray = 1000;
		private bool _isSpinnerActive = false; // TO DO TO CHANGE
		IArrayInitializer arrayInitializer;
		public event PropertyChangedEventHandler PropertyChanged;
		public int[][] MultipleArrays { get => arrayInitializer.InitializeMultipleArrays(_arrayIncementFactor, _numberOfValuesPerArray); }
		public List<AlgorithmSelection> AlgorithmSelections { get; }

		public bool IsSpinnerActive // TO DO TO CHANGE
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
				arrayInitializer = value;
				OnPropertyChanged(nameof(SelectedArrayInitializer));
				OnPropertyChanged(nameof(MultipleArrays));
			}
		}













		// RelayCommands
		#region Commands
		private ICommand _navigateToChartViewCommand;
		public ICommand NavigateToChartViewCommand => _navigateToChartViewCommand ??= new RelayCommand(param => NavigateToChartView(), param => true);

		private ICommand _navigateBackToMainViewCommand;
		public ICommand NavigateBackToMainViewCommand => _navigateBackToMainViewCommand ??= new RelayCommand(param => NavigateBackToMainView(), param => true);

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
					OnPropertyChanged(nameof(ArrayIncrementFactor));
					if(_arrayIncementFactor > NumberOfValuesPerArray)
					{
						NumberOfValuesPerArray = _arrayIncementFactor;
					}
				}
			}
		}
		public int NumberOfValuesPerArray
		{
			get { return _numberOfValuesPerArray; }
			set
			{
				if (_numberOfValuesPerArray != value)
				{
					if (_arrayIncementFactor > _numberOfValuesPerArray)
					{
						_numberOfValuesPerArray = _arrayIncementFactor;
					}
					_numberOfValuesPerArray = Math.Clamp(value, MinValuesPerArray, MaxValuesPerArray);
					OnPropertyChanged(nameof(NumberOfValuesPerArray));
				}
			}
		}


		public MainViewModel()
		{
			AlgorithmSelections = Factory.AllSortingAlgorithmsList.Select(alg => new AlgorithmSelection { Algorithm = alg, IsSelected = true }).ToList();
			var efmssqlAlgorithmSelection = AlgorithmSelections.FirstOrDefault(alg => alg.Algorithm.Name == "EFMSSQL");
			if(efmssqlAlgorithmSelection!= null)
			{
				efmssqlAlgorithmSelection.IsSelected = false;
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
