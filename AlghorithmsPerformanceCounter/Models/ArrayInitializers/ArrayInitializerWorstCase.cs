using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlghorithmsPerformanceCounter.Models.ArrayInitializers
{
	internal class ArrayInitializerWorstCase : ArrayInitializerAbstractBase
	{
		public override string Name => "Worst case";

		public override int[] SingleArrayInitializer(int numberOfValusInArray)
		{
			int maxValue = numberOfValusInArray;
			int currentArrayIdex = 0;
			int[] array = new int[numberOfValusInArray];
			while (maxValue > 0) 
			{
				array[currentArrayIdex] = maxValue;
				currentArrayIdex++;
				maxValue--;
			}
			return array;
		}
	}
}
