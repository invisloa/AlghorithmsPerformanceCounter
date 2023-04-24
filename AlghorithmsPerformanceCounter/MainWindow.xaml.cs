using AlghorithmsPerformanceCounter.ViewModels;
using System;
using System.Windows;

namespace AlghorithmsPerformanceCounter
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			DataContext = new MainViewModel();
			InitializeComponent();
		}

		private void MainContentControl_Loaded(object sender, RoutedEventArgs e)
		{
			var mainView = new MainView();
			mainView.NavigateToChartView += MainView_NavigateToChartView;
			MainContentControl.Content = mainView;
		}
		private void MainView_NavigateToChartView(object sender, EventArgs e)
		{
			var chartView = sender as ChartView;
			var chartViewModel = chartView.DataContext as ChartViewModel;
			chartView.NavigateBackToMainView += ChartView_NavigateBackToMainView;
			MainContentControl.Content = chartView;
		}
		private void ChartView_NavigateBackToMainView(object sender, EventArgs e)
		{
			var mainView = new MainView();
			mainView.NavigateToChartView += MainView_NavigateToChartView;
			MainContentControl.Content = mainView;
		}
	}
}
