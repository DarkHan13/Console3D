namespace ConsoleRender.ConsoleEngine.MyMath;

public struct Vector2
{
    public float X, Y;

    public Vector2(float x, float y)
    {
        X = x;
        Y = y;
    }
    
    public static Vector2 operator +(Vector2 a, Vector2 b)
    {
        return new Vector2(a.X + b.X, a.Y + b.Y);
    }
    
    public static Vector2 operator +(Vector2 a, float scalar)
    {
        return new Vector2(a.X + scalar, a.Y + scalar);
    }

    public static Vector2 operator -(Vector2 a, Vector2 b)
    {
        return new Vector2(a.X - b.X, a.Y - b.Y);
    }
    
    public static Vector2 operator -(Vector2 a, float scalar)
    {
        return new Vector2(a.X - scalar, a.Y - scalar);
    }

    public static Vector2 operator *(Vector2 a, Vector2 b)
    {
        return new Vector2(a.X * b.X, a.Y * b.Y);
    }

    public static Vector2 operator *(Vector2 a, float scalar)
    {
        return new Vector2(a.X * scalar, a.Y * scalar);
    }

    public static Vector2 operator /(Vector2 a, Vector2 b)
    {
        if (b.X == 0 || b.Y == 0)
            throw new DivideByZeroException("Деление на ноль в одном из компонентов.");
        return new Vector2(a.X / b.X, a.Y / b.Y);
    }

    public static Vector2 operator /(Vector2 a, float scalar)
    {
        if (scalar == 0)
            throw new DivideByZeroException("Деление на ноль.");
        return new Vector2(a.X / scalar, a.Y / scalar);
    }

    // Переопределение метода ToString для удобного вывода
    public override string ToString()
    {
        return $"({X}, {Y})";
    }
}