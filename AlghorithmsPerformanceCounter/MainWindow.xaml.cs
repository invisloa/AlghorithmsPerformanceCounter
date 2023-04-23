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
			var mainWindowViewModel = DataContext as MainViewModel;                              // XXXXXXXXXXXXXXXXXXXXXXXXXXXXX ERROR
			var chartViewModel = new ChartViewModel(mainWindowViewModel);

			var chartView = new ChartView(chartViewModel);
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
