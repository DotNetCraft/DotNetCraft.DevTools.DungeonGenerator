using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetCraft.DevTools.DungeonGenerator.Business.Graphs;
using DotNetCraft.DevTools.DungeonGenerator.Core.Graphs;
using DotNetCraft.DevTools.DungeonGenerator.Core.Utils;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using NSubstitute.ReceivedExtensions;

namespace DotNetCraft.DevTools.DungeonGenerator.Tests.Graphs
{
    [TestClass]
    public class GraphMinimumSpanningTreeTests
    {
        [TestMethod]
        public void MinimumSpanningTreeBuildTest()
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

            random.RandomNumber(1, 9).Returns(4);

            var tree = graphAlgorithms.Kruskals_MST(graph);
            Assert.AreEqual(8, tree.Vertices.Count);

            var edges = tree.Edges;
            Assert.AreEqual(4, edges[0].Vertex1);
            Assert.AreEqual(1, edges[1].Vertex1);
            Assert.AreEqual(2, edges[2].Vertex1);
            Assert.AreEqual(3, edges[3].Vertex1);
            Assert.AreEqual(6, edges[4].Vertex1);
            Assert.AreEqual(5, edges[5].Vertex1);
            Assert.AreEqual(7, edges[6].Vertex1);

            Assert.AreEqual(1, edges[0].Vertex2);
            Assert.AreEqual(2, edges[1].Vertex2);
            Assert.AreEqual(3, edges[2].Vertex2);
            Assert.AreEqual(6, edges[3].Vertex2);
            Assert.AreEqual(5, edges[4].Vertex2);
            Assert.AreEqual(7, edges[5].Vertex2);
            Assert.AreEqual(8, edges[6].Vertex2);

            random.Received(1).RandomNumber(1, 9);
        }

        [TestMethod]
        public void MinimumSpanningTreeBuild2Test()
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

            var tree = graphAlgorithms.Kruskals_MST(graph, 4);
            Assert.AreEqual(8, tree.Vertices.Count);

            var edges = tree.Edges;
            Assert.AreEqual(4, edges[0].Vertex1);
            Assert.AreEqual(1, edges[1].Vertex1);
            Assert.AreEqual(2, edges[2].Vertex1);
            Assert.AreEqual(3, edges[3].Vertex1);
            Assert.AreEqual(6, edges[4].Vertex1);
            Assert.AreEqual(5, edges[5].Vertex1);
            Assert.AreEqual(7, edges[6].Vertex1);

            Assert.AreEqual(1, edges[0].Vertex2);
            Assert.AreEqual(2, edges[1].Vertex2);
            Assert.AreEqual(3, edges[2].Vertex2);
            Assert.AreEqual(6, edges[3].Vertex2);
            Assert.AreEqual(5, edges[4].Vertex2);
            Assert.AreEqual(7, edges[5].Vertex2);
            Assert.AreEqual(8, edges[6].Vertex2);

            random.DidNotReceive().RandomNumber(Arg.Any<int>(), Arg.Any<int>());
        }
    }
}
