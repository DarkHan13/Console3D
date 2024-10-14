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

    public static Vector3 operator -(Vector3 a)
    {
        return new Vector3(-a.X, -a.Y, -a.Z);
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
        if (b.X == 0 || b.Y == 0 || b.Z == 0)
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
    
    public static Vector3 RotateY(Vector3 v, float angle)
    {
        // Поворот вектора вокруг оси Y (в 3D-пространстве)
        float cos = MathF.Cos(angle);
        float sin = MathF.Sin(angle);
        return new Vector3(
            v.X * cos - v.Z * sin,
            v.Y,
            v.X * sin + v.Z * cos
        );
    }

    public static Vector3 Abs(Vector3 v)
    {
        return new Vector3(MathF.Abs(v.X), MathF.Abs(v.Y), MathF.Abs(v.Z));
    }

    public static Vector3 Sign(Vector3 v)
    {
        return new Vector3(MathF.Sign(v.X), MathF.Sign(v.Y), MathF.Sign(v.Z));
    }

    public static Vector3 Step(Vector3 v, Vector3 edge)
    {
        return new Vector3(Math.Step(v.X, edge.X), Math.Step(v.Y, edge.Y), Math.Step(v.Z, edge.Z));
    }
}