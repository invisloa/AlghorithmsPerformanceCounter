using AlghorithmsPerformanceCounter.Models;
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
			// Set the DataContext to the passed-in ChartViewModel
			DataContext = chartViewModel;
			PopulateArraysSizesTable();
			PopulatePerformancesTable();




			chart1.AxisX.Add(new LiveCharts.Wpf.Axis
			{
				Title = "Array Sizes",
				Labels = chartViewModel.ArraySizes,
			});
			chart1.AxisY.Add(new LiveCharts.Wpf.Axis
			{
				Title = "Time Complexity",
				LabelFormatter = value => value.ToString()
			});
			chart1.LegendLocation = LiveCharts.LegendLocation.Right;
			chart1.Series.Clear();
			SeriesCollection series = new SeriesCollection();
			var arrays = chartViewModel.ArraySizes;

			for (int i = 0; i < arrays.Count; i++)
			{
				List<long> values = new List<long>();
				{
					for (int j = 0; j < chartViewModel.AlgorithmsNames.Count; j++)
					{
						var data = chartViewModel.SortingPerformanceForAllArraysAndAlgorithms[j][i].Stopwatch.ElapsedTicks;
						values.Add(data);
					}
				}
				series.Add(new LineSeries() { Title = chartViewModel.ArraySizes.ToString(), Values = new ChartValues<long>(values) });
			}
			chart1.Series = series;
		}









		public event EventHandler NavigateBackToMainView;
		private void BackToMainViewButton_Click(object sender, RoutedEventArgs e)
		{
			NavigateBackToMainView?.Invoke(this, EventArgs.Empty);
		}

		void PopulateArraysSizesTable()
		{
			ArraySizeTable.CanUserResizeColumns = false;
			ArraySizeTable.CanUserReorderColumns = false;
			var chartViewModel = DataContext as ChartViewModel;
			// Get MultipleArrays from the DataContext (ChartViewModel)
			int[][] multipleArrays = chartViewModel.MultipleArrays;
			var firstEmptyColumn = new DataGridTextColumn() { Header = "", Width = 125 };
			ArraySizeTable.Columns.Add(firstEmptyColumn);

			for (int arrayIndex = 0; arrayIndex < multipleArrays.Length; arrayIndex++)
			{
				var newColumn = new DataGridTextColumn();
				newColumn.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
				newColumn.Header = $"Array size {multipleArrays[arrayIndex].Length}";
				// Add the column to the DataGrid
				ArraySizeTable.Columns.Add(newColumn);
			}
		}
		void PopulatePerformancesTable()
		{
			PerformancesTable.CanUserResizeColumns = false;
			PerformancesTable.CanUserReorderColumns = false;
			PerformancesTable.AutoGenerateColumns = false;
			PerformancesTable.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;

			var chartViewModel = DataContext as ChartViewModel;
			// Get MultipleArrays from the DataContext (ChartViewModel)
			int[][] multipleArrays = chartViewModel.MultipleArrays;
			var algorithmsName = new DataGridTextColumn() { Header = "Algorithms name", Width = 125, Binding = new Binding("AlgorithmName") };
			PerformancesTable.Columns.Add(algorithmsName);
			for (int arrayIndex = 0; arrayIndex < multipleArrays.Length; arrayIndex++)
			{
				var actionsColumn = new DataGridTextColumn() { Binding = new Binding($"Actions[{arrayIndex}]") };
				actionsColumn.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
				actionsColumn.Header = $"Actions";
				// Add the column to the DataGrid
				PerformancesTable.Columns.Add(actionsColumn);
				var timeColumn = new DataGridTextColumn() { Binding = new Binding($"Time[{arrayIndex}]") };
				timeColumn.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
				timeColumn.Header = $"Time (ms)";
				// Add the column to the DataGrid
				PerformancesTable.Columns.Add(timeColumn);
			}

			// Set the ItemsSource for the PerformancesTable DataGrid
			PerformancesTable.ItemsSource = GeneratePerformanceRows();
		}
		private List<AlgorithmPerformanceRow> GeneratePerformanceRows()
		{
			var chartViewModel = DataContext as ChartViewModel;
			var sortingPerformance = chartViewModel.SortingPerformanceForAllArraysAndAlgorithms;
			var performanceRows = new List<AlgorithmPerformanceRow>();

			for (int i = 0; i < sortingPerformance.Count; i++)
			{
				var row = new AlgorithmPerformanceRow
				{
					AlgorithmName = sortingPerformance[i][0].AlgorithmName,		// second array is for each array scan first is for algorithms used
					Actions = new List<long>(),
					Time = new List<double>()
				};

				for (int j = 0; j < sortingPerformance[i].Count; j++)
				{
					row.Actions.Add(sortingPerformance[i][j].ActionsTaken);
					row.Time.Add(sortingPerformance[i][j].Stopwatch.ElapsedTicks);
				}

				performanceRows.Add(row);
			}

			return performanceRows;
		}

	}
}
