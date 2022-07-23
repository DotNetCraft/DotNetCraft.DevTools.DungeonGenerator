using System;
using System.Collections.Generic;
using DotNetCraft.DevTools.DungeonGenerator.Core.Geometry;
using DotNetCraft.DevTools.DungeonGenerator.Core.Graphs;
using Microsoft.Extensions.Logging;

namespace DotNetCraft.DevTools.DungeonGenerator.Business.Graphs
{
    public class GraphMetadataStorage: IGraphMetadataStorage
    {
        private readonly ILogger<GraphMetadataStorage> _logger;
        private readonly Dictionary<string, GraphMetadata> _graphMetadatas;

        public GraphMetadataStorage(ILogger<GraphMetadataStorage> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _graphMetadatas = new Dictionary<string, GraphMetadata>();
        }

        #region Implementation of IGraphMetadataStorage

        public void AddVertexGeometry(string graphId, int vertex, Rect rect)
        {
            var key = $"{graphId}";
            if (_graphMetadatas.TryGetValue(key, out var metadata) == false)
            {
                metadata = new GraphMetadata();
                _graphMetadatas[key] = metadata;
            }
            metadata.Geometries[vertex] = rect;
        }

        public Rect GetVertexGeometry(string graphId, int vertex)
        {
            var key = $"{graphId}";
            if (_graphMetadatas.TryGetValue(key, out var metadata))
                return metadata.Geometries[vertex];

            throw new ArgumentOutOfRangeException(nameof(graphId), $"There is no geometry for {graphId}->{vertex}");
        }

        public void AddEdgeCommonSide(string graphId, int vertex1, int vertex2, LineSegment lineSegment)
        {
            var key = $"{graphId}";
            if (_graphMetadatas.TryGetValue(key, out var metadata) == false)
            {
                metadata = new GraphMetadata();
                _graphMetadatas[key] = metadata;
            }

            key = $"{vertex1}->{vertex2}";
            metadata.LineSegments[key] = lineSegment;
        }

        public LineSegment GetEdgeCommonSide(string graphId, int vertex1, int vertex2)
        {
            var key = $"{graphId}";
            if (_graphMetadatas.TryGetValue(key, out var metadata))
            {
                key = $"{vertex1}->{vertex2}";
                return metadata.LineSegments[key];
            }

            throw new ArgumentOutOfRangeException(nameof(graphId),
                $"There is no line segment for {graphId}->{vertex1}-{vertex2}");
        }

        public void RemoveGraphMetadata(string graphId)
        {
            _graphMetadatas.Remove(graphId);
        }

        #endregion
    }
}
