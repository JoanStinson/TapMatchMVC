using UnityEngine;

namespace JGM.Game
{
    public class AudioSourcePool : ComponentPool<AudioSource>
    {
        private readonly MonoBehaviour m_monoBehaviour;

        public AudioSourcePool(int poolSize, Transform poolParent, MonoBehaviour monoBehaviour) : base(poolSize, poolParent)
        {
            m_monoBehaviour = monoBehaviour;
        }

        public void Play(AudioClip audioClip, bool loop = false)
        {
            if (!loop)
            {
                PlayOneShot(audioClip);
            }
            else
            {
                PlayLooped(audioClip);
            }
        }

        public void Stop(AudioClip audioClip)
        {
            foreach (var audioSource in m_pool)
            {
                if (audioSource.isPlaying && audioSource.clip == audioClip)
                {
                    audioSource.Stop();
                    audioSource.gameObject.SetActive(false);
                    break;
                }
            }
        }

        public bool IsPlaying(AudioClip audioClip)
        {
            foreach (var audioSource in m_pool)
            {
                if (audioSource.gameObject.activeSelf && audioSource.clip == audioClip)
                {
                    return audioSource.isPlaying;
                }
            }
            return false;
        }

        public void SetVolume(AudioClip audioClip, in float volume)
        {
            foreach (var audioSource in m_pool)
            {
                if (audioSource.clip == audioClip)
                {
                    audioSource.volume = volume;
                    break;
                }
            }
        }

        private void PlayOneShot(AudioClip audioClip)
        {
            Get(out var audioSource);
            audioSource.loop = false;
            audioSource.PlayOneShot(audioClip);
            m_monoBehaviour.StartCoroutine(ObjectDisabler.DisableAudioSourceAfterFinishedPlaying(audioSource));
        }

        private void PlayLooped(AudioClip audioClip)
        {
            Get(out var audioSource);
            audioSource.clip = audioClip;
            audioSource.loop = true;
            audioSource.Play();
            m_monoBehaviour.StartCoroutine(ObjectDisabler.DisableAudioSourceAfterFinishedPlaying(audioSource));
        }
    }
}