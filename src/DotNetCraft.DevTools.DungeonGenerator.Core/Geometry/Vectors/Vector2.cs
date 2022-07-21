namespace DotNetCraft.DevTools.DungeonGenerator.Core.Geometry.Vectors
{
    public class Vector2
    {
        public float X { get; set; }
        public float Y { get; set; }

        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        #region Overrides of Object

        public override string ToString()
        {
            return $"x: {X}; y: {Y}";
        }

        #endregion
    }
}
