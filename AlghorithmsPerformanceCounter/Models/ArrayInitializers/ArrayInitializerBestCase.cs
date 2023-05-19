using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlghorithmsPerformanceCounter.Models.ArrayInitializers
{
	internal class ArrayInitializerBestCase : ArrayInitializerAbstractBase
	{
		public override string Name => "Best case";

		public override int[] SingleArrayInitializer(int numberOfValusInArray)
		{
			int currentArrayIdex = 0;
			int[] array = new int[numberOfValusInArray];
			while (currentArrayIdex < numberOfValusInArray)
			{
				array[currentArrayIdex] = currentArrayIdex;
				currentArrayIdex++;
			}
			return array;
		}
	}
}
