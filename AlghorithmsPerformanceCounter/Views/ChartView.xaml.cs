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
		private ScrollViewer _arraySizeTableScrollViewer;
		private ScrollViewer _performancesTableScrollViewer;
		public ChartView(ChartViewModel chartViewModel)
		{
			InitializeComponent();
			DataContext = chartViewModel;
			PopulateArraysSizesTable();
			_ = PopulatePerformancesTableAsync();
			_ = PopulateChartsAsync();


			ArraySizeTable.Loaded += ArraySizeTable_Loaded;
			PerformancesTable.Loaded += PerformancesTable_Loaded; 
			chartViewModel.NavigateBackToMainViewCommand = new RelayCommand(param => chartViewModel.NavigateBackToMainView());

		}
		class DummyRow
		{
			public string Size { get; set; } // Add more properties if needed
		}
		void PopulateArraysSizesTable()
		{

			int numberOfRows = PerformancesTable.Items.Count;
			List<DummyRow> dummyRows = new List<DummyRow>();

			dummyRows.Add(new DummyRow { Size = "" }); // Empty row

			var chartViewModel = DataContext as ChartViewModel;
			var firstEmptyColumn = new DataGridTextColumn() { Header = "", MinWidth= 130 };
			ArraySizeTable.Columns.Add(firstEmptyColumn);
			for (int arrayIndex = 0; arrayIndex < chartViewModel.ArraySizes.Length; arrayIndex++)
			{
				var newColumn = new DataGridTextColumn();
				newColumn.MinWidth = 150;
				newColumn.Header = $"Array size {chartViewModel.ArraySizes[arrayIndex].Length}";
				ArraySizeTable.Columns.Add(newColumn);
			}
			ArraySizeTable.ItemsSource = dummyRows;

		}

		public event EventHandler NavigateBackToMainView;
		private void BackToMainViewButton_Click(object sender, RoutedEventArgs e)
		{
			NavigateBackToMainView?.Invoke(this, EventArgs.Empty);
		}

		async Task PopulatePerformancesTableAsync()
		{
			var chartViewModel = DataContext as ChartViewModel;
			var algorithmsName = new DataGridTextColumn() { Header = "Algorithms name", MinWidth = 125, Binding = new Binding("AlgorithmName") };
			PerformancesTable.Columns.Add(algorithmsName);
			for (int arrayIndex = 0; arrayIndex < chartViewModel.ArraySizes.Length; arrayIndex++)
			{
				var actionsColumn = new DataGridTextColumn() { MinWidth = 75, Binding = new Binding($"Actions[{arrayIndex}]") };
				actionsColumn.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
				actionsColumn.Header = $"Actions";
				PerformancesTable.Columns.Add(actionsColumn);
				var timeColumn = new DataGridTextColumn() { MinWidth=75, Binding = new Binding($"Time[{arrayIndex}]") };
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

		//Synchronize ScrollBars
		#region Synchronize ScrollBars
		//ScrollSynchronization helper methods
		private void ArraySizeTable_Loaded(object sender, RoutedEventArgs e)
		{
			// Find and store the ScrollViewer for the DataGrid.
			_arraySizeTableScrollViewer = GetScrollViewer(ArraySizeTable);

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
