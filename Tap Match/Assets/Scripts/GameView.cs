using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace JGM.Game
{
    public class GameView : MonoBehaviour, IGameView
    {
        [SerializeField] private CanvasGroup m_canvasGroup;
        [SerializeField] private GridLayoutGroup m_grid;
        [SerializeField] private CellView m_cellViewPrefab;

        private GameController m_controller;
        private GridModel m_gridModel;
        private List<CellView> m_cells;

        public void Initialize(GameSettings gameSettings)
        {
            m_controller = new GameController();

            //for (int i = 0; i < m_grid.transform.childCount; i++)
            //{
            //    Destroy(m_grid.transform.GetChild(i).gameObject);
            //}

            m_cells = new List<CellView>();
            m_gridModel = m_controller.BuildGridModel(gameSettings);
            for (int i = 0; i < m_gridModel.rows; i++)
            {
                for (int j = 0; j < m_gridModel.cols; j++)
                {
                    var go = Instantiate(m_cellViewPrefab);
                    var transformParent = m_grid.transform.GetChild(i * m_gridModel.cols + j);
                    go.transform.SetParent(transformParent, false);
                    var cellModel = m_gridModel.GetCell(i, j);
                    go.Initialize(cellModel, OnClickCell);
                    m_cells.Add(go);
                }
            }
        }

        private async void OnClickCell(CellModel cellModel)
        {
            m_canvasGroup.blocksRaycasts = false;

            var connectedCells = new List<Coordinate>();
            var targetCoordinate = cellModel.coordinate;
            var targetColor = cellModel.color;

            FindConnectedCells(targetCoordinate, targetColor, connectedCells);

            if (connectedCells.Count <= 1)
            {
                m_canvasGroup.blocksRaycasts = true;
                return;
            }

            foreach (var connectedCoordinate in connectedCells)
            {
                CellView cellView = m_cells.Find(view => view.model.coordinate == connectedCoordinate);
                if (cellView != null)
                {
                    m_gridModel.EmptyCell(connectedCoordinate);
                    m_cells.Remove(cellView);
                    Destroy(cellView.gameObject);
                }

                connectedCoordinate.IsVisited = false;
            }

            await Task.Delay(TimeSpan.FromSeconds(1));
            m_gridModel.CascadeAndShiftCells();
            m_controller.FillEmptySlotsAfterCascade(m_gridModel);

            // Destroy existing cell views and clear the list
            foreach (var cellView in m_cells)
            {
                Destroy(cellView.gameObject);
            }
            m_cells.Clear();

            // Loop through the grid rows and columns to recreate cell views
            for (int i = 0; i < m_gridModel.rows; i++)
            {
                for (int j = 0; j < m_gridModel.cols; j++)
                {
                    var cellModel2 = m_gridModel.GetCell(i, j);
                    if (!cellModel2.IsEmpty())
                    {
                        var cellView2 = Instantiate(m_cellViewPrefab);
                        m_cells.Add(cellView2);
                        cellView2.Initialize(cellModel2, OnClickCell);
                        var transformParent = m_grid.transform.GetChild(i * m_gridModel.cols + j);
                        cellView2.transform.SetParent(transformParent, false);

                        // Set sibling index to the last position in the column
                        cellView2.transform.SetSiblingIndex(transformParent.childCount - 1);
                    }
                }
            }

            m_canvasGroup.blocksRaycasts = true;
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