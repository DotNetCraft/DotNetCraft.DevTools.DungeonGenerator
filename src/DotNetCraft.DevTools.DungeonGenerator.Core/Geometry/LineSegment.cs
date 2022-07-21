using System;
using DotNetCraft.DevTools.DungeonGenerator.Core.Geometry.Vectors;

namespace DotNetCraft.DevTools.DungeonGenerator.Core.Geometry
{
    public class LineSegment
    {
        public Vector2 Start { get; }
        public Vector2 End { get; }

        public LineSegment(Vector2 start, Vector2 end)
        {
            Start = start ?? throw new ArgumentNullException(nameof(start));
            End = end ?? throw new ArgumentNullException(nameof(end));
        }

        #region Overrides of Object

        public override string ToString()
        {
            return $"Start: {Start}; End: {End}";
        }

        #endregion
    }
}
