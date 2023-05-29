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
					// ArrayId is used to specify which array is being used - no need to create separate table for each array
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
					performanceCounter.Stopwatch.Start();   // START TIME

					var count = dbContext.Numbers.Where(x => x.ArrayId == arrayId).First(); ;  // USING First TO MATERIALIZE THE QUERRY TIME (no need to copy all array)

					performanceCounter.Stopwatch.Stop();    // END TIME

				}
			});
			return performanceCounter;
		}
		public override string ToString() => "EFMSSQL";
	}

}