using UnityEngine;

namespace JGM.Game
{
    public class CellModel
    {
        public Coordinate coordinate => m_coordinate;
        public Color color => m_color;

        private readonly Coordinate m_coordinate;
        private Color m_color = Color.gray;

        public CellModel(Coordinate coordinate, Color color)
        {
            m_coordinate = coordinate;
            m_color = color;
        }

        public void EmptyCell()
        {
            m_color = Color.gray;
        }

        public bool IsEmpty()
        {
            return m_color == Color.gray;
        }
    }
}