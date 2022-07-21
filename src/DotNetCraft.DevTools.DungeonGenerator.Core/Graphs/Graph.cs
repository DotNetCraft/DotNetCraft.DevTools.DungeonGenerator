using System.Collections.Generic;

namespace DotNetCraft.DevTools.DungeonGenerator.Core.Graphs
{
    public class Graph
    {
        public List<int> Vertices { get; } = new List<int>();
        public List<Edge> Edges { get; } = new List<Edge>();

        private void RefreshData()
        {
            Edges.ForEach(x => x.Weight = -1);
        }

        public void AddEdge(int vertexA, int vertexB)
        {
            if (Vertices.Contains(vertexA) == false)
                Vertices.Add(vertexA);
            if (Vertices.Contains(vertexB) == false)
                Vertices.Add(vertexB);

            var edge = new Edge
            {
                Vertex1 = vertexA,
                Vertex2 = vertexB,
                Weight = -1
            };
            Edges.Add(edge);

            RefreshData();
        }
    }
}
