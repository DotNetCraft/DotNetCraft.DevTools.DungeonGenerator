using System;
using System.Collections.Generic;
using System.Text;
using DotNetCraft.DevTools.DungeonGenerator.Core.Geometry;

namespace DotNetCraft.DevTools.DungeonGenerator.Core.Rooms
{
    public class RoomsMap
    {
        private readonly string[][] _cells;

        public RoomsMap(Rect globalRect)
        {
            _cells = new string[globalRect.Width][];
            for (int x = 0; x < globalRect.Width; x++)
            {
                var verticalLine = new string[globalRect.Height];
                _cells[x] = verticalLine;
                for (int y = 0; y < verticalLine.Length; y++)
                {
                    verticalLine[y] = null;
                }
            }
        }

        public void AddRoom(int roomNumber, Rect geometry)
        {
            for (int i = 0; i < geometry.Width; i++)
            {
                var x = (int)geometry.X + i;
                var verticalLine = _cells[x];

                for (int j = 0; j < geometry.Height; j++)
                {
                    var y = (int)geometry.Y + j;
                    var cellValue = verticalLine[y];
                    if (string.IsNullOrWhiteSpace(cellValue))
                    {
                        verticalLine[y] = $"Room_{roomNumber}";
                        continue;
                    }

                    var existingRoom = int.Parse(cellValue.Split('_')[1]);
                    verticalLine[y] = $"Connection_{existingRoom}_{roomNumber}";
                }
            }
        }

        #region Overrides of Object

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            
            for (int x = 0; x < _cells.Length; x++)
            {
                var verticalLine = _cells[x];
                for (int y = 0; y < verticalLine.Length; y++)
                {
                    stringBuilder.Append(verticalLine[y]);
                }

                stringBuilder.AppendLine();
            }

            return stringBuilder.ToString();
        }

        #endregion
    }
}
