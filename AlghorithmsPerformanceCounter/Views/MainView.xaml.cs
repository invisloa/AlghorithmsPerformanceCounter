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

		private void NavigateToChartViewButton_Click_1(object sender, RoutedEventArgs e)
		{
			RefreshSign.Visibility = Visibility.Visible;
		}
	}
}
