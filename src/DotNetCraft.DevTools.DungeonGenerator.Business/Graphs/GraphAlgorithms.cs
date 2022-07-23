using System;
using System.Collections.Generic;
using System.Linq;
using DotNetCraft.DevTools.DungeonGenerator.Core.Graphs;
using DotNetCraft.DevTools.DungeonGenerator.Core.Utils;
using Microsoft.Extensions.Logging;

namespace DotNetCraft.DevTools.DungeonGenerator.Business.Graphs
{
    public class GraphAlgorithms: IGraphAlgorithms
    {
        private readonly IRandom _random;
        private readonly ILogger<GraphAlgorithms> _logger;

        public GraphAlgorithms(IRandom random, ILogger<GraphAlgorithms> logger)
        {
            _random = random ?? throw new ArgumentNullException(nameof(random));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #region Implementation of IGraphAlgorithms

        public int CalculateMinDistance(Graph graph, int rootVertex)
        {
            graph.Edges.ForEach(x => x.Weight = -1);

            var distances = new int[graph.Vertices.Count + 1]; //because vertices start from 1
            for (var i = 0; i < distances.Length; i++)
            {
                distances[i] = -1;
            }
            distances[rootVertex] = 0;

            var queue = new Queue<int>();
            queue.Enqueue(rootVertex);

            var currentDistance = 1;
            var queueCount = queue.Count;
            var distanceVerticesCount = 1;

            while (queue.Count > 0)
            {
                var currentVertex = queue.Dequeue();
                queueCount--;

                foreach (var edge in graph.Edges)
                {
                    if (currentVertex == edge.Vertex1)
                    {
                        if (distances[edge.Vertex2] == -1)
                        {
                            distanceVerticesCount++;
                            distances[edge.Vertex2] = currentDistance;
                            edge.Weight = currentDistance;
                            queue.Enqueue(edge.Vertex2);
                        }
                    }

                    if (currentVertex == edge.Vertex2)
                    {
                        if (distances[edge.Vertex1] == -1)
                        {
                            distanceVerticesCount++;
                            distances[edge.Vertex1] = currentDistance;
                            edge.Weight = currentDistance;
                            queue.Enqueue(edge.Vertex1);
                        }
                    }
                }

                if (queueCount == 0)
                {
                    if (distanceVerticesCount == graph.Vertices.Count)
                    {
                        foreach (var edge in graph.Edges)
                        {
                            if (edge.Weight == -1)
                                edge.Weight = currentDistance;
                        }
                        break;
                    }

                    currentDistance++;
                    queueCount = queue.Count;
                }
            }

            return currentDistance;
        }

        public Graph Kruskals_MST(Graph graph, int? rootVertex = null)
        {
            var result = new Graph();

            var markedVertices = new int[graph.Vertices.Count + 1];//Vertices start from 1
            for (var i = 0; i < markedVertices.Length; i++)
            {
                markedVertices[i] = -1;
            }

            var activeVertex = rootVertex ?? _random.RandomNumber(1, graph.Vertices.Count + 1);
            markedVertices[activeVertex] = 1;

            var queue = new Queue<int>();
            queue.Enqueue(activeVertex);

            var weight = 1;
            do
            {
                var nextQueue = new Queue<int>();
                while (queue.Count > 0)
                {
                    var currentVertex = queue.Dequeue();
                    var connectedEdges = graph.Edges.Where(x => x.Vertex1 == currentVertex || x.Vertex2 == currentVertex).ToList();

                    foreach (var edge in connectedEdges)
                    {
                        if (edge.Vertex1 == currentVertex && markedVertices[edge.Vertex2] == -1)
                        {
                            markedVertices[edge.Vertex2] = 1;
                            nextQueue.Enqueue(edge.Vertex2);
                            result.AddEdge(edge.Vertex1, edge.Vertex2, weight);
                        }

                        if (edge.Vertex2 == currentVertex && markedVertices[edge.Vertex1] == -1)
                        {
                            markedVertices[edge.Vertex1] = 1;
                            nextQueue.Enqueue(edge.Vertex1);
                            result.AddEdge(edge.Vertex2, edge.Vertex1, weight);
                        }
                    }
                }

                queue = nextQueue;
            } while (queue.Count > 0);

            return result;
        }

        #endregion
    }
}
