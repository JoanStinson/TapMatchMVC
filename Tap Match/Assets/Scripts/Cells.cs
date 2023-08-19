using UnityEngine;

namespace JGM.Game
{
    [CreateAssetMenu(fileName = "Cells", menuName = "Settings/Cells", order = 0)]
    public class Cells : ScriptableObject
    {
        [SerializeField] private Color[] m_colors = new Color[3];

        public Color[] colors
        {
            get { return m_colors; }
            set { m_colors = value; }
        }
    }
}