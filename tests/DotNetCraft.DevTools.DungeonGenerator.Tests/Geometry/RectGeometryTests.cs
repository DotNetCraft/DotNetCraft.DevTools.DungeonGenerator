using System;
using System.Collections.Generic;
using System.Text;
using DotNetCraft.DevTools.DungeonGenerator.Business.Geometry;
using DotNetCraft.DevTools.DungeonGenerator.Core.Geometry;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotNetCraft.DevTools.DungeonGenerator.Tests.Geometry
{
    [TestClass]
    public class RectGeometryTests
    {
        [DataTestMethod]
        [DataRow(0, 2, 2, 2, 2, 4)]
        [DataRow(4, 2, 4, 2, 4, 4)]
        [DataRow(2, 0, 2, 2, 4, 2)]
        [DataRow(2, 4, 2, 4, 4, 4)]
        public void RectGeometryPositiveTest(float x, float y, float startX, float startY, float endX, float endY)
        {
            var nullLogger = new NullLogger<LineGeometry>();
            var lineGeometry = new LineGeometry(nullLogger);
            var logger = new NullLogger<RectGeometry>();

            var rectGeometry = new RectGeometry(lineGeometry, logger);

            var mainRoot = new Rect(2, 2, 2, 2);
            var rect = new Rect(x, y, 2, 2);
            var res = rectGeometry.TryGetCommonSide(mainRoot, rect, out var lineSegment);

            Assert.IsTrue(res);
            Assert.AreEqual(startX, lineSegment.Start.X);
            Assert.AreEqual(startY, lineSegment.Start.Y);
            Assert.AreEqual(endX, lineSegment.End.X);
            Assert.AreEqual(endY, lineSegment.End.Y);
        }
    }
}
