using UnityEngine;

namespace JGM.Game
{
    [CreateAssetMenu(fileName = "Game Settings", menuName = "Settings/Game Settings", order = 0)]
    public class GameSettings : ScriptableObject
    {
        [SerializeField] private GridSize m_gridSize;
        [SerializeField] private Cells m_cells;

        public GridSize gridSize => m_gridSize;
        public Cells cells => m_cells;
    }
}