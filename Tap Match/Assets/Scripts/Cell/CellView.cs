using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JGM.Game
{
    public class CellView : MonoBehaviour
    {
        public CellModel model { get; private set; }

        [SerializeField] private Image m_image;
        [SerializeField] private Button m_button;
        [SerializeField] private TextMeshProUGUI m_text;

        public void Initialize(CellModel model, Action<CellModel> onClickCell)
        {
            this.model = model;
            m_image.sprite = model.sprite;
            m_button.onClick.RemoveAllListeners();
            m_button.onClick.AddListener(() => onClickCell(model));
            m_text.text = $"{model.coordinate.x}, {model.coordinate.y}";
        }
    }
}