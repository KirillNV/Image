using System.Collections.Generic;
using System.Linq;

namespace Recognizer
{
	internal static class MedianFilterTask
	{
		public static double[,] MedianFilter(double[,] original)
		{
			var jober = new double[original.GetLength(0), original.GetLength(1)];
			int length = original.GetLength(0);
			int widht = original.GetLength(1);


			for (int i = 0; i < length; i++)
				for (int j = 0; j < widht; j++)
				{
					var list = new List<double>();
					for (int k = - 1; k < 2; k++)
						for (int p = - 1; p < 2; p++)
							try { list.Add(original[i + k, j + p]); } catch { }
					
					jober[i, j] = SortMedian(list.ToArray());
				}
			return jober;
		}

		public static double SortMedian(double[] pixelMedian)
		{
			
			if(pixelMedian.Length % 2 == 0)
            {
				var median = SortBubble(pixelMedian);
				int leftElement = (int)((median.Length / 2) - 0.5);
				int rightElement = (int)((median.Length / 2) + 0.5);
				return (median[leftElement] + median[rightElement]) / 2;
			}
            else 
			{
				var median = SortBubble(pixelMedian);
				int midleElement = (int)((median.Length / 2.0) - 0.1);
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