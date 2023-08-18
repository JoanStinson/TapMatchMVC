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
            m_tapMatch.SetDependencies(m_gameViewMock.Object, m_gameControllerMock.Object);
        }

        [UnityTest]
        public IEnumerator When_StartIsCalled_InitializeMethodsGetCalledOnce()
        {
            yield return null;
            m_gameViewMock.Verify(mock => mock.Initialize(), Times.Once());
            m_gameControllerMock.Verify(mock => mock.Initialize(), Times.Once());
        }
    }

    public class CoordinateTest
    {
        private Coordinate m_coordinate;

        [SetUp]
        public void SetUp()
        {
            m_coordinate = new Coordinate(1, 1, Color.white);
        }

        /* 0 X 0
         * X X X
         * 0 X 0
         * Only vertically and horizontally
         */
        [TestCase(false, 0, 0)]
        [TestCase(true, 0, 1)]
        [TestCase(false, 0, 2)]
        [TestCase(true, 1, 0)]
        [TestCase(true, 1, 2)]
        [TestCase(false, 2, 0)]
        [TestCase(true, 2, 1)]
        [TestCase(false, 2, 2)]
        public void When_IsConnectedIsCalled_ReturnsExpectedValue(bool expectedResult, params int[] coordinates)
        {
            var newCoordinate = new Coordinate(coordinates[0], coordinates[1], Color.white);
            bool actualResult = m_coordinate.IsConnected(newCoordinate);
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}