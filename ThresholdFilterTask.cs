using System.Collections.Generic;

namespace Recognizer
{
	public static class ThresholdFilterTask
	{
		public static double[,] ThresholdFilter(double[,] original, double whitePixelsFraction)
		{
			int length = original.GetLength(0);
			int widht = original.GetLength(1);
			var pixelList = new List<double>();
			int T = (int)(length * widht * whitePixelsFraction);

			for(int i = 0; i < length; i++)
				for (int j = 0; j < widht; j++)
					pixelList.Add(original[i,j]);
			pixelList.Sort();

			double maxPixel = pixelList[pixelList.Count - T];
			var finishPixel = new double[original.GetLength(0), original.GetLength(1)];
			for (int i = 0; i < length; i++)
				for (int j = 0; j < widht; j++)
                {
					if (original[i, j] < maxPixel)
						finishPixel[i, j] = 0.0;
					else finishPixel[i, j] = 1.0;
				}

			return finishPixel;
		}
	}
}