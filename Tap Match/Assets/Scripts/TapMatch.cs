using UnityEngine;

namespace JGM.Game
{
    public class TapMatch : MonoBehaviour
    {
        [SerializeField] private IGameView m_gameView;
        private IGameController m_gameController;

        private void Start()
        {
            m_gameView.Initialize();
            m_gameController ??= new GameController();
            m_gameController.Initialize();
        }

        public void SetDependencies(IGameView gameView, IGameController gameController)
        {
            m_gameView = gameView;
            m_gameController = gameController;
        }
    }
}