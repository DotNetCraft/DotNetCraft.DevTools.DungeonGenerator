namespace DotNetCraft.DevTools.DungeonGenerator.Core.Geometry
{
    public interface ILineGeometry
    {
        bool CheckLine(float a1, float a2, float a3, float a4);

        bool TryGetCommonLineSegment(float a1, float a2, float a3, float a4, out float a, out float b);
    }
}
