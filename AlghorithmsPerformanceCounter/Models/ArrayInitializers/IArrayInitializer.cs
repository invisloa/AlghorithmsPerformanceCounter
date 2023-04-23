namespace AlghorithmsPerformanceCounter.Models.ArrayInitializers
{
	public interface IArrayInitializer
	{
		public int[] SingleArrayInitializer(int numberOfValuesInArray);
		public int[][] InitializeMultipleArrays(int numberOfArrays,int numberOfValuesInArray);
	}
}