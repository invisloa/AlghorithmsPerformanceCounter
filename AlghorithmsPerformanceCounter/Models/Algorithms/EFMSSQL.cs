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

					// Get and sort the numbers with this ArrayId
					performanceCounter.Stopwatch.Start();   // START COUNTING TIME

					var count = dbContext.Numbers.Where(x => x.ArrayId == arrayId).First(); ;  // USING First TO MATERIALIZE THE QUERRY TIME (First SIGNIFICANTLY REDUCES THE TIME)

					performanceCounter.Stopwatch.Stop();    // END COUNTING TIME

				}
			});
			return performanceCounter;
		}
		public override string ToString() => "EFMSSQL";
	}

}