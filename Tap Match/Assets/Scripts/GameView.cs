using UnityEngine;

namespace JGM.Game
{
    public class GameView : MonoBehaviour
    {
        [SerializeField] private TopHeaderView m_topHeaderView;
        [SerializeField] private GridView m_gridView;

        public void Initialize(GameSettings settings)
        {
            m_topHeaderView.Initialize(0);
            m_gridView.onCellsMatch += m_topHeaderView.IncreaseMatchesAmount;
            m_gridView.Initialize(new GridController(), settings);
        }
    }
}