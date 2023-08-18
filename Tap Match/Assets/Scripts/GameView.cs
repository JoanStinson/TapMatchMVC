using System;
using UnityEngine;
using UnityEngine.UI;

namespace JGM.Game
{
    public class GameView : MonoBehaviour, IGameView
    {
        [SerializeField] private GridLayoutGroup m_grid;
        [SerializeField] private CellView m_cellViewPrefab;

        private GameController m_controller;

        public void Initialize(GameSettings gameSettings)
        {
            m_controller = new GameController();

            for (int i = 0; i < m_grid.transform.childCount; i++)
            {
                Destroy(m_grid.transform.GetChild(i).gameObject);
            }

            var gridModel = m_controller.BuildGridModel(gameSettings);
            for (int i = 0; i < gridModel.rows; i++)
            {
                for (int j = 0; j < gridModel.cols; j++)
                {
                    var go = Instantiate(m_cellViewPrefab, m_grid.transform, false);
                    var cellModel = gridModel.GetCell(i, j);
                    go.Initialize(cellModel, OnClickCell);
                }
            }
        }

        private void OnClickCell(CellModel cellModel)
        {
            Debug.Log("Hello");
        }
    }
}