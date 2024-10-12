using System.Drawing;
using ConsoleRender.ConsoleEngine.MyMath;
using Math = System.Math;

namespace ConsoleRender.ConsoleEngine;

class Programm
{
    static string gradient = " .:!/r(I1Z4H9W8$@";
    static int gradientSize = 4;
    
    static void Main()
    {
        gradientSize = gradient.Length;
        int width = 120, height = 30;
        // int width = Console.LargestWindowWidth, height = Console.LargestWindowHeight;
        Console.SetWindowSize(width, height);
        float aspect = (float)width / height;
        Size charSize = Kernel32.GetConsoleFontSize();
        float pixelAspect = (float)charSize.Width / charSize.Height;

        
        char[] buffer = new char[width * height];

        Vector3 cameraPos = new Vector3(0, 0, 0);
        Vector3 lightPos = new Vector3(0, -5, 0);
        Vector3 spherePos = new Vector3(0, 0, 10);
        
        Console.CursorVisible = false;
        for (int t = 0; t < 1000; t++)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKey key = Console.ReadKey(true).Key; // Считываем нажатую клавишу без вывода на экран

                switch (key)
                {
                    case ConsoleKey.W: // Движение вверх
                        cameraPos.Y -= 0.1f;
                        break;
                    case ConsoleKey.S: // Движение вниз
                        cameraPos.Y += 0.1f;
                        break;
                    case ConsoleKey.A: // Движение влево
                        cameraPos.X -= 0.1f;
                        break;
                    case ConsoleKey.D: // Движение вправо
                        cameraPos.X += 0.1f;
                        break;
                    case ConsoleKey.Escape: // Выход из приложения
                        return;
                }
            }
            
            lightPos.X = MathF.Cos(t * 0.01f) * 2f;
            lightPos.Z = MathF.Sin(t * 0.01f) * 2f;
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Vector2 uv = new Vector2(i, j) / new Vector2(width, height) * 2f - 1f;
                    uv.X *= aspect * pixelAspect;
                    Vector3 rayDir = new Vector3(uv.X, uv.Y, 1).Normalize();
                    char pixel = ' ';
                    if (RayIntersectsSphere(cameraPos, rayDir,
                            spherePos, 5f, out Vector3 hit))
                    {
                        Vector3 normal = (hit - spherePos).Normalize();
                        float brightness = MathF.Max(0, Vector3.Dot(normal, (lightPos - hit).Normalize()));
                        pixel = gradient[(int)(brightness * (gradientSize - 1))];
                        
                    }
                    buffer[i + j * width] = pixel;
                }
            }
            Thread.Sleep(16);
            Console.Write(buffer);
        }
        
        Console.ReadKey();
    }

    public static bool RayIntersectsSphere(Vector3 origin, Vector3 direction, Vector3 spherePos, float radius, out Vector3 hit)
    {
        Vector3 oc = origin - spherePos;
        float a = Vector3.Dot(direction, direction);
        float b = 2f * Vector3.Dot(oc, direction);
        float c = Vector3.Dot(oc, oc) - radius * radius;
        float discriminant = b * b - 4 * a * c;
        if (discriminant < 0)
        {
            hit = new Vector3(0, 0, 0);
            return false;
        }

        float t = (-b - MathF.Sqrt(discriminant)) / (2f * a);
        hit = origin + direction * t;
        return true;
    }
}