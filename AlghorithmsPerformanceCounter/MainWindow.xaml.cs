using AlghorithmsPerformanceCounter.ViewModels;
using System;
using System.Windows;

namespace AlghorithmsPerformanceCounter
{
	public partial class MainWindow : Window
	{


		public MainWindow()
		{
			var mainViewModel = new MainViewModel();
			DataContext = mainViewModel;
			InitializeComponent();

			mainViewModel.NavigateToChartView = () =>
			{
				var chartViewModel = new ChartViewModel(mainViewModel);
				var chartView = new ChartView(chartViewModel);
				chartViewModel.NavigateBackToMainView = () => MainContentControl.Content = new MainView { DataContext = mainViewModel };
				MainContentControl.Content = chartView;
			};

			MainContentControl.Content = new MainView { DataContext = mainViewModel };
		}
	}
}
