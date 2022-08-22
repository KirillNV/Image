using System.Collections.Generic;

namespace Recognizer
{
	public static class ThresholdFilterTask
	{
		public static double[,] ThresholdFilter(double[,] original, double whitePixelsFraction)
		{
			int length = original.GetLength(0);
			int widht = original.GetLength(1);

			int T = (int)(length * widht * whitePixelsFraction);
			double maxPixel = FiltredPixel(CreateSortedList(original, length, widht), T);

			var finishPixel = new double[original.GetLength(0), original.GetLength(1)];
			for (int i = 0; i < length; i++)
				for (int j = 0; j < widht; j++)
				{
					if (original[i, j] >= maxPixel)
						finishPixel[i, j] = 1.0;
					else finishPixel[i, j] = 0.0;
				}

			return finishPixel;
		}

		public static List<double> CreateSortedList(double[,] original, int length, int widht)
		{
			var pixelList = new List<double>();
			for (int i = 0; i < length; i++)
				for (int j = 0; j < widht; j++)
					pixelList.Add(original[i, j]);
			pixelList.Sort();
			return pixelList;
		}

		public static double FiltredPixel(List<double> pixelList, int T)
		{
			double maxPixel;
			if (T == 0)
				maxPixel = double.MaxValue;
			else
				maxPixel = pixelList[pixelList.Count - T];
			return maxPixel;
		}
	}
}