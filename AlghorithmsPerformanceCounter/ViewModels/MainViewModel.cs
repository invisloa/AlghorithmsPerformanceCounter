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
using System.Windows.Input;

namespace AlghorithmsPerformanceCounter.ViewModels
{
	public class MainViewModel : INotifyPropertyChanged
	{
		private const int MinValuesPerArray = 1;
		private const int MaxValuesPerArray = 1000000;
		private int _arrayIncementFactor = 2;
		private int _numberOfValuesPerArray = 1000;
		IArrayInitializer arrayInitializer = Factory.CreateArrayInitializer;
		public event PropertyChangedEventHandler PropertyChanged;
		public int[][] MultipleArrays { get => arrayInitializer.InitializeMultipleArrays(_arrayIncementFactor, _numberOfValuesPerArray); }
		public List<AlgorithmSelection> AlgorithmSelections { get; }

		// RelayCommands
		#region Commands
		private ICommand _navigateToChartViewCommand;
public ICommand NavigateToChartViewCommand => _navigateToChartViewCommand ??= new AsyncRelayCommand(async () => await NavigateToChartView(), () => true);

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
					_numberOfValuesPerArray = Math.Clamp(value, MinValuesPerArray, MaxValuesPerArray);
					OnPropertyChanged(nameof(NumberOfValuesPerArray));
				}
			}
		}

		public MainViewModel()
		{
			AlgorithmSelections = Factory.AllSortingAlgorithmsList.Select(alg => new AlgorithmSelection { Algorithm = alg, IsSelected = true }).ToList();
		}
		protected virtual void OnPropertyChanged(string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
