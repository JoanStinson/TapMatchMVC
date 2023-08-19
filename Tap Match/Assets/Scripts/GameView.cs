using UnityEngine;

namespace JGM.Game
{
    public class GameView : MonoBehaviour
    {
        [SerializeField] private GridView m_gridView;

        public void Initialize(GameController controller, GameSettings settings)
        {
            m_gridView.Initialize(new GridController(), settings);
        }
    }
}