using AlghorithmsPerformanceCounter.Models.Algorithms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlghorithmsPerformanceCounter.Models
{
	public class AlgorithmSelection
	{
		public AbstractSortingAlgorithm Algorithm { get; set; }
		public bool IsSelected { get; set; }
	}
}
