using UnityEngine;
using UnityEngine.UI;

namespace JGM.Game
{
    public class GameView : MonoBehaviour
    {
        [SerializeField] private TopHeaderView m_topHeaderView;
        [SerializeField] private GridView m_gridView;
        [SerializeField] private int m_initialMovesAmount = 0;
        [SerializeField] private Image m_fadePanelImage;
        [SerializeField] private float m_fadeDuration = 1.0f;

        public void Initialize()
        {
            m_topHeaderView.Initialize(m_initialMovesAmount);
            m_gridView.onCellsMatch += m_topHeaderView.IncreaseMovesAmount;
            m_gridView.Initialize(new GridController());
            m_fadePanelImage.enabled = true;
            m_fadePanelImage.CrossFadeAlpha(0f, m_fadeDuration, false);
        }
    }
}