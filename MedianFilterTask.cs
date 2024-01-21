using NUnit.Framework;
using System.Collections.Generic;

namespace Recognizer;

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
        if (original.Length == 1)
        {
            return original;
        }
        
        int rows = original.GetLength(0);
        int cols = original.GetLength(1);
        double[,] filtred = new double[rows, cols];

        for (int i = 0; i < rows; i++)
        {
             for(int j = 0; j < cols; j++)
             {
                 filtred[i,j] = Filter(i, j, original, rows, cols);
             }
        }

            return filtred;
	}

    public static double Filter(int row, int col, in double[,] original, in int maxY, in int maxX)
    {
        List<double> nearbyPixels = new();
        for (int i = row - 1; i <= row + 1; i++)
        {
            for (int j = col - 1; j <= col + 1; j++)
            {
                if (i >= 0 && i < maxY && j >= 0 && j < maxX)
                {
                    nearbyPixels.Add(original[i, j]);
                }
            }
        }
        nearbyPixels.Sort();

        if (nearbyPixels.Count % 2 == 0)
        {
            int index = nearbyPixels.Count / 2;
            return (nearbyPixels[index - 1] + nearbyPixels[index]) / 2;
        }
        else
        {
            return nearbyPixels[nearbyPixels.Count / 2];
        }
    }
}
