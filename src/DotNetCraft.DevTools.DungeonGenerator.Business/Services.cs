using DotNetCraft.DevTools.DungeonGenerator.Business.BinarySpacePartitioning;
using DotNetCraft.DevTools.DungeonGenerator.Business.Geometry;
using DotNetCraft.DevTools.DungeonGenerator.Business.Graphs;
using DotNetCraft.DevTools.DungeonGenerator.Business.Utils;
using DotNetCraft.DevTools.DungeonGenerator.Core.BinarySpacePartitioning;
using DotNetCraft.DevTools.DungeonGenerator.Core.Geometry;
using DotNetCraft.DevTools.DungeonGenerator.Core.Graphs;
using DotNetCraft.DevTools.DungeonGenerator.Core.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetCraft.DevTools.DungeonGenerator.Business
{
    public static class Services
    {
        public static IServiceCollection UseDungeonGenerator(this IServiceCollection services)
        {
            services.AddSingleton<IRandom, SimpleRandom>();

            services.AddSingleton<ILineGeometry, LineGeometry>();
            services.AddSingleton<IRectGeometry, RectGeometry>();
            services.AddSingleton<IGraphAlgorithms, GraphAlgorithms>();

            services.AddSingleton<IGraphMetadataStorage, GraphMetadataStorage>();
            services.AddSingleton<IGraphBuilder, GraphBuilder>();

            services.AddSingleton<IBinarySpacePartitioningBuilder, BinarySpacePartitioningBuilder>();
            return services;
        }
    }
}
