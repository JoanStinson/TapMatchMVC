namespace JGM.Game
{
    public class GridModel
    {
        public virtual int rows { get; private set; }
        public virtual int columns { get; private set; }

        private readonly CellModel[,] m_grid;

        public GridModel() { }

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

        public virtual CellModel GetCell(Coordinate coordinate)
        {
            return m_grid[coordinate.x, coordinate.y];
        }
    }
}