namespace DotNetCraft.DevTools.DungeonGenerator.Core.Utils
{
    public interface IRandom
    {
        public float Random();
        public int RandomNumber(int minInclusive, int maxExclusive);
    }
}
