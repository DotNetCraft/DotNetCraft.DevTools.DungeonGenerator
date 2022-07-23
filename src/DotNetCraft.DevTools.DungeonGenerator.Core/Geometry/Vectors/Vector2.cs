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

        public static Vector2 operator *(Vector2 a, int scale)
        {
            return new Vector2(scale * a.X, scale * a.Y);
        }

        #region Overrides of Object

        public override string ToString()
        {
            return $"x: {X}; y: {Y}";
        }

        #endregion
    }
}
