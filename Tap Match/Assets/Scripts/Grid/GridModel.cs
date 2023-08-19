using UnityEngine;

namespace JGM.Game
{
    public class GridModel
    {
        public int rows => m_rows;
        public int columns => m_columns;

        private readonly CellModel[,] m_grid;
        private readonly int m_rows;
        private readonly int m_columns;

        public GridModel(int rows, int columns)
        {
            m_grid = new CellModel[rows, columns];
            m_rows = rows;
            m_columns = columns;
        }

        public void SetCell(Coordinate coordinate, Sprite sprite, int type)
        {
            m_grid[coordinate.x, coordinate.y] = new CellModel(coordinate, sprite, type);
        }

        public void EmptyCell(Coordinate coordinate)
        {
            m_grid[coordinate.x, coordinate.y].EmptyCell();
        }

        public CellModel GetCell(Coordinate coordinate)
        {
            return m_grid[coordinate.x, coordinate.y];
        }
    }
}