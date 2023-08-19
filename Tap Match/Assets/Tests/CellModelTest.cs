//using JGM.Game;
//using NUnit.Framework;
//using UnityEngine;

//namespace JGM.GameTests
//{
//    public class CellModelTest
//    {
//        private CellModel m_cellModel;

//        [SetUp]
//        public void SetUp()
//        {
//            m_cellModel = new CellModel(new Coordinate(1, 1), Color.white);
//        }

//        /* P W P
//         * W W B
//         * P B P
//         * Only vertically and horizontally
//         */
//        [TestCase(0, 0, "pink", false)]
//        [TestCase(0, 1, "white", true)]
//        [TestCase(0, 2, "pink", false)]
//        [TestCase(1, 0, "white", true)]
//        [TestCase(1, 2, "blue", false)]
//        [TestCase(2, 0, "pink", false)]
//        [TestCase(2, 1, "blue", false)]
//        [TestCase(2, 2, "pink", false)]
//        public void When_IsConnectedIsCalled_ReturnsExpectedValue(int xCoord, int yCoord, string colorStr, bool expectedResult)
//        {
//            var color = (colorStr == "white") ? Color.white : Color.red;
//            var newCellModel = new CellModel(new Coordinate(xCoord, yCoord), color);
//            bool actualResult = m_cellModel.IsConnected(newCellModel);
//            Assert.AreEqual(expectedResult, actualResult);
//        }
//    }
//}