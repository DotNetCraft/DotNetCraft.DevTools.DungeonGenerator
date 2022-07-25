using System;
using DotNetCraft.DevTools.DungeonGenerator.Core.Geometry;

namespace DotNetCraft.DevTools.DungeonGenerator.Core.BinarySpacePartitioning
{
    public class BuildConfig
    {
        public int MinSize { get; set; }
        public int MaxSize { get; set; }
        public Func<Rect, bool> SkipRoomFunc { get; set; }

        #region Overrides of Object

        public override string ToString()
        {
            if (SkipRoomFunc == null)
                return $"Min: {MinSize}, Max: {MaxSize}";
            return $"Min: {MinSize}, Max: {MaxSize}; Func: " + "{...}";
        }

        #endregion
    }
}
