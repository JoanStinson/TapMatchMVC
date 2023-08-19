using UnityEngine;

namespace JGM.Game
{
    public class CellModel
    {
        public Coordinate coordinate => m_coordinate;
        public Sprite sprite => m_sprite;
        public AnimatorOverrideController animatorController => m_animatorController;
        public int type => m_type;
        public bool needsToAnimate
        {
            get => m_needsToAnimate;
            set => m_needsToAnimate = value;
        }

        private Coordinate m_coordinate;
        private Sprite m_sprite;
        private AnimatorOverrideController m_animatorController;
        private int m_type;
        private bool m_needsToAnimate;

        public CellModel(Coordinate coordinate, CellAsset cellAsset, int type)
        {
            SetValues(coordinate, cellAsset, type, true);
        }

        public void SetValues(Coordinate coordinate, CellAsset cellAsset, int type, bool needsToAnimate)
        {
            m_coordinate = coordinate;
            m_sprite = cellAsset.sprite;
            m_animatorController = cellAsset.animatorController;
            m_type = type;
            m_needsToAnimate = needsToAnimate;
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