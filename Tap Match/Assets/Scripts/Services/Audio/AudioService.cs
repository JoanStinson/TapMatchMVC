using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace JGM.Game
{
    public class AudioService : MonoBehaviour, IAudioService
    {
        [SerializeField]
        [Range(1, 20)]
        private int m_maxSimultaneousAudioSources = 10;

        [Inject]
        private AudioLibrary m_audioAssets;

        private AudioSourcePool m_audioSourcePool;
        private Dictionary<string, AudioClip> m_audioLibrary;

        private void Awake()
        {
            m_audioSourcePool = new AudioSourcePool(m_maxSimultaneousAudioSources, transform, this);
            m_audioLibrary = new Dictionary<string, AudioClip>();
            for (int i = 0; i < m_audioAssets.Assets.Length; i++)
            {
                m_audioLibrary.Add(m_audioAssets.Assets[i].name, m_audioAssets.Assets[i]);
            }
        }

        public void Play(string audioFileName, bool loop = false)
        {
            if (!m_audioLibrary.ContainsKey(audioFileName))
            {
                Debug.LogWarning("Trying to play a clip that doesn't exist!");
                return;
            }
            var audioClip = m_audioLibrary[audioFileName];
            m_audioSourcePool.Play(audioClip, loop);
        }

        public void Stop(string audioFileName)
        {
            if (!m_audioLibrary.ContainsKey(audioFileName))
            {
                Debug.LogWarning("Trying to play a clip that doesn't exist!");
                return;
            }
            var audioClip = m_audioLibrary[audioFileName];
            m_audioSourcePool.Stop(audioClip);
        }

        public bool IsPlaying(string audioFileName)
        {
            if (!m_audioLibrary.ContainsKey(audioFileName))
            {
                Debug.LogWarning("Trying to play a clip that doesn't exist!");
                return false;
            }
            var audioClip = m_audioLibrary[audioFileName];
            return m_audioSourcePool.IsPlaying(audioClip);
        }

        public void SetVolume(string audioFileName, float volume)
        {
            if (!m_audioLibrary.ContainsKey(audioFileName))
            {
                Debug.LogWarning("Trying to play a clip that doesn't exist!");
                return;
            }
            var audioClip = m_audioLibrary[audioFileName];
            m_audioSourcePool.SetVolume(audioClip, volume);
        }
    }
}