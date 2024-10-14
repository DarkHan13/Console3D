namespace ConsoleRender.ConsoleEngine.MyMath;

public static class Math
{
    public static float Clamp(float value, float min, float max)
    {
        return Min(Max(value, min), max);
    }
    
    public static int Clamp(int value, int min, int max)
    {
        return Min(Max(value, min), max);
    }

    public static float Min(float a, float b)
    {
        return a < b ? a : b;
    }
    
    public static int Min(int a, int b)
    {
        return a < b ? a : b;
    }

    public static float Max(float a, float b)
    {
        return a > b ? a : b;
    }

    public static int Max(int a, int b)
    {
        return a > b ? a : b;
    }

    public static float Step(float x, float edge)
    {
        return x > edge ? 1f : 0;
    }
}