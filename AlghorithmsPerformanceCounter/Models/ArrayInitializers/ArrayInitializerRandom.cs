using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlghorithmsPerformanceCounter.Models.ArrayInitializers
{
	internal class ArrayInitializerRandom : ArrayInitializerAbstractBase
	{
		private Random random = new Random();

		public override string Name => "Random case";

		public override int[] SingleArrayInitializer(int numberOfValusInArray)
		{
			int[] array = new int[numberOfValusInArray];
			for (int i = 0; i < numberOfValusInArray; i++)
			{
				array[i] = ValuesRandoizer();
			}
			return array;
		}
		int ValuesRandoizer()
		{
			return random.Next(1, 1001);
		}
	}
}
