using UnityEngine;

namespace JGM.Game
{
    public class CellModel
    {
        private Coordinate m_coordinate;
        private Color m_color = Color.gray;

        public Color color => m_color;

        public CellModel(Coordinate coordinate, Color color)
        {
            SetCell(coordinate, color);
        }

        public void SetCell(Coordinate coordinate, Color color)
        {
            m_coordinate = coordinate;
            m_color = color;
        }

        public bool IsConnected(CellModel otherCell)
        {
            if (m_color != otherCell.m_color)
            {
                return false;
            }

            return m_coordinate.IsAdjacent(otherCell.m_coordinate);
        }

        public void EmptyCell()
        {
            m_coordinate.Reset();
            m_color = Color.gray;
        }
    }
}