using AlghorithmsPerformanceCounter.Models;
using AlghorithmsPerformanceCounter.Models.Algorithms.PerformancesCounting;
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
		private ScrollViewer _arraySizeTableScrollViewer;
		private ScrollViewer _performancesTableScrollViewer;
		private ChartView(ChartViewModel chartViewModel)
		{
			InitializeComponent();
			DataContext = chartViewModel;
			PopulateArraysSizesTable();
			ArraySizeItemsControl.Loaded += ArraySizeTable_Loaded;
			PerformancesTable.Loaded += PerformancesTable_Loaded;
			chartViewModel.NavigateBackToMainViewCommand = new RelayCommand(param => chartViewModel.NavigateBackToMainView());
		}
		public static async Task<ChartView> CreateAsync(ChartViewModel chartViewModel)
		{
			var chartView = new ChartView(chartViewModel);
			await chartView.InitializeAsync();
			return chartView;
		}
		private async Task InitializeAsync()
		{

			await PopulatePerformancesTableAsync();
			await PopulateActionsChartsAsync();
			await PopulateTimeChartsAsync();

		}
		void PopulateArraysSizesTable()
		{
			var chartViewModel = DataContext as ChartViewModel;
			foreach (var arraySize in chartViewModel.ArraySizes)
			{
				ArraySizeItemsControl.Items.Add(arraySize.Length);
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

			// Define styles
			var normalStyle = new Style(typeof(DataGridCell));
			var alternateStyle = new Style(typeof(DataGridCell));
			alternateStyle.Setters.Add(new Setter { Property = BackgroundProperty, Value = Brushes.Gray });

			var algorithmsName = new DataGridTextColumn() { Header = "Algorithms name", MinWidth = 143, Width = 143, Binding = new Binding("AlgorithmName") };
			PerformancesTable.Columns.Add(algorithmsName);

			for (int arrayIndex = 0; arrayIndex < chartViewModel.ArraySizes.Length; arrayIndex++)
			{
				var actionsColumn = new DataGridTextColumn() { MinWidth = 75, Binding = new Binding($"Actions[{arrayIndex}]") };
				actionsColumn.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
				actionsColumn.Header = $"Actions";
				actionsColumn.CellStyle = alternateStyle;
				PerformancesTable.Columns.Add(actionsColumn);

				var timeColumn = new DataGridTextColumn() { MinWidth = 75, Binding = new Binding($"Time[{arrayIndex}]") };
				timeColumn.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
				timeColumn.Header = $"Time (ms)";
				PerformancesTable.Columns.Add(timeColumn);
			}
			// Set the ItemsSource for the PerformancesTable DataGrid
			PerformancesTable.ItemsSource = await chartViewModel.AlgorithmPerformanceRows;
		}

		async Task PopulateTimeChartsAsync()
		{
			await PopulateChartAsync(TimeComplexityChart, "Time Complexity", performance => performance.Stopwatch.ElapsedTicks);
		}

		async Task PopulateActionsChartsAsync()
		{
			await PopulateChartAsync(ActionsCountChart, "Actions Count", performance => performance.ActionsTaken);
		}
		async Task PopulateChartAsync(LiveCharts.Wpf.CartesianChart chart, string yAxisTitle, Func<IAlgorithmPerformanceCounter, long> getValue)
		{
			var chartViewModel = DataContext as ChartViewModel;
			var arraySizesLabels = chartViewModel.ArraySizes.Select(array => array.Length.ToString()).ToList();

			chart.AxisX.Clear();
			chart.AxisX.Add(new LiveCharts.Wpf.Axis
			{
				Title = "Array Sizes",
				Labels = arraySizesLabels,
			});

			chart.AxisY.Clear();
			chart.AxisY.Add(new LiveCharts.Wpf.Axis
			{
				Title = yAxisTitle,
				LabelFormatter = value => value.ToString()
			});

			chart.LegendLocation = LiveCharts.LegendLocation.Right;
			chart.Series.Clear();
			SeriesCollection series = new SeriesCollection();
			var algorithmNames = chartViewModel.AlgorithmsNames;
			var sortingPerformanceForAllArraysAndAlgorithms = await chartViewModel.SortingPerformanceForAllArraysAndAlgorithms;

			for (int i = 0; i < algorithmNames.Count; i++)
			{
				List<long> values = new List<long>();
				for (int j = 0; j < arraySizesLabels.Count; j++)
				{
					var data = getValue(sortingPerformanceForAllArraysAndAlgorithms[i][j]);
					values.Add(data);
				}
				series.Add(new LineSeries()
				{
					Title = algorithmNames[i],
					Values = new ChartValues<long>(values),
					PointGeometry = DefaultGeometries.None
				});
			}
			chart.Series = series;
		}

		//Synchronize ScrollBars REGION
		#region Synchronize ScrollBars
		//ScrollSynchronization helper methods
		private void ArraySizeTable_Loaded(object sender, RoutedEventArgs e)
		{
			// Find and store the ScrollViewer for the DataGrid.
			_arraySizeTableScrollViewer = GetScrollViewer(ArraySizeScrollViewer);

			// Add ScrollChanged event handler.
			_arraySizeTableScrollViewer.ScrollChanged += ArraySizeTable_ScrollChanged;
		}

		private void PerformancesTable_Loaded(object sender, RoutedEventArgs e)
		{
			// Find and store the ScrollViewer for the DataGrid.
			_performancesTableScrollViewer = GetScrollViewer(PerformancesTable);

			// Add ScrollChanged event handler.
			_performancesTableScrollViewer.ScrollChanged += PerformancesTable_ScrollChanged;
		}

		private void ArraySizeTable_ScrollChanged(object sender, ScrollChangedEventArgs e)
		{
			_performancesTableScrollViewer.ScrollToHorizontalOffset(e.HorizontalOffset);
		}

		private void PerformancesTable_ScrollChanged(object sender, ScrollChangedEventArgs e)
		{
			_arraySizeTableScrollViewer.ScrollToHorizontalOffset(e.HorizontalOffset);
		}

		// This helper method finds the ScrollViewer within the DataGrid control template.
		private ScrollViewer GetScrollViewer(DependencyObject depObj)
		{
			if (depObj is ScrollViewer) return depObj as ScrollViewer;

			for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
			{
				var child = VisualTreeHelper.GetChild(depObj, i);

				var result = GetScrollViewer(child);
				if (result != null) return result;
			}
			return null;
		}
		#endregion
	}
}
