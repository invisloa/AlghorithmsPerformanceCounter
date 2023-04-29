using AlghorithmsPerformanceCounter.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlghorithmsPerformanceCounter.Models
{
	public class AlgorithmPerformanceRow : IAlgorithmPerformanceRow
	{
		public string AlgorithmName { get; set; }
		public List<long> Actions { get; set; }
		public List<double> Time { get; set; }
	}
}
