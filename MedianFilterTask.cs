using System.Linq;

namespace Recognizer
{
	internal static class MedianFilterTask
	{
		/* 
		 * Для борьбы с пиксельным шумом, подобным тому, что на изображении,
		 * обычно применяют медианный фильтр, в котором цвет каждого пикселя, 
		 * заменяется на медиану всех цветов в некоторой окрестности пикселя.
		 * https://en.wikipedia.org/wiki/Median_filter
		 * 
		 * Используйте окно размером 3х3 для не граничных пикселей,
		 * Окно размером 2х2 для угловых и 3х2 или 2х3 для граничных.
		 */
		public static double[,] MedianFilter(double[,] original)
		{
			var jober = new double[original.GetLength(0), original.GetLength(1)];
			int length = original.GetLength(0);
			int widht = original.GetLength(1);

			for (int i = 1; i < length - 1; i++)
				for (int j = 1; j < widht - 1; j++)
				{
					var array = new double[] { original[i - 1, j - 1], original[i - 1, j], original[i - 1, j + 1], original[i, j - 1], original[i, j], original[i, j + 1], original[i + 1, j - 1], original[i + 1, j], original[i + 1, j + 1]};
					jober[i, j] = SortMedian(array);
				}
			return jober;
		}

		public static double SortMedian(double[] pixelMedian)
		{
			
			if(pixelMedian.Length % 2 == 0)
            {
				var median = SortBubble(pixelMedian);
				int leftElement = (int)((median.Length / 2) - 0.4);
				int rightElement = (int)((median.Length / 2) - 0.4);
				return (median[leftElement] + median[rightElement]) / 2;
			}
            else 
			{
				var median = SortBubble(pixelMedian);
				int midleElement = (int)((median.Length / 2) - 0.1);
				return median[midleElement];
			}	
		}

		static double[] SortBubble(double[] myArray)
		{
			double temp;
			for (int i = 0; i < myArray.Length; i++)// перебор "1-ого" элемента
			{
				for (int j = i + 1; j < myArray.Length; j++)// перебор "2-ого" элемента
				{
					if (myArray[i] > myArray[j])//сравние близике элементы
					{                           // первый элемент больше второго, то меняем местами
						temp = myArray[i];
						myArray[i] = myArray[j];
						myArray[j] = temp;
					}
				}
			}
			return myArray;
		}
	}
}