using System.Collections;
using UnityEngine;
using NUnit.Framework;
using Moq;
using JGM.Game;
using UnityEngine.TestTools;

namespace JGM.GameTests
{
    public class AudioServiceTest
    {
        private AudioService audioService;
        private AudioLibrary mockAudioLibrary;
        private Mock<ICoroutineService> mockCoroutineService;
        private Mock<ComponentPool<AudioSource>> mockPool;

        [SetUp]
        public void SetUp()
        {
            mockCoroutineService = new Mock<ICoroutineService>();
            mockPool = new Mock<ComponentPool<AudioSource>>();

            audioService = new GameObject().AddComponent<AudioService>();

            mockAudioLibrary = ScriptableObject.CreateInstance<AudioLibrary>();
            mockAudioLibrary.SetAssets(new AudioClip[0]);

            audioService.SetDependencies(mockAudioLibrary, mockCoroutineService.Object, mockPool.Object);
            audioService.Initialize();
        }

        [Test]
        public void When_PlayWithNonExistingAudio_Expect_WarningLogged()
        {
            string audioFileName = "NonExistingAudio";

            audioService.Play(audioFileName);

            LogAssert.Expect(LogType.Warning, "Trying to play a clip that doesn't exist!");
            mockPool.Verify(p => p.Get(), Times.Never());
            mockCoroutineService.Verify(c => c.StartExternalCoroutine(It.IsAny<IEnumerator>()), Times.Never());
        }
    }
}
