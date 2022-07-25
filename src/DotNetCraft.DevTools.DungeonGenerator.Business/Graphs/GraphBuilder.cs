using System;
using System.Collections.Generic;
using System.Linq;
using DotNetCraft.DevTools.DungeonGenerator.Core.BinarySpacePartitioning;
using DotNetCraft.DevTools.DungeonGenerator.Core.Geometry;
using DotNetCraft.DevTools.DungeonGenerator.Core.Graphs;
using Microsoft.Extensions.Logging;

namespace DotNetCraft.DevTools.DungeonGenerator.Business.Graphs
{
    public class GraphBuilder : IGraphBuilder
    {
        private readonly IRectGeometry _rectGeometry;
        private readonly ILogger<IGraphBuilder> _logger;

        public GraphBuilder(IRectGeometry rectGeometry, ILogger<IGraphBuilder> logger)
        {
            _rectGeometry = rectGeometry ?? throw new ArgumentNullException(nameof(rectGeometry));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #region Implementation of IGraphBuilder

        public GraphDoc Build(List<Leaf> leaves)
        {
            if (leaves == null)
                throw new ArgumentNullException(nameof(leaves));

            _logger.LogDebug($"Building a graph for {leaves.Count} leaves...");

            var graphDoc = new GraphDoc();

            var activeLeaves = leaves.Where(x => x.ActiveLeaf).ToList();

            for (var vertex = 0; vertex < activeLeaves.Count; vertex++)
            {
                var leaf = activeLeaves[vertex];
                graphDoc.Metadata.Geometries[vertex] = leaf.Bounds;
            }

            for (var i = 0; i < activeLeaves.Count - 1; i++)
            {
                var leafA = activeLeaves[i];
                for (int j = i + 1; j < activeLeaves.Count; j++)
                {
                    var leafB = activeLeaves[j];

                    var hasCommonSide = _rectGeometry.TryGetCommonSide(leafA.Bounds, leafB.Bounds, out var commonSide);
                    if (hasCommonSide)
                    {
                        graphDoc.Graph.AddEdge(i, j);
                        var key = $"{i}<->{j}";
                        graphDoc.Metadata.LineSegments[key] = commonSide;
                    }
                }
            }
            
            _logger.LogDebug($"The Graph was built: {graphDoc.Graph.Vertices.Count}");
            return graphDoc;
        }

        #endregion
    }
}
