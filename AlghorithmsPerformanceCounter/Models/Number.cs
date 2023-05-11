using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlghorithmsPerformanceCounter.Models
{
	public class Number
	{
		public int Id { get; set; }
		public int Value { get; set; }
		public int ArrayId { get; set; }  // New property to distinguish different arrays

	}
}
