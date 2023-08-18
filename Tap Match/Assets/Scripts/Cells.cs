using UnityEngine;

namespace JGM.Game
{
    [CreateAssetMenu(fileName = "Cells", menuName = "Settings/Cells", order = 0)]
    public class Cells : ScriptableObject
    {
        [SerializeField] private Color[] m_colors;

        public Color[] colors => m_colors;
    }
}