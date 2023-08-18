using System;
using UnityEngine;

namespace JGM.Game
{
    public interface IGameDependency
    {
        void Initialize();
    }

    public interface IGameView : IGameDependency
    {

    }

    public interface IGameController : IGameDependency
    {

    }

    public class GameView : MonoBehaviour, IGameView
    {
        public void Initialize()
        {
            
        }
    }

    public class GameController : IGameController
    {
        public void Initialize()
        {
            
        }
    }

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