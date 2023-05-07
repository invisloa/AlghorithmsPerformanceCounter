using AlghorithmsPerformanceCounter.Models;
using AlghorithmsPerformanceCounter.Services;
using AlghorithmsPerformanceCounter.ViewModels;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AlghorithmsPerformanceCounter
{
	public partial class ChartView : UserControl
	{

		public ChartView(ChartViewModel chartViewModel)
		{
			InitializeComponent();
			DataContext = chartViewModel;
			PopulateArraysSizesTable();
			_ = PopulatePerformancesTableAsync();
			_ = PopulateChartsAsync();

			chartViewModel.NavigateBackToMainViewCommand = new RelayCommand(param => chartViewModel.NavigateBackToMainView());

		}
		void PopulateArraysSizesTable()
		{
			var chartViewModel = DataContext as ChartViewModel;
			var firstEmptyColumn = new DataGridTextColumn() { Header = "", Width = 128 };
			ArraySizeTable.Columns.Add(firstEmptyColumn);
			for (int arrayIndex = 0; arrayIndex < chartViewModel.ArraySizes.Length; arrayIndex++)
			{
				var newColumn = new DataGridTextColumn();
				newColumn.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
				newColumn.Header = $"Array size {chartViewModel.ArraySizes[arrayIndex].Length}";
				ArraySizeTable.Columns.Add(newColumn);
			}
		}

		public event EventHandler NavigateBackToMainView;
		private void BackToMainViewButton_Click(object sender, RoutedEventArgs e)
		{
			NavigateBackToMainView?.Invoke(this, EventArgs.Empty);
		}

		async Task PopulatePerformancesTableAsync()
		{
			var chartViewModel = DataContext as ChartViewModel;
			var algorithmsName = new DataGridTextColumn() { Header = "Algorithms name", Width = 125, Binding = new Binding("AlgorithmName") };
			PerformancesTable.Columns.Add(algorithmsName);
			for (int arrayIndex = 0; arrayIndex < chartViewModel.ArraySizes.Length; arrayIndex++)
			{
				var actionsColumn = new DataGridTextColumn() { Binding = new Binding($"Actions[{arrayIndex}]") };
				actionsColumn.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
				actionsColumn.Header = $"Actions";
				PerformancesTable.Columns.Add(actionsColumn);
				var timeColumn = new DataGridTextColumn() { Binding = new Binding($"Time[{arrayIndex}]") };
				timeColumn.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
				timeColumn.Header = $"Time (ms)";
				PerformancesTable.Columns.Add(timeColumn);
			}
			// Set the ItemsSource for the PerformancesTable DataGrid
			PerformancesTable.ItemsSource = await chartViewModel.AlgorithmPerformanceRows;
		}
		async Task PopulateChartsAsync()
		{
			var chartViewModel = DataContext as ChartViewModel;
			var arraySizesLabels = chartViewModel.ArraySizes.Select(array => array.Length.ToString()).ToList();
			chart1.AxisX.Add(new LiveCharts.Wpf.Axis
			{
				Title = "Array Sizes",
				Labels = arraySizesLabels,
			});
			chart1.AxisY.Add(new LiveCharts.Wpf.Axis
			{
				Title = "Time Complexity",
				LabelFormatter = value => value.ToString()
			});
			chart1.LegendLocation = LiveCharts.LegendLocation.Right;
			chart1.Series.Clear();
			SeriesCollection series = new SeriesCollection();
			var algorithmNames = chartViewModel.AlgorithmsNames;
			var sortingPerformanceForAllArraysAndAlgorithms = await chartViewModel.SortingPerformanceForAllArraysAndAlgorithms;

			for (int i = 0; i < algorithmNames.Count; i++)
			{
				List<long> values = new List<long>();
				for (int j = 0; j < arraySizesLabels.Count; j++)
				{
					var data = sortingPerformanceForAllArraysAndAlgorithms[i][j].Stopwatch.ElapsedTicks;
					values.Add(data);
				}
				series.Add(new LineSeries()
				{
					Title = algorithmNames[i],
					Values = new ChartValues<long>(values)
				});
			}
			chart1.Series = series;
		}
	}
}
