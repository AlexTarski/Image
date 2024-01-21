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
		
		List<double> pixels = new();
		for (int i = 0; i < original.GetLength(0); i++)
		{
			for (int j = 0; j < original.GetLength(1); j++)
			{
				pixels.Add(original[i, j]);
			}
		}

		int pixelsCount = (int)(whitePixelsFraction * original.Length);
		pixels.Sort();
		double T = pixels[^pixelsCount];

        for (int i = 0; i < original.GetLength(0); i++)
        {
            for (int j = 0; j < original.GetLength(1); j++)
            {
                if (original[i,j] >= T)
				{
					original[i,j] = 1.0;
				}
				else
				{
					original[i,j] = 0.0;
				}
            }
        }

        return original;
	}
}
