using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace JGM.Game
{
    public class GridController
    {
        private GridModel m_grid;
        private CellAsset[] m_cellAssets;

        public GridModel BuildGrid(GameSettings settings)
        {
            m_grid = new GridModel(settings.rows, settings.columns);
            m_cellAssets = settings.cellAssets;

            for (int i = 0; i < settings.rows; i++)
            {
                for (int j = 0; j < settings.columns; j++)
                {
                    int randomIndex = Random.Range(0, m_cellAssets.Length);
                    CellAsset cellAsset = m_cellAssets[randomIndex];
                    m_grid.InitCell(new Coordinate(i, j), cellAsset, randomIndex);
                }
            }

            return m_grid;
        }

        public bool EmptyConnectedCells(CellModel cell, out List<CellModel> connectedCells)
        {
            connectedCells = new List<CellModel>();
            FindConnectedCells(cell.coordinate, cell.type, connectedCells);

            if (connectedCells.Count > 1)
            {
                foreach (var connectedCell in connectedCells)
                {
                    connectedCell.EmptyCell();
                    connectedCell.coordinate.visited = false;
                }
                return true;
            }

            if (connectedCells.Count > 0)
            {
                connectedCells[0].coordinate.visited = false;
            }
            return false;
        }

        private void FindConnectedCells(Coordinate coordinate, int targetType, List<CellModel> connectedCells)
        {
            if (coordinate.x < 0 || coordinate.x >= m_grid.rows || coordinate.y < 0 || coordinate.y >= m_grid.columns)
            {
                return;
            }

            var cell = m_grid.GetCell(coordinate);

            if (cell.type != targetType || cell.coordinate.visited)
            {
                return;
            }

            cell.coordinate.visited = true;
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
                    FindConnectedCells(neighbor, targetType, connectedCells);
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
                int emptyRowIndex = -1;

                // Loop through each row in reverse order to get filled cells and find the empty row index
                for (int row = m_grid.rows - 1; row >= 0; row--)
                {
                    var cell = m_grid.GetCell(new Coordinate(row, column));
                    if (!cell.IsEmpty())
                    {
                        filledCells.Add(cell);
                    }
                    else if (emptyRowIndex == -1)
                    {
                        emptyRowIndex = row;
                    }
                }

                // If there are filled cells, shift them to the bottom of the column
                if (filledCells.Count > 0 && filledCells.Count < m_grid.rows)
                {
                    int rowIndex = m_grid.rows - 1;

                    foreach (var filledCell in filledCells)
                    {
                        var cellAsset = new CellAsset(filledCell.sprite, filledCell.overrideController);
                        bool shouldAnimate = filledCell.coordinate.x <= emptyRowIndex;
                        m_grid.SetCell(new Coordinate(rowIndex, column), cellAsset, filledCell.type, shouldAnimate);
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
                int randomIndex = Random.Range(0, m_cellAssets.Length);
                CellAsset cellAsset = m_cellAssets[randomIndex];

                if (m_grid.GetCell(coordinate).IsEmpty())
                {
                    m_grid.SetCell(coordinate, cellAsset, randomIndex, true);
                }
            }
        }

        public void EmptyCellsFromType(int cellType, out List<CellModel> cellsFromType)
        {
            cellsFromType = new List<CellModel>();

            for (int i = 0; i < m_grid.rows; i++)
            {
                for (int j = 0; j < m_grid.columns; j++)
                {
                    var cell = m_grid.GetCell(new Coordinate(i, j));
                    if (!cell.IsEmpty() && cell.type == cellType)
                    {
                        cellsFromType.Add(cell);
                    }
                }
            }

            foreach (var cell in cellsFromType)
            {
                cell.EmptyCell();
            }
        }
    }
}