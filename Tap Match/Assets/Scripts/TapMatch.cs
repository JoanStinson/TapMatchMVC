using UnityEngine;

namespace JGM.Game
{
    [RequireComponent(typeof(IGameView))]
    public class TapMatch : MonoBehaviour
    {
        [SerializeField] private GameSettings m_gameSettings;
        
        private GameView m_gameView;

        private void Start()
        {
            if (m_gameView == null)
            {
                m_gameView = gameObject.GetComponent<GameView>();
            }
            m_gameView.Initialize(m_gameSettings);
        }

        public void SetDependencies(IGameView gameView)
        {
            //m_gameView = gameView;
        }
    }
}