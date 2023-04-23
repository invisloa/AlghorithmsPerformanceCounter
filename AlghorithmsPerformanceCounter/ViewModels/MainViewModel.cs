﻿using AlghorithmsPerformanceCounter.Models.ArrayInitializers;
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
		private const int MaxValuesPerArray = 10000;
		private int _numberOfArrays =2;
		private int _numberOfValuesPerArray =411;
		IArrayInitializer arrayInitializer = Factory.CreateArrayInitializer;
		public int[][] MultipleArrays { get => arrayInitializer.InitializeMultipleArrays(_numberOfArrays, _numberOfValuesPerArray); }
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
		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged(string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
