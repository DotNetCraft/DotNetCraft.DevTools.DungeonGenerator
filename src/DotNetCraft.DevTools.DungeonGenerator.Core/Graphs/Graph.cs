using System;
using System.Collections.Generic;

namespace DotNetCraft.DevTools.DungeonGenerator.Core.Graphs
{
    public class Graph
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("N");

        public List<int> Vertices { get; } = new List<int>();
        public List<Edge> Edges { get; } = new List<Edge>();

        public void AddEdge(int vertexA, int vertexB, int weight = -1)
        {
            if (Vertices.Contains(vertexA) == false)
                Vertices.Add(vertexA);
            if (Vertices.Contains(vertexB) == false)
                Vertices.Add(vertexB);

            var edge = new Edge
            {
                Vertex1 = vertexA,
                Vertex2 = vertexB,
                Weight = weight
            };
            Edges.Add(edge);
        }
    }
}
