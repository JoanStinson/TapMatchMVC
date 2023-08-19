﻿using UnityEngine;

namespace JGM.Game
{
    [CreateAssetMenu(fileName = "Game Settings", menuName = "Game Settings", order = 0)]
    public class GameSettings : ScriptableObject
    {
        public int rows => m_rows;
        public int columns => m_columns;
        public float minCellSize => m_minCellSize;
        public Color[] cellColors
        {
            get { return m_cellColors; }
            set { m_cellColors = value; }
        }

        [SerializeField, Range(5, 20)] private int m_rows;
        [SerializeField, Range(5, 20)] private int m_columns;
        [SerializeField] private float m_minCellSize = 20f;
        [SerializeField] private Color[] m_cellColors = new Color[3];
    }
}