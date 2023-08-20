using NUnit.Framework;
using Moq;
using JGM.Game;
using UnityEngine;

namespace JGM.GameTests
{
    public class GridControllerTest
    {
        private GridController m_gridController;
        private Mock<GridModel> mockGridModel;

        [SetUp]
        public void SetUp()
        {
            m_gridController = new GridController();
            mockGridModel = new Mock<GridModel>();
        }

        [Test]
        public void When_BuildGrid_Expect_CorrectGridBuilt()
        {
            GameSettings settings = ScriptableObject.CreateInstance<GameSettings>();
            settings.SetRowsAndColumns(4, 5);
            settings.cellAssets = new CellAsset[1];
            settings.cellAssets[0] = new CellAsset();
            mockGridModel.Setup(g => g.rows).Returns(settings.rows);
            mockGridModel.Setup(g => g.columns).Returns(settings.columns);

            m_gridController.BuildGrid(settings);

            Assert.AreEqual(settings.rows, mockGridModel.Object.rows);
            Assert.AreEqual(settings.columns, mockGridModel.Object.columns);
        }
    }
}