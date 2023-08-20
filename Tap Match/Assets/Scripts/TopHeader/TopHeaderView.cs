using TMPro;
using UnityEngine;

namespace JGM.Game
{
    public class TopHeaderView : MonoBehaviour
    {
        [SerializeField] 
        private TextMeshProUGUI m_matchesAmountText;

        private int m_matchesAmount;

        public void Initialize(int initialMatchesAmount)
        {
            m_matchesAmount = initialMatchesAmount;
            m_matchesAmountText.text = m_matchesAmount.ToString();
        }

        public void IncreaseMatchesAmount()
        {
            m_matchesAmount++;
            m_matchesAmountText.text = m_matchesAmount.ToString();
        }
    }
}