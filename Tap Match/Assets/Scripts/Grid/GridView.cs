using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace JGM.Game
{
    public class GridView : MonoBehaviour
    {
        public Action onCellsMatch;

        [SerializeField] private CanvasGroup m_canvasGroup;
        [SerializeField] private GridLayoutGroup m_gridLayoutGroup;
        [SerializeField] private ContentSizeFitter m_contentSizeFitter;
        [SerializeField] private float m_delayToShiftCellsInSeconds = 0.5f;

        [Inject] private IAudioService m_audioService;
        [Inject] private GameSettings m_settings;
        [Inject] private CellView.Factory m_cellViewFactory;
        [Inject] private CellView.SlotFactory m_cellSlotFactory;

        private GridModel m_grid;
        private GridController m_controller;
        private Dictionary<Coordinate, CellView> m_cellViewInstances;

        public void Initialize(GridController controller)
        {
            m_controller = controller;
            m_grid = m_controller.BuildGrid(m_settings);
            m_cellViewInstances = new Dictionary<Coordinate, CellView>();
            InstantiateCellsInsideGrid();
            new DynamicGridLayout().SetupGridLayout(m_settings, m_gridLayoutGroup, m_contentSizeFitter);
        }

        private void InstantiateCellsInsideGrid()
        {
            for (int i = 0; i < m_grid.rows; i++)
            {
                for (int j = 0; j < m_grid.columns; j++)
                {
                    var cellSlotInstance = m_cellSlotFactory.Create();
                    cellSlotInstance.SetParent(m_gridLayoutGroup.transform, false);
                    
                    var cellViewInstance = m_cellViewFactory.Create();
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
                m_audioService.Play(AudioFileNames.CannotPopSfx);
                return;
            }

            onCellsMatch?.Invoke();
            m_audioService.Play(AudioFileNames.PopSfx);
            RefreshConnectedCellsInGrid(connectedCells);
            await Task.Delay(TimeSpan.FromSeconds(m_delayToShiftCellsInSeconds));
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
                    cellView.PlayPopAnimation();
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