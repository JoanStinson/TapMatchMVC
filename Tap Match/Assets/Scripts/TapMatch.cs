using UnityEngine;
using Zenject;

namespace JGM.Game
{
    public class TapMatch : MonoBehaviour
    {
        [SerializeField] private GameSettings m_settings;
        [SerializeField] private GameView m_gameView;

        [Inject]
        private IAudioService m_audioService;

        private void Start()
        {
            m_gameView.Initialize(m_settings);
            m_audioService.Play(AudioFileNames.BackgroundMusic, true);
        }
    }
}