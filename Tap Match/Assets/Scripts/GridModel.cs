using System;
using System.Collections.Generic;
using UnityEngine;

namespace JGM.Game
{
    public class GridModel
    {
        private CellModel[,] m_grid;

        private const int m_minSize = 5;
        private const int m_maxSize = 20;

        public int rows;
        public int cols;

        public GridModel(int numberOfRows, int numberOfCols)
        {
            Debug.Assert(numberOfRows >= m_minSize && numberOfRows <= m_maxSize);
            Debug.Assert(numberOfCols >= m_minSize && numberOfCols <= m_maxSize);
            m_grid = new CellModel[numberOfRows, numberOfCols];
            rows = numberOfRows;
            cols = numberOfCols;
        }

        public CellModel SetCell(Coordinate coordinate, Color color)
        {
            //Debug.Assert(coordinate.x >= 0 && coordinate.x <= m_grid.GetLength(0));
            //Debug.Assert(coordinate.y >= 0 && coordinate.y <= m_grid.GetLength(1));
            m_grid[coordinate.x, coordinate.y] = new CellModel(coordinate, color);
            return m_grid[coordinate.x, coordinate.y];
        }

        public CellModel EmptyCell(Coordinate coordinate)
        {
            Debug.Assert(coordinate.x >= 0 && coordinate.x <= m_grid.GetLength(0));
            Debug.Assert(coordinate.y >= 0 && coordinate.y <= m_grid.GetLength(1));
            m_grid[coordinate.x, coordinate.y].EmptyCell();
            return m_grid[coordinate.x, coordinate.y];
        }

        internal CellModel GetCell(int i, int j)
        {
            return m_grid[i, j];
        }

        public void CascadeAndShiftCells()
        {
            List<int> columnsToShift = new List<int>(); // Keep track of columns to shift

            // Loop through each column
            for (int col = 0; col < cols; col++)
            {
                List<CellModel> nonEmptyCells = new List<CellModel>();

                // Loop through each row in reverse order to find non-empty cells
                for (int row = rows - 1; row >= 0; row--)
                {
                    var cell = GetCell(row, col);
                    if (!cell.IsEmpty())
                    {
                        nonEmptyCells.Add(cell);
                    }
                }

                // If there are non-empty cells, move them to the bottom of the column
                if (nonEmptyCells.Count > 0)
                {
                    int rowIndex = rows - 1;

                    // Loop through each non-empty cell and move it to the bottom
                    foreach (var cell in nonEmptyCells)
                    {
                        SetCell(new Coordinate(rowIndex, col), cell.color);
                        rowIndex--;
                    }

                    // Empty remaining cells at the top
                    while (rowIndex >= 0)
                    {
                        EmptyCell(new Coordinate(rowIndex, col));
                        rowIndex--;
                    }

                    if (rowIndex < rows - 1) // Check if any shifting was performed
                    {
                        columnsToShift.Add(col); // Store the column index for shifting
                    }
                }
            }

            // Call ShiftCells for each column that needs shifting
            foreach (int colToShift in columnsToShift)
            {
                ShiftCells(colToShift);
            }
        }

        public void ShiftCells(int col)
        {
            List<CellModel> nonEmptyCells = new List<CellModel>();

            // Loop through each row in reverse order to find non-empty cells
            for (int row = rows - 1; row >= 0; row--)
            {
                var cell = GetCell(row, col);
                if (!cell.IsEmpty())
                {
                    nonEmptyCells.Add(cell);
                }
            }

            // Move non-empty cells to the bottom of the column
            if (nonEmptyCells.Count > 0)
            {
                int rowIndex = rows - 1;

                // Loop through each non-empty cell and move it to the bottom
                foreach (var cell in nonEmptyCells)
                {
                    SetCell(new Coordinate(rowIndex, col), cell.color);
                    rowIndex--;
                }

                // Empty remaining cells at the top
                while (rowIndex >= 0)
                {
                    EmptyCell(new Coordinate(rowIndex, col));
                    rowIndex--;
                }
            }
        }




    }
}