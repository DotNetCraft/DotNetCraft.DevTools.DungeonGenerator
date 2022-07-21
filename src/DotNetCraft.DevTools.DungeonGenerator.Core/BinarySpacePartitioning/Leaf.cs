using System;
using DotNetCraft.DevTools.DungeonGenerator.Core.Geometry;

namespace DotNetCraft.DevTools.DungeonGenerator.Core.BinarySpacePartitioning
{
    public class Leaf
    {
        public Rect Bounds { get; private set; }
        public Leaf RightChild { get; private set; }
        public Leaf LeftChild { get; private set; }

        public bool ActiveLeaf { get; set; }

        public Leaf(Rect bounds)
        {
            Bounds = bounds ?? throw new ArgumentNullException(nameof(bounds));
        }

        public void SplitVertical(int deltaWidth)
        {
            var leftRect = new Rect(Bounds.X, Bounds.Y, deltaWidth, Bounds.Height);
            var rightRect = new Rect(Bounds.X + deltaWidth, Bounds.Y, Bounds.Width - deltaWidth, Bounds.Height);

            LeftChild = new Leaf(leftRect);
            RightChild = new Leaf(rightRect);
        }

        public void SplitHorizontal(int deltaHeight)
        {
            var leftRect = new Rect(Bounds.X, Bounds.Y, Bounds.Width, deltaHeight);
            var rightRect = new Rect(Bounds.X, Bounds.Y + deltaHeight, Bounds.Width, Bounds.Height - deltaHeight);

            LeftChild = new Leaf(leftRect);
            RightChild = new Leaf(rightRect);
        }

        #region Overrides of Object

        public override string ToString()
        {
            return $"{Bounds} [Active: {ActiveLeaf}]";
        }

        #endregion
    }
}
