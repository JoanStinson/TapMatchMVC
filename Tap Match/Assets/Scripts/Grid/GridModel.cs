namespace JGM.Game
{
    public class GridModel
    {
        public readonly int rows;
        public readonly int columns;

        private readonly CellModel[,] m_grid;

        public GridModel(int rows, int columns)
        {
            this.rows = rows;
            this.columns = columns;
            m_grid = new CellModel[rows, columns];
        }

        public void InitCell(Coordinate coordinate, CellAsset cellAsset, int type)
        {
            m_grid[coordinate.x, coordinate.y] = new CellModel(coordinate, cellAsset, type);
        }

        public void SetCell(Coordinate coordinate, CellAsset cellAsset, int type, bool needsToAnimate)
        {
            m_grid[coordinate.x, coordinate.y].SetValues(coordinate, cellAsset, type, needsToAnimate);
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