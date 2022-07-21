using System;
using DotNetCraft.DevTools.DungeonGenerator.Core.Geometry;
using DotNetCraft.DevTools.DungeonGenerator.Core.Geometry.Vectors;
using Microsoft.Extensions.Logging;

namespace DotNetCraft.DevTools.DungeonGenerator.Business.Geometry
{
    public class RectGeometry: IRectGeometry
    {
        private const double Eps = 0.0000001f;

        private readonly ILineGeometry _lineGeometry;
        private readonly ILogger<RectGeometry> _logger;


        public RectGeometry(ILineGeometry lineGeometry, ILogger<RectGeometry> logger)
        {
            _lineGeometry = lineGeometry ?? throw new ArgumentNullException(nameof(lineGeometry));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #region Implementation of IRectGeometry

        public bool TryGetCommonSide(Rect r1, Rect r2, out LineSegment lineSegment)
        {
            lineSegment = null;

            if (Math.Abs(r1.X + r1.Width - r2.X) < Eps || Math.Abs(r1.X - (r2.X + r2.Width)) < Eps)
            {
                var res = _lineGeometry.TryGetCommonLineSegment(r1.Y, r1.Y + r1.Height, r2.Y, r2.Y + r2.Height, out var y1, out var y2);
                if (res)
                {
                    var x = Math.Max(r1.X, r2.X);
                    Vector2 start = new Vector2(x, y1);
                    Vector2 end = new Vector2(x, y2);
                    lineSegment = new LineSegment(start, end);
                }

                return res;
            }

            if (Math.Abs(r1.Y + r1.Height - r2.Y) < Eps || Math.Abs(r1.Y - (r2.Y + r2.Height)) < Eps)
            {
                var res = _lineGeometry.TryGetCommonLineSegment(r1.X, r1.X + r1.Width, r2.X, r2.X + r2.Width, out var x1, out var x2);
                if (res)
                {
                    var y = Math.Max(r1.Y, r2.Y);
                    Vector2 start = new Vector2(x1, y);
                    Vector2 end = new Vector2(x2, y);
                    lineSegment = new LineSegment(start, end);
                }

                return res;
            }

            return false;
        }

        #endregion
    }
}
