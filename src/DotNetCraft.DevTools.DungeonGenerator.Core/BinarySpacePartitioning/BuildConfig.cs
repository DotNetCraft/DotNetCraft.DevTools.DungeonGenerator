namespace DotNetCraft.DevTools.DungeonGenerator.Core.BinarySpacePartitioning
{
    public class BuildConfig
    {
        public int MinSize { get; set; }
        public int MaxSize { get; set; }

        #region Overrides of Object

        public override string ToString()
        {
            return $"Min: {MinSize}, Max: {MaxSize}";
        }

        #endregion
    }
}
