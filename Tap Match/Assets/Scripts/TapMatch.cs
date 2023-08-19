using UnityEngine;

namespace JGM.Game
{
    public class TapMatch : MonoBehaviour
    {
        [SerializeField] private GameSettings m_settings;
        [SerializeField] private GameView m_gameView;

        private void Start()
        {
            m_gameView.Initialize(new GameController(), m_settings);
        }
    }
}