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

        public bool EmptyConnectedCells(CellModel cell, out List<CellModel> connectedCells)
        {
            connectedCells = new List<CellModel>();
            FindConnectedCells(cell.coordinate, cell.color, connectedCells);

            if (connectedCells.Count > 1)
            {
                foreach (var connectedCell in connectedCells)
                {
                    connectedCell.EmptyCell();
                    connectedCell.coordinate.isVisited = false;
                }
                return true;
            }

            if (connectedCells.Count > 0)
            {
                connectedCells[0].coordinate.isVisited = false;
            }
            return false;
        }

        private void FindConnectedCells(Coordinate coordinate, Color targetColor, List<CellModel> connectedCells)
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
            connectedCells.Add(cell);

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