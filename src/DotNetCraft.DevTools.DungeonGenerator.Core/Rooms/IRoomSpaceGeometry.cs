using DotNetCraft.DevTools.DungeonGenerator.Core.Geometry;

namespace DotNetCraft.DevTools.DungeonGenerator.Core.Rooms
{
    public interface IRoomSpaceGeometry
    {
        void AddRoomSpace(int roomSpaceId, Rect rect);
    }
}
