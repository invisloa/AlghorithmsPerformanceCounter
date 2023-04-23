using AlghorithmsPerformanceCounter.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
		}
		public event EventHandler NavigateBackToMainView;
		private void BackToMainViewButton_Click(object sender, RoutedEventArgs e)
		{
			NavigateBackToMainView?.Invoke(this, EventArgs.Empty);
		}
	}
}
