using AlghorithmsPerformanceCounter.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;

namespace AlghorithmsPerformanceCounter
{
	public partial class MainView : UserControl
	{
		public event EventHandler NavigateToChartView;
		public MainView()
		{
			InitializeComponent();
			DataContext = new MainViewModel();
		}

		private void NumberOfRandomValuesInput_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
		{
			if (!int.TryParse(e.Text, out _))
			{
				e.Handled = true;
			}
		}

		private void NavigateToChartViewButton_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			// Get the MainViewModel object from the DataContext
			var mainWindowViewModel = DataContext as MainViewModel;
			if (mainWindowViewModel == null)
			{
				throw new InvalidOperationException("DataContext is not set to a MainViewModel object.");
			}

			// Create a new instance of the ChartViewModel and set its MainViewModel property to the current instance
			var chartViewModel = new ChartViewModel(mainWindowViewModel);

			// Create a new instance of the ChartView and set its DataContext to the ChartViewModel
			var chartView = new ChartView(chartViewModel);

			// Raise the NavigateToChartView event with the ChartView as the sender
			NavigateToChartView?.Invoke(chartView, EventArgs.Empty);
		}
	}
}
