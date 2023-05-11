using AlghorithmsPerformanceCounter.Models.Algorithms.PerformancesCounting;
using AlghorithmsPerformanceCounter.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlghorithmsPerformanceCounter.Models.Algorithms
{
	internal class EFMSSQL : AbstractSortingAlgorithm
	{
		public override string Name => this.ToString();

		public async override Task<IAlgorithmPerformanceCounter> SortArray(int[] array)
		{
			IAlgorithmPerformanceCounter performanceCounter = Factory.CreatePerformanceCounter(this.ToString());

			await Task.Run(() =>
			{
				performanceCounter.Stopwatch.Start();
				using (var dbContext = new NumberDbContext())
				{
					// Calculate the ArrayId by checking the cumulative size of the arrays
					int arrayId = 1;
					int cumulativeSize = 0;
					while (cumulativeSize < array.Length)
					{
						cumulativeSize += dbContext.Numbers.Count(n => n.ArrayId == arrayId);
						arrayId++;
					}

					// Decrement arrayId because it's incremented one extra time in the loop
					arrayId--;

					// Now arrayId should correspond to the array with the same size as int[] array

					// Get and sort the numbers with this ArrayId
					var numbers = dbContext.Numbers
													.Where(n => n.ArrayId == arrayId);


					performanceCounter.Stopwatch.Start();	// START COUNTING TIME

					List<Number> sortedNumbers = numbers
														.OrderBy(n => n.Value)
														.ToList();
					performanceCounter.Stopwatch.Stop();    // END COUNTING TIME

				}
			});
			return performanceCounter;
		}
		public override string ToString() => "EFMSSQL";
	}

}