using UnityEngine;

namespace JGM.Game
{
    [CreateAssetMenu(fileName = "Grid Size", menuName = "Settings/Grid Size", order = 0)]
    public class GridSize : ScriptableObject
    {
        [SerializeField, Range(5, 20)] private int m_rows;
        [SerializeField, Range(5, 20)] private int m_cols;
        [SerializeField] private float m_minCellSize = 20f;

        public int rows => m_rows;
        public int cols => m_cols;
        public float minCellSize => m_minCellSize;
    }
}