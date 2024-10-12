namespace ConsoleRender.ConsoleEngine.MyMath;

public struct Vector3
{
    public float X, Y, Z;

    public Vector3(float x, float y, float z)
    {
        X = x;
        Y = y;
        Z = z;
    }
    
    public static Vector3 operator +(Vector3 a, Vector3 b)
    {
        return new Vector3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
    }
    
    public static Vector3 operator +(Vector3 a, float scalar)
    {
        return new Vector3(a.X + scalar, a.Y + scalar, a.Z + scalar);
    }

    public static Vector3 operator -(Vector3 a, Vector3 b)
    {
        return new Vector3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
    }
    
    public static Vector3 operator -(Vector3 a, float scalar)
    {
        return new Vector3(a.X - scalar, a.Y - scalar, a.Z - scalar);
    }

    public static Vector3 operator *(Vector3 a, Vector3 b)
    {
        return new Vector3(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
    }

    public static Vector3 operator *(Vector3 a, float scalar)
    {
        return new Vector3(a.X * scalar, a.Y * scalar, a.Z * scalar);
    }

    public static Vector3 operator /(Vector3 a, Vector3 b)
    {
        if (b.X == 0 || b.Y == 0)
            throw new DivideByZeroException("Деление на ноль в одном из компонентов.");
        return new Vector3(a.X / b.X, a.Y / b.Y, a.Z / b.Z);
    }

    public static Vector3 operator /(Vector3 a, float scalar)
    {
        if (scalar == 0)
            throw new DivideByZeroException("Деление на ноль.");
        return new Vector3(a.X / scalar, a.Y / scalar, a.Z / scalar);
    }

    // Переопределение метода ToString для удобного вывода
    public override string ToString()
    {
        return $"({X}, {Y}, {Z})";
    }

    public Vector3 Normalize()
    {
        return this / Length();
    }

    public float Length()
    {
        return (float)System.Math.Sqrt(X * X + Y * Y + Z * Z);
    }

    public static float Dot(Vector3 v1, Vector3 v2)
    {
        return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
    }
}