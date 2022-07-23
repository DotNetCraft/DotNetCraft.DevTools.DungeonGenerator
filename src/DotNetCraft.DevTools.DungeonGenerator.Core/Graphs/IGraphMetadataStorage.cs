using System;
using System.Collections.Generic;
using System.Text;
using DotNetCraft.DevTools.DungeonGenerator.Core.Geometry;

namespace DotNetCraft.DevTools.DungeonGenerator.Core.Graphs
{
    public interface IGraphMetadataStorage
    {
        void AddVertexGeometry(string graphId, int vertex, Rect rect);
        Rect GetVertexGeometry(string graphId, int vertex);
        void AddEdgeCommonSide(string graphId, int vertex1, int vertex2, LineSegment lineSegment);
        LineSegment GetEdgeCommonSide(string graphId, int vertex1, int vertex2);
        void RemoveGraphMetadata(string graphId);
    }
}
