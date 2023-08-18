using System;
using UnityEngine;
using UnityEngine.UI;

namespace JGM.Game
{
    public class CellView : MonoBehaviour
    {
        [SerializeField] private Image m_image;
        [SerializeField] private Button m_button;

        public void Initialize(CellModel model, Action<CellModel> onClickCell)
        {
            m_image.color = model.color;
            m_button.onClick.RemoveAllListeners();
            m_button.onClick.AddListener(() => onClickCell(model));
        }
    }
}