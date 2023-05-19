using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlghorithmsPerformanceCounter.Models.ArrayInitializers
{
	abstract class ArrayInitializerAbstractBase : IArrayInitializer
	{
		public abstract string Name { get; }

		private const int minArraySize = 3;
		public abstract int[] SingleArrayInitializer(int numberOfValusInArray);
		public int[][] InitializeMultipleArrays(int arrayIncrementValue, int maxNumberOfValues)
		{
			// number of arrays but first array is always of size 3
			int howManySubarrays = ((maxNumberOfValues - minArraySize) / arrayIncrementValue) + 2;
			int[][] arrayOfArrays = new int[howManySubarrays][];
			int[] baseArray = SingleArrayInitializer(maxNumberOfValues);
			int[] firstArray = new int[minArraySize];
			int currentArraySize = minArraySize;
			int currentCreatedArrayIndex = 0;
			while (currentArraySize < maxNumberOfValues)
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
	}
}
