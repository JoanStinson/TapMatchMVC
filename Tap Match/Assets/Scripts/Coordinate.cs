using System;

namespace JGM.Game
{
    public class Coordinate
    {
        public int x => m_x;
        public int y => m_y;
        public bool IsVisited { get; set; }

        private int m_x = -1;
        private int m_y = -1;

        public Coordinate(int x, int y)
        {
            m_x = x;
            m_y = y;
        }

        public bool IsAdjacent(Coordinate otherCoordinate)
        {
            int rowDiff = Math.Abs(m_x - otherCoordinate.m_x);
            int colDiff = Math.Abs(m_y - otherCoordinate.m_y);
            return (rowDiff == 1 && colDiff == 0) || (rowDiff == 0 && colDiff == 1);
        }

        public void Reset()
        {
            m_x = -1;
            m_y = -1;
        }
    }
}