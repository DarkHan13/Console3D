using System.Diagnostics;
using System.Drawing;
using ConsoleRender.ConsoleEngine.Math.Raycast;
using ConsoleRender.ConsoleEngine.MyMath;
using ConsoleRender.ConsoleEngine.MyMath.Shapes;
using Math = System.Math;

namespace ConsoleRender.ConsoleEngine;

class Programm
{
    static string gradient = " .:!/r(I1Z4H9W8$@";
    static int gradientSize = 4;
    
    static void Main()
    {
        gradientSize = gradient.Length;
        // int width = 120, height = 30;
        int width = Console.LargestWindowWidth, height = Console.LargestWindowHeight;
        Console.SetWindowSize(width, height);
        float aspect = (float)width / height;
        Size charSize = Kernel32.GetConsoleFontSize();
        float pixelAspect = (float)charSize.Width / charSize.Height;

        
        char[] buffer = new char[width * height];

        float cameraAngle = 0;
        Vector3 cameraPos = new Vector3(0, 0, -2);
        Vector3 lightPos = new Vector3(0, 5, 0);
        // AABB aabb = new AABB(new Vector3(-1, -1, -1), new Vector3(1, 1, 1));

        List<Shape> shapes = new List<Shape>() 
            {
                // new AABB(new Vector3(0, 0, 0), 1), 
                new Sphere(new Vector3(0, 0, 5), 1f),
            };
        for (int x = -5; x <= 5; x++)
        {
            for (int z = -5; z <= 5; z++)
            {
                shapes.Add(new AABB(new Vector3(x, 1, z), 0.8f));
            }
        }
        Console.CursorVisible = false;
        float t = 0, deltaTime = 0;
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        while (true)
        {
            var newW = Console.WindowWidth;
            var newH = Console.WindowHeight;
            if (newW != width || newH != height)
            {
                width = newW;
                height = newH;
                buffer = new char[width * height];
                aspect = (float)width / height;
            }
            deltaTime = stopwatch.ElapsedMilliseconds - t;
            t = stopwatch.ElapsedMilliseconds; 
            if (Console.KeyAvailable)
            {
                ConsoleKey key = Console.ReadKey(true).Key; // Считываем нажатую клавишу без вывода на экран

                switch (key)
                {
                    case ConsoleKey.S: 
                        cameraPos.Z -= 0.1f;
                        break;
                    case ConsoleKey.W: 
                        cameraPos.Z += 0.1f;
                        break;
                    case ConsoleKey.A: 
                        cameraPos.X -= 0.1f;
                        break;
                    case ConsoleKey.D: 
                        cameraPos.X += 0.1f;
                        break;
                    case ConsoleKey.LeftArrow:
                        cameraAngle += 0.02f;
                        break;
                    case ConsoleKey.RightArrow:
                        cameraAngle -= 0.02f;
                        break;
                    case ConsoleKey.Escape: // Выход из приложения
                        Console.Clear();
                        Console.SetCursorPosition(width / 2, height / 2);
                        Console.WriteLine("Exit...");
                        return;
                }
            }
            
            lightPos.X = MathF.Cos(t * 0.005f) * 20f;
            lightPos.Y = MathF.Sin(t * 0.005f) * 20f;
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Vector2 uv = new Vector2(i, j) / new Vector2(width, height) * 2f - 1f;
                    uv.X *= aspect * pixelAspect;
                    Vector3 rayDir = new Vector3(uv.X, uv.Y, 1);
                    rayDir = Vector3.RotateY(rayDir, cameraAngle);
                    Ray ray = new Ray(cameraPos, rayDir);
                    char pixel = ' ';
                    HitInfo bestHit = new HitInfo();
                    float minDist = Single.PositiveInfinity;
                    foreach (var shape in shapes)
                    {
                        if (shape.Intersect(ray, out var hit))
                        {
                            var dist = (ray.Origin - hit.HitPoint).Length();
                            if (dist < minDist)
                            {
                                minDist = dist;
                                bestHit = hit;
                            }

                        }
                        
                    }

                    if (bestHit.IsHit)
                    {
                        float brightness = MathF.Max(0, Vector3.Dot(bestHit.Normal, (bestHit.HitPoint - lightPos).Normalize()));
                        pixel = gradient[(int)(brightness * (gradientSize - 1))];
                    }
                    buffer[i + j * width] = pixel;
                }
            }
            var s = (stopwatch.ElapsedMilliseconds).ToString("00");
            buffer[0] = s[0];
            buffer[1] = s[1];
            Console.Write(buffer);
        }
        
    }

    
}