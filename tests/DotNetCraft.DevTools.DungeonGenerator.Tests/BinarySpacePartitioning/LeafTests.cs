using System;
using System.Collections.Generic;
using System.Text;
using DotNetCraft.DevTools.DungeonGenerator.Core.BinarySpacePartitioning;
using DotNetCraft.DevTools.DungeonGenerator.Core.Geometry;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotNetCraft.DevTools.DungeonGenerator.Tests.BinarySpacePartitioning
{
    [TestClass]
    public class LeafTests
    {
        [TestMethod]
        public void SplitHorizontalTest()
        {
            var bounds = new Rect(0, 0, 10, 22);
            var leaf = new Leaf(bounds);

            leaf.SplitHorizontal(10);

            Assert.AreEqual(bounds.X, leaf.LeftChild.Bounds.X);
            Assert.AreEqual(bounds.Width, leaf.LeftChild.Bounds.Width);
            Assert.AreEqual(0, leaf.LeftChild.Bounds.Y);
            Assert.AreEqual(10, leaf.LeftChild.Bounds.Height);

            Assert.AreEqual(bounds.X, leaf.RightChild.Bounds.X);
            Assert.AreEqual(bounds.Width, leaf.RightChild.Bounds.Width);
            Assert.AreEqual(10, leaf.RightChild.Bounds.Y);
            Assert.AreEqual(12, leaf.RightChild.Bounds.Height);
        }

        [TestMethod]
        public void SplitVerticalTest()
        {
            var bounds = new Rect(0, 0, 12, 20);
            var leaf = new Leaf(bounds);

            leaf.SplitVertical(5);

            Assert.AreEqual(bounds.Y, leaf.LeftChild.Bounds.Y);
            Assert.AreEqual(bounds.Height, leaf.LeftChild.Bounds.Height);
            Assert.AreEqual(0, leaf.LeftChild.Bounds.X);
            Assert.AreEqual(5, leaf.LeftChild.Bounds.Width);

            Assert.AreEqual(bounds.Y, leaf.RightChild.Bounds.Y);
            Assert.AreEqual(bounds.Height, leaf.RightChild.Bounds.Height);
            Assert.AreEqual(5, leaf.RightChild.Bounds.X);
            Assert.AreEqual(7, leaf.RightChild.Bounds.Width);
        }
    }
}
