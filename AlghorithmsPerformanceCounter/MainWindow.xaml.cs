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

			mainViewModel.NavigateToChartView = async () =>
			{
				var chartViewModel = new ChartViewModel(mainViewModel);
				ChartView chartView = await ChartView.CreateAsync(chartViewModel);
				chartViewModel.NavigateBackToMainView = () => MainContentControl.Content = new MainView { DataContext = mainViewModel };
				MainContentControl.Content = chartView;
			};


			MainContentControl.Content = new MainView { DataContext = mainViewModel };
		}
	}
}
