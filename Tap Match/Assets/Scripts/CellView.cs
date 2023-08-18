using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JGM.Game
{
    public class CellView : MonoBehaviour
    {
        [SerializeField] private Image m_image;
        [SerializeField] private Button m_button;
        [SerializeField] private TextMeshProUGUI m_text;

        public CellModel model { get; internal set; }

        public void Initialize(CellModel model, Action<CellModel> onClickCell)
        {
            this.model = model;
            m_image.color = model.color;
            m_button.onClick.RemoveAllListeners();
            m_button.onClick.AddListener(() => onClickCell(model));
            m_text.text = $"{model.coordinate.x}, {model.coordinate.y}";
        }
    }
}