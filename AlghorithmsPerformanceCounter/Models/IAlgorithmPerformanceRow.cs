using System.Collections.Generic;

namespace AlghorithmsPerformanceCounter.Models
{
	public interface IAlgorithmPerformanceRow
	{
		List<long> Actions { get; set; }
		string AlgorithmName { get; set; }
		List<double> Time { get; set; }
	}
}