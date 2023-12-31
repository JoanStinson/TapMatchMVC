﻿using UnityEngine;
using UnityEngine.UI;

namespace JGM.Game
{
    public class GameView : MonoBehaviour
    {
        [SerializeField] private TopHeaderView m_topHeaderView;
        [SerializeField] private GridView m_gridView;
        [SerializeField] private Image m_fadePanelImage;
        [SerializeField] private float m_fadeDuration = 1.0f;

        public void Initialize()
        {
            m_topHeaderView.Initialize();
            m_topHeaderView.onClickBombButton += m_gridView.EmptyCellsThatMatchWithBombType;
            m_gridView.Initialize(new GridController());
            m_gridView.onCellsMatch += m_topHeaderView.IncreaseMovesAmount;
            m_fadePanelImage.enabled = true;
            m_fadePanelImage.CrossFadeAlpha(0f, m_fadeDuration, false);
        }
    }
}