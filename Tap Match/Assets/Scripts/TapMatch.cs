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

    public class CellModel
    {
        private Coordinate m_coordinate;
        private Color m_color;

        public CellModel(Coordinate coordinate, Color color)
        {
            m_coordinate = coordinate;
            m_color = color;
        }

        public bool IsConnected(CellModel otherCell)
        {
            if (m_color != otherCell.m_color)
            {
                return false;
            }

            return m_coordinate.IsAdjacent(otherCell.m_coordinate);
        }
    }

    public class Coordinate
    {
        private int m_x;
        private int m_y;

        public Coordinate(int x, int y)
        {
            m_x = x;
            m_y = y;
        }

        public bool IsAdjacent(Coordinate otherCoordinate)
        {
            int rowDiff = Math.Abs(m_x - otherCoordinate.m_x);
            int colDiff = Math.Abs(m_y - otherCoordinate.m_y);
            return (rowDiff == 1 && colDiff == 0) || (rowDiff == 0 && colDiff == 1);
        }
    }
}