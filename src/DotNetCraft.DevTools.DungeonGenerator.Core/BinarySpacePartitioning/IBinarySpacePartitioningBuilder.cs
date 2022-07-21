using System.Collections.Generic;
using DotNetCraft.DevTools.DungeonGenerator.Core.Geometry;

namespace DotNetCraft.DevTools.DungeonGenerator.Core.BinarySpacePartitioning
{
    public interface IBinarySpacePartitioningBuilder
    {
        public List<Leaf> Build(Rect mainRect, BuildConfig buildConfig);
    }
}
