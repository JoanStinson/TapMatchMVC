using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace JGM.Game
{
    public class CellView : MonoBehaviour
    {
        public class Factory : PlaceholderFactory<CellView> { }
        public class SlotFactory : PlaceholderFactory<Transform> { }

        [SerializeField] private Image m_image;
        [SerializeField] private Button m_button;
        [SerializeField] private TextMeshProUGUI m_text;
        [SerializeField] private Animator m_animator;

        public void Initialize(CellModel model, Action<CellModel> onClickCell)
        {
            m_image.sprite = model.sprite;
            m_button.onClick.RemoveAllListeners();
            m_button.onClick.AddListener(() => onClickCell(model));
            m_text.text = $"{model.coordinate.x}, {model.coordinate.y}";
            m_animator.runtimeAnimatorController = model.animatorController;

            if (model.needsToAnimate)
            {
                m_animator.Rebind();
                model.needsToAnimate = false;
            }
            else
            {
                m_animator.SetTrigger("Idle");
            }
        }

        public void PlayPopAnimation()
        {
            m_animator.SetTrigger("Pop");
        }
    }
}