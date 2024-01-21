using System.Collections.Generic;

namespace Recognizer;

public static class ThresholdFilterTask
{
	public static double[,] ThresholdFilter(double[,] original, double whitePixelsFraction)
    {
        if (original.Length == 1 && whitePixelsFraction == 0)
        {
            original[0, 0] = 0;
            return original;
        }

        int rows = original.GetLength(0);
        int cols = original.GetLength(1);

        List<double> pixels = ToList(original, rows, cols);

        int pixelsCount = (int)(whitePixelsFraction * original.Length);
        if (pixelsCount > 0)
        {
            pixels.Sort();
            double T = pixels[^pixelsCount];
            MakeBlackAndWhite(ref original, rows, cols, T);

            return original;
        }
        else
        {
            PaintInBlack(rows, cols, ref original);
            return original;
        }
    }

    private static List<double> ToList(in double[,] original, in int rows, in int cols)
    {
        List<double> pixels = new();

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                pixels.Add(original[i, j]);
            }
        }

        return pixels;
    }

    private static void MakeBlackAndWhite(ref double[,] original, in int rows, in int cols, in double T)
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (original[i, j] >= T)
                {
                    original[i, j] = 1.0;
                }
                else
                {
                    original[i, j] = 0.0;
                }
            }
        }
    }

    private static void PaintInBlack(in int rows, in int cols, ref double[,] original)
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                original[i, j] = 0.0;
            }
        }
    }
}
