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
		private async void NavigateToChartViewButton_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			BusyIndicator.Visibility = Visibility.Visible;

			var mainWindowViewModel = DataContext as MainViewModel;
			var chartViewModel = new ChartViewModel(mainWindowViewModel);
			ChartView chartView = await ChartView.CreateAsync(chartViewModel);
			NavigateToChartView?.Invoke(chartView, EventArgs.Empty);
			BusyIndicator.Visibility = Visibility.Collapsed;

		}
	}
}
