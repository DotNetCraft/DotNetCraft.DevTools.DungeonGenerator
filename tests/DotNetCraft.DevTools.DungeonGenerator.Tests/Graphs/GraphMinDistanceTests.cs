using DotNetCraft.DevTools.DungeonGenerator.Business.Graphs;
using DotNetCraft.DevTools.DungeonGenerator.Core.Graphs;
using DotNetCraft.DevTools.DungeonGenerator.Core.Utils;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace DotNetCraft.DevTools.DungeonGenerator.Tests.Graphs
{
    [TestClass]
    public class GraphMinDistanceTests
    {
        [TestMethod]
        public void FindMinDistanceTest()
        {
            var graph = new Graph();

            graph.AddEdge(1,2);
            graph.AddEdge(1,4);
            graph.AddEdge(2,3);
            graph.AddEdge(2,5);
            graph.AddEdge(3,6);
            graph.AddEdge(4,5);
            graph.AddEdge(4,7);
            graph.AddEdge(5,6);
            graph.AddEdge(5,7);
            graph.AddEdge(6,8);
            graph.AddEdge(7,8);

            var random = Substitute.For<IRandom>();
            var logger = new NullLogger<GraphAlgorithms>();
            var graphAlgorithms = new GraphAlgorithms(random, logger);

            var distance = graphAlgorithms.CalculateMinDistance(graph, 1);
            Assert.AreEqual(3, distance);

            foreach (var edge in graph.Edges)
            {
                Assert.IsTrue(edge.Weight > 0 && edge.Weight <= distance);
            }
        }

        [TestMethod]
        public void FindMinDistance2Test()
        {
            var graph = new Graph();

            graph.AddEdge(1, 2);
            graph.AddEdge(1, 4);
            graph.AddEdge(2, 3);
            graph.AddEdge(2, 5);
            graph.AddEdge(3, 6);
            graph.AddEdge(4, 5);
            graph.AddEdge(4, 7);
            graph.AddEdge(5, 6);
            graph.AddEdge(5, 7);
            graph.AddEdge(7, 8);

            var random = Substitute.For<IRandom>();
            var logger = new NullLogger<GraphAlgorithms>();
            var graphAlgorithms = new GraphAlgorithms(random, logger);

            var distance = graphAlgorithms.CalculateMinDistance(graph, 1);
            Assert.AreEqual(3, distance);

            foreach (var edge in graph.Edges)
            {
                Assert.IsTrue(edge.Weight > 0 && edge.Weight <= distance);
            }
        }

        [TestMethod]
        public void FindMinDistance3Test()
        {
            var graph = new Graph();

            graph.AddEdge(3, 6);
            graph.AddEdge(4, 5);
            graph.AddEdge(1, 2);
            graph.AddEdge(1, 4);
            graph.AddEdge(2, 3);
            graph.AddEdge(2, 5);
            graph.AddEdge(5, 6);
            graph.AddEdge(5, 7);
            graph.AddEdge(4, 7);
            graph.AddEdge(6, 8);

            var random = Substitute.For<IRandom>();
            var logger = new NullLogger<GraphAlgorithms>();
            var graphAlgorithms = new GraphAlgorithms(random, logger);

            var distance = graphAlgorithms.CalculateMinDistance(graph, 1);
            Assert.AreEqual(4, distance);

            foreach (var edge in graph.Edges)
            {
                Assert.IsTrue(edge.Weight > 0 && edge.Weight <= distance);
            }
        }

        [TestMethod]
        public void FindMinDistance4Test()
        {
            var graph = new Graph();

            graph.AddEdge(1, 2);
            graph.AddEdge(1, 4);
            graph.AddEdge(2, 3);
            graph.AddEdge(2, 5);
            graph.AddEdge(3, 6);
            graph.AddEdge(4, 5);
            graph.AddEdge(4, 7);
            graph.AddEdge(5, 6);
            graph.AddEdge(5, 7);
            graph.AddEdge(6, 8);
            graph.AddEdge(7, 8);

            var random = Substitute.For<IRandom>();
            var logger = new NullLogger<GraphAlgorithms>();
            var graphAlgorithms = new GraphAlgorithms(random, logger);

            var distance = graphAlgorithms.CalculateMinDistance(graph, 5);
            Assert.AreEqual(2, distance);

            foreach (var edge in graph.Edges)
            {
                Assert.IsTrue(edge.Weight > 0 && edge.Weight <= distance);
            }
        }

        [TestMethod]
        public void FindMinDistance5Test()
        {
            var graph = new Graph();

            graph.AddEdge(1, 2);
            graph.AddEdge(1, 4);
            graph.AddEdge(2, 3);
            graph.AddEdge(2, 5);
            graph.AddEdge(3, 6);
            //graph.AddEdge(4, 5);
            graph.AddEdge(4, 7);
            graph.AddEdge(5, 6);
            //graph.AddEdge(5, 7);
            graph.AddEdge(6, 8);
            //graph.AddEdge(7, 8);

            var random = Substitute.For<IRandom>();
            var logger = new NullLogger<GraphAlgorithms>();
            var graphAlgorithms = new GraphAlgorithms(random, logger);

            var distance = graphAlgorithms.CalculateMinDistance(graph, 3);
            Assert.AreEqual(4, distance);

            foreach (var edge in graph.Edges)
            {
                Assert.IsTrue(edge.Weight > 0 && edge.Weight <= distance);
            }
        }
    }
}
