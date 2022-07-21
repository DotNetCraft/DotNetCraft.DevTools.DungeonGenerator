namespace DotNetCraft.DevTools.DungeonGenerator.Core.Graphs
{
    public interface IGraphAlgorithms
    {
        int CalculateMinDistance(Graph graph, int rootVertex);
        Graph Kruskals_MST(Graph graph, int? rootVertex = null);
    }
}
