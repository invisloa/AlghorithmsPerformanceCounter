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
		public int[][] InitializeMultipleArrays(int arrayIncrementValue, int maxNumberOfValues)
		{
			// number of arrays but first array is always of size 3
			int howManySubarrays = ((maxNumberOfValues-minArraySize)/arrayIncrementValue) +2; 
			int[][] arrayOfArrays = new int[howManySubarrays][];
			int[] baseArray = SingleArrayInitializer(maxNumberOfValues);
			int[] firstArray = new int[minArraySize];
			int currentArraySize = minArraySize;
			int currentCreatedArrayIndex = 0;
			while(currentArraySize < maxNumberOfValues)
			{
				int[] arrayToAdd = new int[currentArraySize];
				Array.Copy(baseArray, 0, arrayToAdd, 0, currentArraySize);
				arrayOfArrays[currentCreatedArrayIndex] = arrayToAdd;
				currentArraySize += arrayIncrementValue;
				currentCreatedArrayIndex++;
			}
			arrayOfArrays[arrayOfArrays.Length - 1] = baseArray;
			return arrayOfArrays;
		}
		int ValuesRandoizer()
		{
			return random.Next(1, 1001);
		}
	}
}
