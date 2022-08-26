using System;

namespace Recognizer
{
    internal static class SobelFilterTask
    {
        public static double[,] SobelFilter(double[,] g, double[,] sx)
        {
            var width = g.GetLength(0);
            var height = g.GetLength(1);
            var widthSx = sx.GetLength(0);
            var result = new double[width, height];
            int shiftMatrix = (int)(sx.GetLength(0) / 2.0);


            for (int i = shiftMatrix; i < width - shiftMatrix; i++)
                for (int j = shiftMatrix; j < height - shiftMatrix; j++)
                {
                    var midleResult = GetNeighbourhood(g, i, j, widthSx);
                    double resultX = SumingMatrix(MultiplicationMatrix(midleResult, sx));
                    double resultY = SumingMatrix(MultiplicationMatrix(midleResult, TranspositionMatrix(sx)));
                    result[i, j] = Math.Sqrt(resultX * resultX + resultY * resultY);
                }

            return result;
        }

        public static double[,] TranspositionMatrix(double[,] matrix)
        {
            var n = matrix.GetUpperBound(0) + 1;
            var m = matrix.GetUpperBound(1) + 1;
            var result = new double[m, n];
            for (var i = 0; i < n; i++)
                for (var j = 0; j < m; j++)
                    result[j, i] = matrix[i, j];
            return result;
        }

        public static double SumingMatrix(double[,] matrix)
        {
            var width = matrix.GetLength(0);
            var height = matrix.GetLength(1);
            double SumMatrix = 0;
            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                {
                    SumMatrix += matrix[x, y];
                }
            return SumMatrix;
        }

        public static double[,] MultiplicationMatrix(double[,] matrixFirst, double[,] matrixSecond)
        {
            var width = matrixFirst.GetLength(0);
            var height = matrixFirst.GetLength(1);
            var multiplicationMatrix = new double[width, height];
            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                {
                    multiplicationMatrix[x, y] = matrixFirst[x, y] * matrixSecond[x, y];
                }
            return multiplicationMatrix;
        }

        public static double[,] GetNeighbourhood(double[,] g, int x, int y, int size)
        {
            var result = new double[size, size];
            result.Initialize();
            var delta = (int)(size / 2);
            for (var i = x - delta; i <= x + delta; i++)
                for (var j = y - delta; j <= y + delta; j++)
                    result[i - (x - delta), j - (y - delta)] = g[i, j];
            return result;
        }
    }
}