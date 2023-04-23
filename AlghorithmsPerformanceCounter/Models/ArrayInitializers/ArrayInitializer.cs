using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlghorithmsPerformanceCounter.Models.ArrayInitializers
{
	public class ArrayInitializer : IArrayInitializer
	{
		public int[] SingleArrayInitializer(int numberOfValusInArray)
		{
			int[] array = new int[numberOfValusInArray];
			for (int i = 0; i < numberOfValusInArray; i++)
			{
				array[i] = ValuesRandoizer();
			}
			return array;
		}
		public int[][] InitializeMultipleArrays(int numberOfArrays, int numberOfValusInArray)
		{
			int[][] arrayOfArrays = new int[numberOfArrays][];

			for (int i = 0; i < numberOfArrays; i++)
			{
				arrayOfArrays[i] = SingleArrayInitializer(numberOfValusInArray);
			}
			return arrayOfArrays;
		}
		int ValuesRandoizer()
		{
			Random random = new Random();
			return random.Next(1, 1001);
		}

	}
}
