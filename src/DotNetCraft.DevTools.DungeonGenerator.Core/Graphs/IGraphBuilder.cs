using System.Collections.Generic;
using DotNetCraft.DevTools.DungeonGenerator.Core.BinarySpacePartitioning;

namespace DotNetCraft.DevTools.DungeonGenerator.Core.Graphs
{
    public interface IGraphBuilder
    {
        Graph Build(List<Leaf> leaves);
    }
}
