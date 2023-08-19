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
        private Dictionary<Coordinate, CellView> m_cellViewInstances;

        public void Initialize(GridController controller, GameSettings settings)
        {
            m_controller = controller;
            m_grid = m_controller.BuildGrid(settings);
            m_cellViewInstances = new Dictionary<Coordinate, CellView>();
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
                    m_cellViewInstances.Add(new Coordinate(i, j), cellViewInstance);
                }
            }
        }

        private async void OnClickCell(CellModel cell)
        {
            m_canvasGroup.blocksRaycasts = false;

            if (!m_controller.EmptyConnectedCells(cell, out var connectedCells))
            {
                m_canvasGroup.blocksRaycasts = true;
                return;
            }

            RefreshConnectedCellsInGrid(connectedCells);
            await Task.Delay(TimeSpan.FromSeconds(0.5f));
            m_controller.ShiftCellsDownwardsAndFillEmptySlots();
            RefreshCellsInGrid();
            m_canvasGroup.blocksRaycasts = true;
        }

        private void RefreshConnectedCellsInGrid(List<CellModel> connectedCells)
        {
            foreach (var connectedCell in connectedCells)
            {
                if (m_cellViewInstances.TryGetValue(connectedCell.coordinate, out CellView cellView))
                {
                    //cellView.Initialize(connectedCell, OnClickCell);
                    cellView.PopAnimation();
                }
            }
        }

        private void RefreshCellsInGrid()
        {
            for (int i = 0; i < m_grid.rows; i++)
            {
                for (int j = 0; j < m_grid.columns; j++)
                {
                    var coordinate = new Coordinate(i, j);
                    var cellModel = m_grid.GetCell(coordinate);
                    if (m_cellViewInstances.TryGetValue(coordinate, out CellView cellView))
                    {
                        cellView.Initialize(cellModel, OnClickCell);
                    }
                }
            }
        }
    }
}