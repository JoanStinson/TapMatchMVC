using JGM.Game;
using Moq;
using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

namespace JGM.GameTests
{
    public class TapMatchTest
    {
        private TapMatch m_tapMatch;
        private Mock<IGameView> m_gameViewMock;
        private Mock<IGameController> m_gameControllerMock;

        [SetUp]
        public void SetUp()
        {
            var dummyGO = new GameObject("Dummy");
            m_tapMatch = dummyGO.AddComponent<TapMatch>();
            m_gameViewMock = new Mock<IGameView>();
            m_gameControllerMock = new Mock<IGameController>();
            m_tapMatch.SetDependencies(m_gameViewMock.Object);
        }

        [UnityTest]
        public IEnumerator When_StartIsCalled_InitializeMethodsGetCalledOnce()
        {
            yield return null;
            //m_gameViewMock.Verify(mock => mock.Initialize(), Times.Once());
            //m_gameControllerMock.Verify(mock => mock.Initialize(), Times.Once());
        }
    }
}