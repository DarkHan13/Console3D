using System.Drawing;
using ConsoleRender.ConsoleEngine.MyMath;
using Math = System.Math;

namespace ConsoleRender.ConsoleEngine;

class Programm
{
    static void Main()
    {
        int width = 120, height = 30;
        // int width = Console.LargestWindowWidth, height = Console.LargestWindowHeight;
        Console.SetWindowSize(width, height);
        float aspect = (float)width / height;
        Size charSize = Kernel32.GetConsoleFontSize();
        float pixelAspect = (float)charSize.Width / charSize.Height;
        string gradient = " .:!/r(I1Z4H9W8$@";
        int gradientSize = gradient.Length;
        
        char[] buffer = new char[width * height];
        buffer[0] = 'A';

        Console.CursorVisible = false;
        for (int t = 0; t < 1000; t++)
        {
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Vector2 uv = new Vector2(i, j) / new Vector2(width, height) * 2f - 1f;
                    uv.X *= aspect * pixelAspect;
                    // uv.X += (float)Math.Sin(t * 0.02);
                    // uv.Y += (float)Math.Cos(t * 0.02);
                    char pixel = ' ';
                    float dist = (float)Math.Sqrt(uv.X * uv.X + uv.Y * uv.Y);
                    int color = (int)(1 / dist);
                    pixel = gradient[MyMath.Math.Clamp(color, 0, gradientSize - 1)];
                    buffer[i + j * width] = pixel;
                }
            }
            Thread.Sleep(16);
            Console.Write(buffer);
        }
        
        Console.ReadKey();
    }
}