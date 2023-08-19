using UnityEngine;

namespace JGM.Game
{
    [CreateAssetMenu(fileName = "Game Settings", menuName = "Game Settings", order = 0)]
    public partial class GameSettings : ScriptableObject
    {
        public int rows => m_rows;
        public int columns => m_columns;
        public float minCellSize => m_minCellSize;
        public CellAsset[] cellAssets
        {
            get => m_cellAssets;
            set => m_cellAssets = value;
        }

        [SerializeField, Range(5, 20)] private int m_rows;
        [SerializeField, Range(5, 20)] private int m_columns;
        [SerializeField] private float m_minCellSize = 20f;
        [SerializeField] private CellAsset[] m_cellAssets = new CellAsset[3];
    }
}