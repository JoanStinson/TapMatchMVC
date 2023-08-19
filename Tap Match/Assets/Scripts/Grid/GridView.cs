using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace JGM.Game
{
    public class GridView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup m_canvasGroup;
        [SerializeField] private GridLayoutGroup m_gridLayoutGroup;
        [SerializeField] private ContentSizeFitter m_contentSizeFitter;
        [SerializeField] private CellView m_cellViewPrefab;
        [SerializeField] private Transform m_cellSlotPrefab;

        private GridModel m_grid;
        private GridController m_controller;
        private List<CellView> m_cellViewInstances;

        public void Initialize(GridController controller, GameSettings settings)
        {
            m_controller = controller;
            m_grid = m_controller.BuildGrid(settings);
            m_cellViewInstances = new List<CellView>();
            InstantiateCellsInsideGrid();
            new DynamicGridLayout().SetupGridLayout(settings, m_gridLayoutGroup, m_contentSizeFitter);
        }

        private void InstantiateCellsInsideGrid()
        {
            for (int i = 0; i < m_grid.rows; i++)
            {
                for (int j = 0; j < m_grid.columns; j++)
                {
                    Instantiate(m_cellSlotPrefab, m_gridLayoutGroup.transform, false);
                    var cellViewInstance = Instantiate(m_cellViewPrefab);
                    var transformParent = m_gridLayoutGroup.transform.GetChild(i * m_grid.columns + j);
                    cellViewInstance.transform.SetParent(transformParent, false);
                    var cellModel = m_grid.GetCell(new Coordinate(i, j));
                    cellViewInstance.Initialize(cellModel, OnClickCell);
                    m_cellViewInstances.Add(cellViewInstance);
                }
            }
        }

        private async void OnClickCell(CellModel cell)
        {
            m_canvasGroup.blocksRaycasts = false;

            var connectedCells = new List<Coordinate>();
            FindConnectedCells(cell.coordinate, cell.color, connectedCells);

            if (connectedCells.Count <= 1)
            {
                m_canvasGroup.blocksRaycasts = true;
                return;
            }

            EmptyConnectedCells(connectedCells);
            await Task.Delay(TimeSpan.FromSeconds(1));
            m_controller.ShiftCellsDownwardsAndFillEmptySlots();
            RefreshCellsInGrid();

            m_canvasGroup.blocksRaycasts = true;
        }

        private void EmptyConnectedCells(List<Coordinate> connectedCells)
        {
            foreach (var connectedCoordinate in connectedCells)
            {
                var cellView = m_cellViewInstances.Find(view => view.model.coordinate == connectedCoordinate);
                if (cellView != null)
                {
                    m_grid.EmptyCell(connectedCoordinate);
                    m_cellViewInstances.Remove(cellView);
                    Destroy(cellView.gameObject);
                }

                connectedCoordinate.isVisited = false;
            }
        }

        private void RefreshCellsInGrid()
        {
            // Destroy existing cell views and clear the list
            foreach (var cellView in m_cellViewInstances)
            {
                Destroy(cellView.gameObject);
            }
            m_cellViewInstances.Clear();

            // Loop through the grid rows and columns to recreate cell views
            for (int i = 0; i < m_grid.rows; i++)
            {
                for (int j = 0; j < m_grid.columns; j++)
                {
                    var cellModel2 = m_grid.GetCell(new Coordinate(i, j));
                    if (!cellModel2.IsEmpty())
                    {
                        var cellView2 = Instantiate(m_cellViewPrefab);
                        m_cellViewInstances.Add(cellView2);
                        cellView2.Initialize(cellModel2, OnClickCell);
                        var transformParent = m_gridLayoutGroup.transform.GetChild(i * m_grid.columns + j);
                        cellView2.transform.SetParent(transformParent, false);
                        cellView2.transform.SetSiblingIndex(transformParent.childCount - 1);
                    }
                }
            }
        }

        private void FindConnectedCells(Coordinate coordinate, Color targetColor, List<Coordinate> connectedCells)
        {
            if (coordinate.x < 0 || coordinate.x >= m_grid.rows ||
                coordinate.y < 0 || coordinate.y >= m_grid.columns)
            {
                return;
            }

            var cell = m_grid.GetCell(coordinate);

            if (cell.color != targetColor || cell.coordinate.isVisited)
            {
                return;
            }

            cell.coordinate.isVisited = true;
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
            return coordinate.x >= 0 && coordinate.x < m_grid.rows && coordinate.y >= 0 && coordinate.y < m_grid.columns;
        }

        private bool IsValidConnection(Coordinate from, Coordinate to)
        {
            return from.x == to.x || from.y == to.y;
        }
    }
}