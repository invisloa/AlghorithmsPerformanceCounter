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
			Width = 1200;
			Height = 850;
			mainViewModel.NavigateToChartView = async () =>
			{
				mainViewModel.IsSpinnerActive = true;

				var chartViewModel = await ChartViewModel.CreateAsync(mainViewModel);
				ChartView chartView = await ChartView.CreateAsync(chartViewModel);
				
				chartViewModel.NavigateBackToMainView = () => MainContentControl.Content = new MainView { DataContext = mainViewModel };
				MainContentControl.Content = chartView;
				mainViewModel.IsSpinnerActive = false;

			};


			MainContentControl.Content = new MainView { DataContext = mainViewModel };
		}
	}
}
