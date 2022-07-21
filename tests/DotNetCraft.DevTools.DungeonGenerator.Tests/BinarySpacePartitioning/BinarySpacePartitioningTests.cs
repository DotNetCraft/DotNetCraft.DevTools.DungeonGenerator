using System.Collections.Generic;
using DotNetCraft.DevTools.DungeonGenerator.Business.BinarySpacePartitioning;
using DotNetCraft.DevTools.DungeonGenerator.Business.Utils;
using DotNetCraft.DevTools.DungeonGenerator.Core.BinarySpacePartitioning;
using DotNetCraft.DevTools.DungeonGenerator.Core.Geometry;
using DotNetCraft.DevTools.DungeonGenerator.Core.Utils;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace DotNetCraft.DevTools.DungeonGenerator.Tests.BinarySpacePartitioning
{
    [TestClass]
    public class BinarySpacePartitioningTests
    {
        [TestMethod]
        public void BinarySpacePartitioningBuildTest()
        {
            var logger = new NullLogger<BinarySpacePartitioningBuilder>();
            var random = Substitute.For<IRandom>();
            var builder = new BinarySpacePartitioningBuilder(random, logger);

            var queue = new Queue<int>();
            queue.Enqueue(6);
            queue.Enqueue(5);
            queue.Enqueue(5);
            //random.WhenForAnyArgs(x=>x.Random()).Do();
            random.RandomNumber(Arg.Any<int>(), Arg.Any<int>())
                .ReturnsForAnyArgs(x => queue.Dequeue());

            var mainRect = new Rect(0, 0, 10, 10);
            var buildConfig = new BuildConfig
            {
                MinSize = 3,
                MaxSize = 10
            };
            var leafs = builder.Build(mainRect, buildConfig);

            Assert.IsNotNull(leafs);
            Assert.AreEqual(7, leafs.Count);

            var square = 0.0f;
            foreach (var leaf in leafs)
            {
                Assert.IsTrue(leaf.Bounds.Width >= 3);
                Assert.IsTrue(leaf.Bounds.Height >= 3);

                if (leaf.ActiveLeaf)
                {
                    square += leaf.Bounds.Width * leaf.Bounds.Height;
                }
            }

            var expected = mainRect.Width * mainRect.Height;
            Assert.AreEqual(expected, square);
        }

        [DataTestMethod]
        [DataRow(10, 10, 100)]
        [DataRow(30, 30, 300)]
        [DataRow(100, 100, 2000)]
        public void BinarySpacePartitioningBuildBigTest(int width, int height, int maxLeaves)
        {
            var logger = new NullLogger<BinarySpacePartitioningBuilder>();
            var random = new SimpleRandom();
            var builder = new BinarySpacePartitioningBuilder(random, logger);

            var mainRect = new Rect(0, 0, width, height);
            var buildConfig = new BuildConfig
            {
                MinSize = 3,
                MaxSize = 10
            };
            var leafs = builder.Build(mainRect, buildConfig);

            Assert.IsNotNull(leafs);
            Assert.IsTrue(leafs.Count < maxLeaves);

            var square = 0.0f;
            foreach (var leaf in leafs)
            {
                Assert.IsTrue(leaf.Bounds.Width >= 3);
                Assert.IsTrue(leaf.Bounds.Height >= 3);

                if (leaf.ActiveLeaf)
                {
                    square += leaf.Bounds.Width * leaf.Bounds.Height;
                }
            }

            var expected = mainRect.Width * mainRect.Height;
            Assert.AreEqual(expected, square);
        }
    }
}
