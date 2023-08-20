using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace JGM.Game
{
    public sealed class AudioService : MonoBehaviour, IAudioService
    {
        [SerializeField]
        [Range(1, 20)]
        private int m_maxSimultaneousAudioSources = 10;

        [Inject] private AudioLibrary m_audioAssets;
        [Inject] private ICoroutineService m_coroutineService;

        private ComponentPool<AudioSource> m_pool;
        private Dictionary<string, AudioClip> m_audioLibrary;

        private void Awake()
        {
            m_pool = new ComponentPool<AudioSource>(m_maxSimultaneousAudioSources, transform);
            m_audioLibrary = new Dictionary<string, AudioClip>();

            for (int i = 0; i < m_audioAssets.Assets.Length; i++)
            {
                m_audioLibrary.Add(m_audioAssets.Assets[i].name, m_audioAssets.Assets[i]);
            }
        }

        public void Play(string audioFileName, bool loop = false)
        {
            if (m_audioLibrary.TryGetValue(audioFileName, out var audioClip))
            {
                var audioSource = m_pool.Get();

                if (audioSource == null)
                {
                    Debug.LogWarning("No available audio source in the pool!");
                    return;
                }

                audioSource.clip = audioClip;
                audioSource.loop = loop;
                audioSource.Play();

                if (!loop)
                {
                    var coroutine = DisableAudioSourceAfterFinishedPlaying(audioSource, audioClip.length);
                    m_coroutineService.StartExternalCoroutine(coroutine);
                }
            }
            else
            {
                Debug.LogWarning("Trying to play a clip that doesn't exist!");
            }
        }

        private IEnumerator DisableAudioSourceAfterFinishedPlaying(AudioSource audioSource, float duration)
        {
            yield return new WaitForSeconds(duration);
            m_pool.Return(audioSource);
        }
    }
}