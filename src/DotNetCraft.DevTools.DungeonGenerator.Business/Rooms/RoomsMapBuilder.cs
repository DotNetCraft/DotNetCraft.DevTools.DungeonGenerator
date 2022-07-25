using System;
using System.Collections.Generic;
using System.Text;
using DotNetCraft.DevTools.DungeonGenerator.Core.Geometry;
using DotNetCraft.DevTools.DungeonGenerator.Core.Graphs;
using DotNetCraft.DevTools.DungeonGenerator.Core.Rooms;
using Microsoft.Extensions.Logging;

namespace DotNetCraft.DevTools.DungeonGenerator.Business.Rooms
{
    public class RoomsMapBuilder : IRoomsMapBuilder
    {
        private readonly ILogger<RoomsMapBuilder> _logger;

        public RoomsMapBuilder(ILogger<RoomsMapBuilder> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #region Implementation of IRoomsMapBuilder

        public RoomsMap Build(GraphDoc graphDoc, Rect globalRect)
        {
            var roomsMap = new RoomsMap(globalRect);
            foreach (var graphVertex in graphDoc.Graph.Vertices)
            {
                var vertexGeometry = graphDoc.Metadata.Geometries[graphVertex];
                var x = (int)vertexGeometry.X;
                var y = (int)vertexGeometry.Y;

                roomsMap.AddRoom(graphVertex, vertexGeometry);
            }

            return roomsMap;
        }

        #endregion
    }
}
