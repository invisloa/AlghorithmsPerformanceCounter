using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlghorithmsPerformanceCounter.Models.ArrayInitializers
{
	public class ArrayInitializer : IArrayInitializer
	{
		int howManyArrays;
		int howManyValuesInAnArray;

		Random random = new Random();

		public int NumberOfArraysToSort => howManyArrays;
		public int NumberOfValuesInArray => howManyValuesInAnArray;
		public int[] SingleArrayInitializer()
		{
			Console.Write("Enter the maximum length of the array: ");
			int NumberOfValuesInArray = GetUserInputValues();
			return SingleArrayInitializer(NumberOfValuesInArray);
		}
		public int[] SingleArrayInitializer(int maxLength)
		{
			int[] array = new int[maxLength];
			for (int i = 0; i < maxLength; i++)
			{
				array[i] = ValuesRandoizer();
			}
			/*			Console.WriteLine("Generated array:");
						Console.WriteLine(string.Join(", ", array));
			*/
			return array;
		}


		public int[][] InitializeMultipleArrays()
		{
			Console.Write("Enter the numbers of arrays: ");
			howManyArrays = GetUserInputValues();
			Console.Write("Enter the numbers of values in a single array: ");
			howManyValuesInAnArray = GetUserInputValues();
			int[][] arrayOfArrays = new int[NumberOfArraysToSort][];

			for (int i = 0; i < NumberOfArraysToSort; i++)
			{
				arrayOfArrays[i] = SingleArrayInitializer(NumberOfValuesInArray);
			}
			return arrayOfArrays;
		}
		int GetUserInputValues()
		{
			int userInput;
			while (!int.TryParse(Console.ReadLine(), out userInput))
			{
				Console.WriteLine("hmmmm.... Are You sure it was a good number....??");
			}
			return userInput;
		}
		int ValuesRandoizer()
		{
			return random.Next(1, 1001);
		}

	}
}
