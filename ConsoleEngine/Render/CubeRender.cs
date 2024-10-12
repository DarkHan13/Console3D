
using ConsoleRender.ConsoleEngine.MyMath;
using Vector2 = System.Numerics.Vector2;

namespace ConsoleRender.ConsoleEngine.Render;

public class CubeRender
{
    private const float CubeSize = 1f;

    private static readonly Vector3[] vertices = new[]
    {
        new Vector3(-CubeSize, -CubeSize, -CubeSize),
        new Vector3(CubeSize, -CubeSize, -CubeSize),
        new Vector3(CubeSize, CubeSize, -CubeSize),
        new Vector3(-CubeSize, CubeSize, -CubeSize),
        new Vector3(-CubeSize, -CubeSize, CubeSize),
        new Vector3(CubeSize, -CubeSize, CubeSize),
        new Vector3(CubeSize, CubeSize, CubeSize),
        new Vector3(-CubeSize, CubeSize, CubeSize),
    };

    public static Vector2 Project(Vector3 point, float distFromCamera, Vector2 screenSize)
    {
        float factor = distFromCamera / (point.Z + distFromCamera);
        int x = (int)((point.X * factor + 1) * screenSize.X / 2);
        int y = (int)((point.Y * factor + 1) * screenSize.Y / 2);
        return new Vector2(x, y);
    }
}