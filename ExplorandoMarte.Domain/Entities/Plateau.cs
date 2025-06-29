using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExplorandoMarte.Domain.Entities
{
    public class Plateau
    {
        public int MaxX { get; }
        public int MaxY { get; }

        private readonly HashSet<string> occupiedPositions = new();

        public Plateau(int maxX, int maxY)
        {
            MaxX = maxX;
            MaxY = maxY;
        }

        public bool IsInside(Position position)
        {
            return position.X >= 0 && position.Y >= 0 &&
                   position.X <= MaxX && position.Y <= MaxY;
        }

        public bool IsOccupied(Position position)
        {
            return occupiedPositions.Contains(GetKey(position));
        }

        public void MarkOccupied(Position position)
        {
            occupiedPositions.Add(GetKey(position));
        }

        public void Free(Position position)
        {
            occupiedPositions.Remove(GetKey(position));
        }

        private string GetKey(Position position)
        {
            return $"{position.X}-{position.Y}";
        }
    }
}
