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
        private Color m_color = Color.gray;

        public CellModel(Coordinate coordinate, Color color)
        {
            SetCell(coordinate, color);
        }

        public void SetCell(Coordinate coordinate, Color color)
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

        public void EmptyCell()
        {
            m_coordinate.Reset();
            m_color = Color.gray;
        }
    }

    public class Coordinate
    {
        public int x => m_x;
        public int y => m_y;

        private int m_x = -1;
        private int m_y = -1;

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

        public void Reset()
        {
            m_x = -1;
            m_y = -1;
        }
    }

    public class GridModel
    {
        private CellModel[,] m_grid;

        private const int m_minSize = 5;
        private const int m_maxSize = 20;

        public GridModel(int numberOfRows, int numberOfCols)
        {
            Debug.Assert(numberOfRows >= m_minSize && numberOfRows <= m_maxSize);
            Debug.Assert(numberOfCols >= m_minSize && numberOfCols <= m_maxSize);
            m_grid = new CellModel[numberOfRows, numberOfCols];
        }

        public CellModel SetCell(Coordinate coordinate, Color color)
        {
            //Debug.Assert(coordinate.x >= 0 && coordinate.x <= m_grid.GetLength(0));
            //Debug.Assert(coordinate.y >= 0 && coordinate.y <= m_grid.GetLength(1));
            m_grid[coordinate.x, coordinate.y].SetCell(coordinate, color);
            return m_grid[coordinate.x, coordinate.y];
        }

        public void EmptyCell(Coordinate coordinate)
        {
            Debug.Assert(coordinate.x >= 0 && coordinate.x <= m_grid.GetLength(0));
            Debug.Assert(coordinate.y >= 0 && coordinate.y <= m_grid.GetLength(1));
            m_grid[coordinate.x, coordinate.y].EmptyCell();
        }
    }
}