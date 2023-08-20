using System.Collections;
using UnityEngine;

namespace JGM.Game
{
    public static class ObjectDisabler
    {
        public const float defaultDelayTime = 5f;

        public static IEnumerator DisableGOAfterDelay(GameObject gameObject, float delayToDisableInSeconds = defaultDelayTime)
        {
            yield return new WaitForSeconds(delayToDisableInSeconds);
            gameObject.SetActive(false);
        }

        public static IEnumerator DisableAudioSourceAfterFinishedPlaying(AudioSource audioSource)
        {
            yield return new WaitWhile(() => audioSource.isPlaying);
            audioSource.gameObject.SetActive(false);
        }
    }
}