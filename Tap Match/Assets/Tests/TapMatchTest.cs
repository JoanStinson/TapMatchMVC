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
            m_coordinate = new Coordinate(1, 1);
        }

        /* 0 X 0
         * X X X
         * 0 X 0
         * Only vertically and horizontally
         */
        [TestCase(0, 0, false)]
        [TestCase(0, 1, true)]
        [TestCase(0, 2, false)]
        [TestCase(1, 0, true)]
        [TestCase(1, 2, true)]
        [TestCase(2, 0, false)]
        [TestCase(2, 1, true)]
        [TestCase(2, 2, false)]
        public void When_IsAdjacentIsCalled_ReturnsExpectedValue(int xCoord, int yCoord, bool expectedResult)
        {
            var newCoordinate = new Coordinate(xCoord, yCoord);
            bool actualResult = m_coordinate.IsAdjacent(newCoordinate);
            Assert.AreEqual(expectedResult, actualResult);
        }
    }

    public class CellModelTest
    {
        private CellModel m_cellModel;

        [SetUp]
        public void SetUp()
        {
            m_cellModel = new CellModel(new Coordinate(1, 1), Color.white);
        }

        /* P W P
         * W W B
         * P B P
         * Only vertically and horizontally
         */
        [TestCase(0, 0, "pink", false)]
        [TestCase(0, 1, "white", true)]
        [TestCase(0, 2, "pink", false)]
        [TestCase(1, 0, "white", true)]
        [TestCase(1, 2, "blue", false)]
        [TestCase(2, 0, "pink", false)]
        [TestCase(2, 1, "blue", false)]
        [TestCase(2, 2, "pink", false)]
        public void When_IsConnectedIsCalled_ReturnsExpectedValue(int xCoord, int yCoord, string colorStr, bool expectedResult)
        {
            var color = (colorStr == "white") ? Color.white : Color.red;
            var newCellModel = new CellModel(new Coordinate(xCoord, yCoord), color);
            bool actualResult = m_cellModel.IsConnected(newCellModel);
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}