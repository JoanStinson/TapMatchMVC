using UnityEngine;
using Zenject;

namespace JGM.Game
{
    public class TapMatch : MonoBehaviour
    {
        [SerializeField] 
        private GameView m_gameView;

        [Inject]
        private IAudioService m_audioService;

        private void Start()
        {
            m_gameView.Initialize();
            m_audioService.Play(AudioFileNames.BackgroundMusic, true);
        }
    }
}