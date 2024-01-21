using System;

namespace Recognizer;
internal static class SobelFilterTask
{
    public static double[,] SobelFilter(double[,] image, double[,] lens)
    {
        int rows = image.GetLength(0);
        int cols = image.GetLength(1);
        var result = new double[rows, cols];
        int midPix = lens.GetLength(0) / 2;
        var sy = Transpose(lens);
        for (int x = midPix; x < rows - midPix; x++)
            for (int y = midPix; y < cols - midPix; y++)
            {
                var gx = ToConvolute(image, lens, x - midPix, y - midPix);
                var gy = ToConvolute(image, sy, x - midPix, y - midPix);
                result[x, y] = Math.Sqrt(gx * gx + gy * gy);
            }
        return result;
    }

    static double ToConvolute(double[,] g, double[,] s, int indexX, int indexY)
    {
        int side = s.GetLength(0);
        double result = 0;
        for (int x = 0; x < side; x++)
        {
            for (int y = 0; y < side; y++)
            {
                result += s[x, y] * g[x + indexX, y + indexY];
            }
        }
        return result;
    }

    static double[,] Transpose(double[,] sx)
    {
        int width = sx.GetLength(0);
        int height = sx.GetLength(1);
        double[,] sy = new double[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (x == y) sy[x, y] = sx[x, y];
                else
                {
                    sy[x, y] = sx[y, x];
                }
            }
        }
        return sy;
    }
}