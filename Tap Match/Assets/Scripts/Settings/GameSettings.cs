using UnityEngine;

namespace JGM.Game
{
    [CreateAssetMenu(fileName = "Game Settings", menuName = "Game Settings", order = 0)]
    public class GameSettings : ScriptableObject
    {
        public int rows => m_rows;
        public int columns => m_columns;
        public float minCellSize => m_minCellSize;
        public Sprite[] cellSprites
        {
            get { return m_cellSprites; }
            set { m_cellSprites = value; }
        }

        [SerializeField, Range(5, 20)] private int m_rows;
        [SerializeField, Range(5, 20)] private int m_columns;
        [SerializeField] private float m_minCellSize = 20f;
        [SerializeField] private Sprite[] m_cellSprites = new Sprite[3];
    }
}