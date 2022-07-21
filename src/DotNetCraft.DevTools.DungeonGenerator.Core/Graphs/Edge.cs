namespace DotNetCraft.DevTools.DungeonGenerator.Core.Graphs
{
    public class Edge
    {
        public int Vertex1 { get; set; }
        public int Vertex2 { get; set; }
        public int Weight { get; set; } = -1;

        #region Overrides of Object

        public override string ToString()
        {
            return $"{Vertex1}->{Vertex2} (Weight: {Weight})";
        }

        #endregion
    }
}
