using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetCraft.DevTools.DungeonGenerator.Business.Rooms;
using DotNetCraft.DevTools.DungeonGenerator.Core.Geometry;
using DotNetCraft.DevTools.DungeonGenerator.Core.Graphs;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotNetCraft.DevTools.DungeonGenerator.Tests.Rooms
{
    [TestClass]
    public class RoomsMapBuilderTests
    {
        [TestMethod]
        public void RoomsMapBuilderTest()
        {
            var logger = new NullLogger<RoomsMapBuilder>();
            var roomsMapBuilder = new RoomsMapBuilder(logger);

            var globalRect = new Rect(0, 0, 20, 20);
            var graphDoc = new GraphDoc();

            graphDoc.Graph.AddEdge(1,2);
            graphDoc.Metadata.Geometries[1] = new Rect(0, 0, 10, 10);
            graphDoc.Metadata.Geometries[2] = new Rect(10, 0, 5, 5);

            var map = roomsMapBuilder.Build(graphDoc, globalRect);
            Assert.IsNotNull(map);
        }
    }
}
