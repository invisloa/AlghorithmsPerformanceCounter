﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AlghorithmsPerformanceCounter.Services
{
	internal class RelayCommand : ICommand
	{
		private readonly Action<object> _execute;
		private readonly Func<object, bool> _canExecute;

		public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
		{
			_execute = execute;
			_canExecute = canExecute;
		}

		public bool CanExecute(object? parameter)
		{
			return _canExecute == null || _canExecute(parameter);
		}
		public event EventHandler? CanExecuteChanged;

		public void Execute(object? parameter)
		{
			_execute(parameter);
		}
		public void RaiseCanExecuteChanged()
		{
			CanExecuteChanged?.Invoke(this, EventArgs.Empty);
		}

	}
}
