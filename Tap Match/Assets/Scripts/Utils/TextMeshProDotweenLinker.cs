using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using JGM.Game;

namespace Ubisoft.Tribes
{
    [RequireComponent(typeof(TextMeshProAnimatedBinder))]
    [RequireComponent(typeof(DOTweenAnimation))]
    [AddComponentMenu("UI/TextMeshProDotweenLinker")]
    public class TextMeshProDotweenLinker : MonoBehaviour
    {
        private TextMeshProAnimatedBinder m_binder;
        private List<Tween> m_tweens = new List<Tween>();
        private DOTweenAnimation[] m_anims;

        private void Start()
        {
            m_binder = GetComponent<TextMeshProAnimatedBinder>();
            m_binder.OnStartAnimation += PlayDotweenAnimations;
            m_anims = GetComponents<DOTweenAnimation>();

            foreach (var anim in m_anims)
            {
                anim.CreateTween();
                anim.tween.Pause();
                anim.tween.onComplete += () => ResetTween(anim.tween);
                m_tweens.Add(anim.tween);
            }
        }

        private void OnDestroy()
        {
            if (m_binder != null)
            {
                m_binder.OnStartAnimation -= PlayDotweenAnimations;
            }

            foreach (var tween in m_tweens)
            {
                tween.onComplete -= () => ResetTween(tween);
            }
        }

        private void PlayDotweenAnimations()
        {
            foreach (var anim in m_anims)
            {
                if (anim.tween != null && anim.tween.IsPlaying())
                {
                    anim.DORewind();
                }
            }

            foreach (var tween in m_tweens)
            {
                tween.Play().SetDelay(tween.Delay());
            }
        }

        private void ResetTween(Tween tween)
        {
            var transformInLastFrame = (tween.target as GameObject).transform;
            Vector3 positionInLastFrame = transformInLastFrame.position;
            Quaternion rotationInLastFrame = transformInLastFrame.rotation;
            Vector3 scaleInLastFrame = transformInLastFrame.localScale;
            tween.Rewind();
            transform.position = positionInLastFrame;
            transform.rotation = rotationInLastFrame;
            transform.localScale = scaleInLastFrame;
        }
    }
}