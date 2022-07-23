using System.Collections.Generic;
using DotNetCraft.DevTools.DungeonGenerator.Core.Geometry;

namespace DotNetCraft.DevTools.DungeonGenerator.Core.Graphs
{
    public class GraphMetadata
    {
        public Dictionary<int, Rect> Geometries { get; set; } = new Dictionary<int, Rect>();
        public Dictionary<string, LineSegment> LineSegments { get; set; } = new Dictionary<string, LineSegment>();
    }
}
