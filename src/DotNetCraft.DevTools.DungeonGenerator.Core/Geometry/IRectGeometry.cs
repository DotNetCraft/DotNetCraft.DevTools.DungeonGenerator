namespace DotNetCraft.DevTools.DungeonGenerator.Core.Geometry
{
    public interface IRectGeometry
    {
        bool TryGetCommonSide(Rect rectA, Rect rectB, out LineSegment lineSegment);
    }
}
