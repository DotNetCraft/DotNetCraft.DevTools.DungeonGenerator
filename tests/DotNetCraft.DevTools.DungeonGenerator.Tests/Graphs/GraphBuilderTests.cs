using System.Collections.Generic;
using System.Linq;
using DotNetCraft.DevTools.DungeonGenerator.Business.Geometry;
using DotNetCraft.DevTools.DungeonGenerator.Business.Graphs;
using DotNetCraft.DevTools.DungeonGenerator.Core.BinarySpacePartitioning;
using DotNetCraft.DevTools.DungeonGenerator.Core.Geometry;
using DotNetCraft.DevTools.DungeonGenerator.Core.Graphs;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotNetCraft.DevTools.DungeonGenerator.Tests.Graphs
{
    [TestClass]
    public class GraphBuilderTests
    {
        [TestMethod]
        public void GraphBuilderTest()
        {
            var logger = new NullLogger<IGraphBuilder>();
            var rectGeometryLogger = new NullLogger<RectGeometry>();
            var lineGeometryLogger = new NullLogger<LineGeometry>();
            var graphMetadataStorageLogger = new NullLogger<GraphMetadataStorage>();

            var lineGeometry = new LineGeometry(lineGeometryLogger);
            var rectGeometry = new RectGeometry(lineGeometry, rectGeometryLogger);
            var graphMetadataStorage = new GraphMetadataStorage(graphMetadataStorageLogger);
            var graphBuilder = new GraphBuilder(graphMetadataStorage, rectGeometry, logger);

            List<Leaf> leaves = new List<Leaf>();

            var mainRect = new Rect(0, 0, 21, 23);
            var mainLeaf = new Leaf(mainRect);
            leaves.Add(mainLeaf);

            mainLeaf.SplitHorizontal(4);
            leaves.Add(mainLeaf.LeftChild);
            leaves.Add(mainLeaf.RightChild);

            var leftChild = mainLeaf.LeftChild;
            leftChild.SplitVertical(6);
            leaves.Add(leftChild.LeftChild);
            leaves.Add(leftChild.RightChild);
            leftChild.LeftChild.ActiveLeaf = true;
            leftChild.RightChild.ActiveLeaf = true;

            var rightChild = mainLeaf.RightChild;
            rightChild.SplitVertical(14);
            leaves.Add(rightChild.LeftChild);
            leaves.Add(rightChild.RightChild);
            rightChild.LeftChild.ActiveLeaf = true;
            rightChild.RightChild.ActiveLeaf = true;
            
            var graph = graphBuilder.Build(leaves);

            Assert.AreEqual(4, graph.Vertices.Count);
            for (var i = 0; i < 4; i++)
                Assert.AreEqual(i, graph.Vertices.ElementAt(i));

            Assert.AreEqual(5, graph.Edges.Count);
        }
    }
}
