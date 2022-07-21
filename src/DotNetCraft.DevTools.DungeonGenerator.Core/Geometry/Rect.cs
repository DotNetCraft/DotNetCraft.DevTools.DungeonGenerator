using DotNetCraft.DevTools.DungeonGenerator.Core.Geometry.Vectors;

namespace DotNetCraft.DevTools.DungeonGenerator.Core.Geometry
{
    public class Rect
    {
        public float X { get; set; }
        public float Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public Vector2 Center { get; }

        public Rect(float x, float y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;

            var cx =  x+(width / 2.0f);
            var cy = y + (height / 2.0f);

            Center = new Vector2(cx, cy);
        }

        #region Overrides of Object

        public override string ToString()
        {
            return $"x: {X}; y: {Y}; [{Width}x{Height}; center: {Center}]";
        }

        #endregion
    }
}
