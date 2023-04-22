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
	internal class MainWindowViewModel : INotifyPropertyChanged
	{
		private int _numberOfArrays;
		IArrayInitializer arrayInitializer = Factory.CreateArrayInitializer;
		IArraySorterPerformanceCounter multiAlgorithmsSorter = Factory.CreateMultiAlgorithmsSorter;
		public int[][] MultipleArrays { get => arrayInitializer.InitializeMultipleArrays(); }

		public MainWindowViewModel()
		{

		}

		public int NumberOfArrays
		{
			get { return _numberOfArrays; }
			set
			{
				if (_numberOfArrays != value)
				{
					_numberOfArrays = value;
					OnPropertyChanged(nameof(NumberOfArrays));
				}
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}




	}
}
