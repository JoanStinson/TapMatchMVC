using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JGM.Game
{
    public class GameView : MonoBehaviour, IGameView
    {
        [SerializeField] private GridLayoutGroup m_grid;
        [SerializeField] private CellView m_cellViewPrefab;

        private GameController m_controller;
        private GridModel m_gridModel;
        private List<CellView> m_cells;

        public void Initialize(GameSettings gameSettings)
        {
            m_controller = new GameController();

            for (int i = 0; i < m_grid.transform.childCount; i++)
            {
                Destroy(m_grid.transform.GetChild(i).gameObject);
            }

            m_cells = new List<CellView>();
            m_gridModel = m_controller.BuildGridModel(gameSettings);
            for (int i = 0; i < m_gridModel.rows; i++)
            {
                for (int j = 0; j < m_gridModel.cols; j++)
                {
                    var go = Instantiate(m_cellViewPrefab, m_grid.transform, false);
                    var cellModel = m_gridModel.GetCell(i, j);
                    go.Initialize(cellModel, OnClickCell);
                    m_cells.Add(go);
                }
            }
        }

        private void OnClickCell(CellModel cellModel)
        {
            var connectedCells = new List<Coordinate>();
            var targetCoordinate = cellModel.coordinate;
            var targetColor = cellModel.color;

            FindConnectedCells(targetCoordinate, targetColor, connectedCells);

            if (connectedCells.Count <= 1)
            {
                return;
            }

            foreach (var connectedCoordinate in connectedCells)
            {
                CellView cellView = null;
                foreach (var item in m_cells)
                {
                    if (connectedCoordinate == item.model.coordinate)
                    {
                        //m_cells.Remove(item);
                        cellView = item;
                        //m_gridModel
                        //Destroy(item.gameObject);
                        break;
                    }
                }

                cellView?.Initialize(m_gridModel.EmptyCell(connectedCoordinate), OnClickCell);
                connectedCoordinate.IsVisited = false;
            }
        }

        private void FindConnectedCells(Coordinate coordinate, Color targetColor, List<Coordinate> connectedCells)
        {
            if (coordinate.x < 0 || coordinate.x >= m_gridModel.rows || coordinate.y < 0 || coordinate.y >= m_gridModel.cols)
            {
                return;
            }

            var cell = m_gridModel.GetCell(coordinate.x, coordinate.y);

            if (cell.color != targetColor || cell.coordinate.IsVisited)
            {
                return;
            }

            cell.coordinate.IsVisited = true;
            connectedCells.Add(cell.coordinate);

            var neighbors = new[]
            {
                new Coordinate(coordinate.x + 1, coordinate.y),
                new Coordinate(coordinate.x - 1, coordinate.y),
                new Coordinate(coordinate.x, coordinate.y + 1),
                new Coordinate(coordinate.x, coordinate.y - 1)
            };

            foreach (var neighbor in neighbors)
            {
                if (IsValidCoordinate(neighbor) && IsValidConnection(coordinate, neighbor))
                {
                    FindConnectedCells(neighbor, targetColor, connectedCells);
                }
            }
        }

        private bool IsValidCoordinate(Coordinate coordinate)
        {
            return coordinate.x >= 0 && coordinate.x < m_gridModel.rows && coordinate.y >= 0 && coordinate.y < m_gridModel.cols;
        }

        private bool IsValidConnection(Coordinate from, Coordinate to)
        {
            return from.x == to.x || from.y == to.y;
        }
    }
}