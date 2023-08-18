using UnityEngine;
using static log4net.Appender.ColoredConsoleAppender;

namespace JGM.Game
{
    public class GameController : IGameController
    {
        private Color[] m_colors;

        public void Initialize()
        {

        }

        public GridModel BuildGridModel(GameSettings gameSettings)
        {
            var gridSize = gameSettings.gridSize;
            var gridModel = new GridModel(gridSize.rows, gridSize.cols);
            m_colors = gameSettings.cells.colors;

            for (int i = 0; i < gridSize.rows; i++)
            {
                for (int j = 0; j < gridSize.cols; j++)
                {
                    int randomIndex = Random.Range(0, m_colors.Length);
                    Color color = m_colors[randomIndex];
                    gridModel.SetCell(new Coordinate(i, j), color);
                }
            }

            return gridModel;
        }

        public void FillEmptySlotsAfterCascade(GridModel gridModel)
        {
            for (int col = 0; col < gridModel.cols; col++)
            {
                bool shouldFillColumn = false;

                // Loop through each row in reverse order to check for empty slots
                for (int row = gridModel.rows - 1; row >= 0; row--)
                {
                    if (gridModel.GetCell(row, col).IsEmpty())
                    {
                        shouldFillColumn = true;
                        break;
                    }
                }

                // If there are empty slots, fill the column
                if (shouldFillColumn)
                {
                    for (int row = 0; row < gridModel.rows; row++)
                    {
                        // Generate a new random cell color
                        int randomIndex = Random.Range(0, m_colors.Length);
                        Color color = m_colors[randomIndex];

                        // Fill the empty slot with the new random color
                        if (gridModel.GetCell(row, col).IsEmpty())
                        {
                            gridModel.SetCell(new Coordinate(row, col), color);
                        }
                    }
                }
            }
        }

    }
}