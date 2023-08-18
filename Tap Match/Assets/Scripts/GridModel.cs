using System;
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
    }
}