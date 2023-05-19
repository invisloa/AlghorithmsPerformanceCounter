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
			int howManySubarrays = (maxNumberOfValues / arrayIncrementValue)+1;
			int[][] arrayOfArrays = new int[howManySubarrays][];
			int[] baseArray = SingleArrayInitializer(maxNumberOfValues);
			int[] firstArray = new int[minArraySize];
			Array.Copy(baseArray, 0, firstArray, 0, firstArray.Length);
			int currentArraySize = arrayIncrementValue;

			arrayOfArrays[0] = firstArray;
			int currentCreatedArrayIndex = 1; 
			while (currentArraySize <= maxNumberOfValues)
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
