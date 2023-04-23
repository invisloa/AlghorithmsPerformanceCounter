using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlghorithmsPerformanceCounter.Models.ArrayInitializers
{
	public class ArrayInitializer : IArrayInitializer
	{
		private const int minArraySize = 3;
		private Random random = new Random();

		public int[] SingleArrayInitializer(int numberOfValusInArray)
		{
			int[] array = new int[numberOfValusInArray];
			for (int i = 0; i < numberOfValusInArray; i++)
			{
				array[i] = ValuesRandoizer();
			}
			return array;
		}
		public int[][] InitializeMultipleArrays(int numberOfArrays, int maxNumberOfValues)
		{
			int[][] arrayOfArrays = new int[numberOfArrays][];
			int[] baseArray = SingleArrayInitializer(maxNumberOfValues);
			int[] firstArray = new int[minArraySize];
			Array.Copy(baseArray, 0, firstArray, 0, minArraySize);
			arrayOfArrays[0] = firstArray;
			if (numberOfArrays > 2)
			{
				for (int i = 1; i < numberOfArrays -1; i++)
				{
					int currentArraySize = (maxNumberOfValues / numberOfArrays) * i;
					int[] arrayToAdd = new int[currentArraySize];
					Array.Copy(baseArray, 0, arrayToAdd, 0, currentArraySize);
					arrayOfArrays[i] = arrayToAdd;
				}
			}
			arrayOfArrays[numberOfArrays-1] = baseArray;
			return arrayOfArrays;
		}

		int ValuesRandoizer()
		{
			return random.Next(1, 1001);
		}

	}
}
