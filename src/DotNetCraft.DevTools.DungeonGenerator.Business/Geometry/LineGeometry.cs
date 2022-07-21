using System;
using DotNetCraft.DevTools.DungeonGenerator.Core.Geometry;
using Microsoft.Extensions.Logging;

namespace DotNetCraft.DevTools.DungeonGenerator.Business.Geometry
{
    public class LineGeometry: ILineGeometry
    {
        private readonly ILogger<LineGeometry> _logger;

        public LineGeometry(ILogger<LineGeometry> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #region Implementation of ILineGeometry

        public bool CheckLine(float a1, float a2, float a3, float a4)
        {
            var res = (a3 <= a1 && a1 <= a4) ||
                      (a3 <= a2 && a2 <= a4) ||
                      (a1 <= a3 && a3 <= a2) ||
                      (a1 <= a4 && a4 <= a2);
            return res;
        }

        public bool TryGetCommonLineSegment(float a1, float a2, float a3, float a4, out float a, out float b)
        {
            if (a3 <= a1 && a1 <= a4)
            {
                a = a1;
                b = (a2 < a4) ? a2 : a4;
                return true;
            }

            if (a3 <= a2 && a2 <= a4)
            {
                b = a2;
                a = (a1 > a3) ? a1 : a3;
                return true;
            }

            if (a1 <= a3 && a3 <= a2)
            {
                a = a3;
                b = (a2 < a4) ? a2 : a4;
                return true;
            }

            if (a1 <= a4 && a4 <= a2)
            {
                a = a4;
                b = (a1 > a3) ? a1 : a3;
                return true;
            }

            a = -1;
            b = -1;
            return false;
        }

        #endregion
    }
}
