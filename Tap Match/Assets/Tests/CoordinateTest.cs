//using JGM.Game;
//using NUnit.Framework;

//namespace JGM.GameTests
//{
//    public class CoordinateTest
//    {
//        private Coordinate m_coordinate;

//        [SetUp]
//        public void SetUp()
//        {
//            m_coordinate = new Coordinate(1, 1);
//        }

//        /* 0 X 0
//         * X X X
//         * 0 X 0
//         * Only vertically and horizontally
//         */
//        [TestCase(0, 0, false)]
//        [TestCase(0, 1, true)]
//        [TestCase(0, 2, false)]
//        [TestCase(1, 0, true)]
//        [TestCase(1, 2, true)]
//        [TestCase(2, 0, false)]
//        [TestCase(2, 1, true)]
//        [TestCase(2, 2, false)]
//        public void When_IsAdjacentIsCalled_ReturnsExpectedValue(int xCoord, int yCoord, bool expectedResult)
//        {
//            var newCoordinate = new Coordinate(xCoord, yCoord);
//            bool actualResult = m_coordinate.IsAdjacent(newCoordinate);
//            Assert.AreEqual(expectedResult, actualResult);
//        }
//    }
//}