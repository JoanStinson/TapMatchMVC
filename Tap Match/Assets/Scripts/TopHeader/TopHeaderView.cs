using TMPro;
using UnityEngine;

namespace JGM.Game
{
    public class TopHeaderView : MonoBehaviour
    {
        [SerializeField] 
        private TextMeshProUGUI m_movesAmountText;

        private int m_movesAmount;

        public void Initialize(int initialMovesAmount)
        {
            m_movesAmount = initialMovesAmount;
            m_movesAmountText.text = m_movesAmount.ToString();
        }

        public void IncreaseMovesAmount()
        {
            m_movesAmount++;
            m_movesAmountText.text = m_movesAmount.ToString();
        }
    }
}