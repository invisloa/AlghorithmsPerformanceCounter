using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlghorithmsPerformanceCounter.Models
{
	public class NumberModelDb
	{
		public int Id { get; set; }
		public int Value { get; set; }
		public int ArrayId { get; set; }  //  property to distinguish different arrays

	}
}
