using System;
using DotNetCraft.DevTools.DungeonGenerator.Core.Utils;

namespace DotNetCraft.DevTools.DungeonGenerator.Business.Utils
{
    public class SimpleRandom: IRandom
    {
        private readonly Random _random = new Random();

        #region Implementation of IRandom

        public float Random()
        {
            return (float)_random.NextDouble();
        }

        public int RandomNumber(int minInclusive, int maxExclusive)
        {
            return _random.Next(minInclusive, maxExclusive);
        }

        #endregion
    }
}
