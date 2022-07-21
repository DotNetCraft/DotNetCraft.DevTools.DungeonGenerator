namespace DotNetCraft.DevTools.DungeonGenerator.Core.Utils
{
    public interface IRandom
    {
        float Random();
        int RandomNumber(int minInclusive, int maxExclusive);
    }
}
