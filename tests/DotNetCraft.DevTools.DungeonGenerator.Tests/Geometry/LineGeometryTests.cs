using DotNetCraft.DevTools.DungeonGenerator.Business.Geometry;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotNetCraft.DevTools.DungeonGenerator.Tests.Geometry
{
    [TestClass]
    public class LineGeometryTests
    {
        [DataTestMethod]
        [DataRow(0, 5, 4, 10, 4, 5)]
        [DataRow(5, 6, 4, 10, 5, 6)]
        [DataRow(4, 10, 0, 5, 4, 5)]
        [DataRow(4, 10, 5, 6, 5, 6)]
        public void LineGeometryPositiveTest(float a1, float a2, float a3, float a4, float expectA, float expectB)
        {
            var logger = new NullLogger<LineGeometry>();
            var lineGeometry = new LineGeometry(logger);

            var res = lineGeometry.TryGetCommonLineSegment(a1, a2, a3, a4, out var a, out var b);
            Assert.IsTrue(res);
            Assert.AreEqual(expectA, a);
            Assert.AreEqual(expectB, b);
        }

        [DataTestMethod]
        [DataRow(0, 3, 4, 10)]
        [DataRow(1, 2, 4, 10)]
        [DataRow(4, 10, 0, 3)]
        [DataRow(4, 10, 1, 2)]
        public void LineGeometryNegativeTest(float a1, float a2, float a3, float a4)
        {
            var logger = new NullLogger<LineGeometry>();
            var lineGeometry = new LineGeometry(logger);

            var res = lineGeometry.TryGetCommonLineSegment(a1, a2, a3, a4, out var a, out var b);
            Assert.IsFalse(res);
        }
    }
}
