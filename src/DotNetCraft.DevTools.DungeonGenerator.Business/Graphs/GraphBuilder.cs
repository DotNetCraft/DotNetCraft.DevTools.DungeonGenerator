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

        public Graph Build(List<Leaf> leaves)
        {
            if (leaves == null)
                throw new ArgumentNullException(nameof(leaves));

            _logger.LogDebug($"Building Graph for {leaves.Count} leaves...");

            var graph = new Graph();

            var activeLeaves = leaves.Where(x => x.ActiveLeaf).ToList();

            for (var i = 0; i < activeLeaves.Count - 1; i++)
            {
                var leafA = activeLeaves[i];
                for (int j = i + 1; j < activeLeaves.Count; j++)
                {
                    var leafB = activeLeaves[j];

                    var hasCommonSide = _rectGeometry.TryGetCommonSide(leafA.Bounds, leafB.Bounds, out var commonSide);
                    if (hasCommonSide)
                    {
                        graph.AddEdge(i, j);
                        //TODO: Save geometry from commonSide
                    }
                }
            }
            
            _logger.LogDebug($"The Graph was built: {graph.Vertices.Count}");
            return graph;
        }

        #endregion
    }
}
