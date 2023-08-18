using UnityEngine;

namespace JGM.Game
{
    public class GameController : IGameController
    {
        public void Initialize()
        {

        }

        public GridModel BuildGridModel(GameSettings gameSettings)
        {
            var gridSize = gameSettings.gridSize;
            var gridModel = new GridModel(gridSize.rows, gridSize.cols);
            var colors = gameSettings.cells.colors;

            for (int i = 0; i < gridSize.rows; i++)
            {
                for (int j = 0; j < gridSize.cols; j++)
                {
                    int randomIndex = Random.Range(0, colors.Length);
                    Color color = colors[randomIndex];
                    gridModel.SetCell(new Coordinate(i, j), color);
                }
            }

            return gridModel;
        }
    }
}