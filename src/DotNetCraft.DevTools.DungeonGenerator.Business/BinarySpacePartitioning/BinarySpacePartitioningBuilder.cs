using System;
using System.Collections.Generic;
using System.Text;
using DotNetCraft.DevTools.DungeonGenerator.Core.BinarySpacePartitioning;
using DotNetCraft.DevTools.DungeonGenerator.Core.Geometry;
using DotNetCraft.DevTools.DungeonGenerator.Core.Utils;
using Microsoft.Extensions.Logging;

namespace DotNetCraft.DevTools.DungeonGenerator.Business.BinarySpacePartitioning
{
    public class BinarySpacePartitioningBuilder : IBinarySpacePartitioningBuilder
    {
        private readonly IRandom _random;
        private readonly ILogger<BinarySpacePartitioningBuilder> _logger;

        public BinarySpacePartitioningBuilder(IRandom random, ILogger<BinarySpacePartitioningBuilder> logger)
        {
            _random = random ?? throw new ArgumentNullException(nameof(random));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #region Implementation of IBinarySpacePartitioningBuilder

        public List<Leaf> Build(Rect mainRect, BuildConfig buildConfig)
        {
            if (mainRect == null)
                throw new ArgumentNullException(nameof(mainRect));
            if (buildConfig == null)
                throw new ArgumentNullException(nameof(buildConfig));

            _logger.LogDebug($"Building BSP ({buildConfig})...");

            var minSize = buildConfig.MinSize;

            var rootLeaf = new Leaf(mainRect);
            var leafQueue = new Queue<Leaf>();
            leafQueue.Enqueue(rootLeaf);
            
            var result = new List<Leaf> { rootLeaf };

            while (leafQueue.Count > 0)
            {
                var leaf = leafQueue.Dequeue();
                var width = leaf.Bounds.Width;
                var height = leaf.Bounds.Height;

                if (width <= buildConfig.MaxSize || height <= buildConfig.MinSize)
                {
                    if (buildConfig.SkipRoomFunc != null)
                    {
                        var skipRoom = buildConfig.SkipRoomFunc(mainRect);
                        if (skipRoom)
                        {
                            leaf.ActiveLeaf = true;
                            continue;
                        }
                    }
                }

                if (width <= 2*minSize && height <= 2*minSize)
                {
                    leaf.ActiveLeaf = true;
                    continue;
                }

                if (width > height)
                {
                    var deltaWidth = _random.RandomNumber(minSize, width - minSize);
                    leaf.SplitVertical(deltaWidth);
                }
                else
                {
                    var deltaHeight = _random.RandomNumber(minSize, height - minSize);
                    leaf.SplitHorizontal(deltaHeight);
                }

                leafQueue.Enqueue(leaf.LeftChild);
                leafQueue.Enqueue(leaf.RightChild);

                result.Add(leaf.LeftChild);
                result.Add(leaf.RightChild);
            }

            _logger.LogDebug("BSP was built");

            return result;
        }

        #endregion
    }
}
