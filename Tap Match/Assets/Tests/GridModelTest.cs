using JGM.Game;
using NUnit.Framework;
using System;
using UnityEngine;
using UnityEngine.TestTools;

namespace JGM.GameTests
{
    public class GridModelTest
    {
        private GridModel m_gridModel;

        [SetUp]
        public void SetUp()
        {
            m_gridModel = new GridModel(5, 10);
        }

        [TestCase(3, 8)]
        [TestCase(6, 1)]
        [TestCase(5, 3)]
        [TestCase(14, 25)]
        public void When_GridIsCreatedWithIncorrectSize_Expect_AssertError(int rows, int cols)
        {
            m_gridModel = new GridModel(rows, cols);
            LogAssert.Expect(LogType.Assert, "Assertion failed");
        }

        [TestCase(-1, 8)]
        [TestCase(7, 12)]
        [TestCase(6, 11)]
        [TestCase(10, 9)]
        public void When_SetCellIsCalledWithIncorrectSize_Expect_AssertError(int xCoord, int yCoord)
        {
            //LogAssert.Expect(LogType.Assert, "Assertion failed");
            Assert.Throws<IndexOutOfRangeException>(() => m_gridModel.SetCell(new Coordinate(xCoord, yCoord), Color.white));
        }
    }
}