using System.Collections.Generic;
using UnityEngine;

namespace JGM.Game
{
    public class GridController
    {
        private GridModel m_grid;
        private Color[] m_cellColors;

        public GridModel BuildGrid(GameSettings settings)
        {
            m_grid = new GridModel(settings.rows, settings.columns);
            m_cellColors = settings.cellColors;

            for (int i = 0; i < settings.rows; i++)
            {
                for (int j = 0; j < settings.columns; j++)
                {
                    int randomIndex = Random.Range(0, m_cellColors.Length);
                    Color color = m_cellColors[randomIndex];
                    m_grid.SetCell(new Coordinate(i, j), color);
                }
            }

            return m_grid;
        }

        public void ShiftCellsDownwardsAndFillEmptySlots()
        {
            for (int column = 0; column < m_grid.columns; column++)
            {
                var filledCells = new List<CellModel>();

                // Loop through each row in reverse order to get filled cells
                for (int row = m_grid.rows - 1; row >= 0; row--)
                {
                    var cell = m_grid.GetCell(new Coordinate(row, column));
                    if (!cell.IsEmpty())
                    {
                        filledCells.Add(cell);
                    }
                }

                // If there are filled cells, shift them to the bottom of the column
                if (filledCells.Count > 0)
                {
                    int rowIndex = m_grid.rows - 1;

                    foreach (var cell in filledCells)
                    {
                        m_grid.SetCell(new Coordinate(rowIndex, column), cell.color);
                        rowIndex--;
                    }

                    // Empty remaining cells at the top
                    while (rowIndex >= 0)
                    {
                        m_grid.EmptyCell(new Coordinate(rowIndex, column));
                        rowIndex--;
                    }
                }

                // Fill empty slots for the current column
                FillEmptySlotsFromColumn(column);
            }
        }

        private void FillEmptySlotsFromColumn(int column)
        {
            for (int row = 0; row < m_grid.rows; row++)
            {
                var coordinate = new Coordinate(row, column);
                int randomIndex = Random.Range(0, m_cellColors.Length);
                Color color = m_cellColors[randomIndex];

                if (m_grid.GetCell(coordinate).IsEmpty())
                {
                    m_grid.SetCell(coordinate, color);
                }
            }
        }
    }
}