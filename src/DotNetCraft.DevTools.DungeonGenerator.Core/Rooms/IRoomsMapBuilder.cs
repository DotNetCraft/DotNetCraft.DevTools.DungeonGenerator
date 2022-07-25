using System.Collections.Generic;
using DotNetCraft.DevTools.DungeonGenerator.Core.Geometry;
using DotNetCraft.DevTools.DungeonGenerator.Core.Graphs;

namespace DotNetCraft.DevTools.DungeonGenerator.Core.Rooms
{
    public interface IRoomsMapBuilder
    {
        RoomsMap Build(GraphDoc graphDoc, Rect globalRect);
    }
}
