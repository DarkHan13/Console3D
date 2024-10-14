using ConsoleRender.ConsoleEngine.Math.Raycast;

namespace ConsoleRender.ConsoleEngine.MyMath.Shapes;

public class AABB : Shape
{
    public Vector3 Center { get; private set; }
    public Vector3 Min, Max;  // Минимальная и максимальная точки куба

    public AABB(Vector3 min, Vector3 max)
    {
        Min = min;
        Max = max;
        Center = new Vector3((Max.X + Min.X) / 2f, (Max.Y + Min.Y) / 2f, (Max.Z + Min.Z) / 2f);
    }

    public AABB(Vector3 center, float boxSide)
    {
        Center = center;
        var half = boxSide / 2;
        Min = new Vector3(Center.X - half, Center.Y - half, Center.Z - half);
        Max = new Vector3(Center.X + half, Center.Y + half, Center.Z + half);
    }

    public void SetCenter(Vector3 center)
    {
        var oldCenter = Center;
        Center = center;
        var offset = Center - oldCenter;
        Min += offset;
        Max += offset;
    }

    public void Move(Vector3 moveVector) => SetCenter(Center + moveVector);
    
    public override bool Intersect(Ray ray, out HitInfo hitInfo)
    {
        float tMin = (Min.X - ray.Origin.X) / ray.Direction.X;
        float tMax = (Max.X - ray.Origin.X) / ray.Direction.X;
        if (tMin > tMax) (tMin, tMax) = (tMax, tMin);

        float tyMin = (Min.Y - ray.Origin.Y) / ray.Direction.Y;
        float tyMax = (Max.Y - ray.Origin.Y) / ray.Direction.Y;
        if (tyMin > tyMax) (tyMin, tyMax) = (tyMax, tyMin);

        hitInfo = new HitInfo();
        if ((tMin > tyMax) || (tyMin > tMax))
        {
            hitInfo.IsHit = false;
            return false;
        }

        tMin = MathF.Max(tMin, tyMin);
        tMax = MathF.Min(tMax, tyMax);

        float tzMin = (Min.Z - ray.Origin.Z) / ray.Direction.Z;
        float tzMax = (Max.Z - ray.Origin.Z) / ray.Direction.Z;
        if (tzMin > tzMax) (tzMin, tzMax) = (tzMax, tzMin);

        if ((tMin > tzMax) || (tzMin > tMax))
        {
            hitInfo.IsHit = false;
            return false;
        }

        tMin = MathF.Max(tMin, tzMin);
        tMax = MathF.Min(tMax, tzMax);

        if (tMin < 0) return false;
        // Вычисляем точку пересечения
        hitInfo.HitPoint = ray.Origin + ray.Direction * tMin;

        // Определяем нормаль в точке пересечения
        hitInfo.Normal = GetNormalAtPoint(hitInfo.HitPoint);
        hitInfo.IsHit = true;
        return true;
    }
    
    private Vector3 GetNormalAtPoint(Vector3 point)
    {
        // Определение нормали в зависимости от пересечённой грани
        if (MathF.Abs(point.X - Min.X) < 0.001f) return new Vector3(-1, 0, 0); // Левая грань
        if (MathF.Abs(point.X - Max.X) < 0.001f) return new Vector3(1, 0, 0);  // Правая грань
        if (MathF.Abs(point.Y - Min.Y) < 0.001f) return new Vector3(0, -1, 0); // Нижняя грань
        if (MathF.Abs(point.Y - Max.Y) < 0.001f) return new Vector3(0, 1, 0);  // Верхняя грань
        if (MathF.Abs(point.Z - Min.Z) < 0.001f) return new Vector3(0, 0, -1); // Задняя грань
        if (MathF.Abs(point.Z - Max.Z) < 0.001f) return new Vector3(0, 0, 1);  // Передняя грань

        return new Vector3(0, 0, 0); // На случай ошибки
    }
}