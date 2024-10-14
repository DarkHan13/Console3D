using ConsoleRender.ConsoleEngine.Math.Raycast;

namespace ConsoleRender.ConsoleEngine.MyMath.Shapes;

public abstract class Shape
{
    public abstract bool Intersect(Ray ray, out HitInfo hit);
}