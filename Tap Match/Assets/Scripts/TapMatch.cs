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

    public class Coordinate
    {
        public int X => m_x;
        public int Y => m_y;
        public Color Color => m_color;
        
        private int m_x;
        private int m_y;
        private Color m_color;

        public Coordinate(int x, int y, Color color)
        {
            m_x = x;
            m_y = y;
            m_color = color;
        }

        public bool IsConnected(Coordinate newCoordinate)
        {
            if (m_color != newCoordinate.m_color)
            {
                return false;
            }

            int rowDiff = Math.Abs(m_x - newCoordinate.m_x);
            int colDiff = Math.Abs(m_y - newCoordinate.m_y);

            bool connectedVertically = (rowDiff == 1 && colDiff == 0);
            bool connectedHorizontally = (rowDiff == 0 && colDiff == 1);

            return connectedVertically || connectedHorizontally;
        }
    }
}