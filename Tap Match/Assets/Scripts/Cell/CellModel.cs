using UnityEngine;

namespace JGM.Game
{
    public class CellModel
    {
        public Coordinate coordinate => m_coordinate;
        public Sprite sprite => m_sprite;
        public int type => m_type;

        private readonly Coordinate m_coordinate;
        private Sprite m_sprite;
        private readonly int m_type;

        public CellModel(Coordinate coordinate, Sprite sprite, int type)
        {
            m_coordinate = coordinate;
            m_sprite = sprite;
            m_type = type;
        }

        public void EmptyCell()
        {
            m_sprite = null;
        }

        public bool IsEmpty()
        {
            return m_sprite == null;
        }
    }
}