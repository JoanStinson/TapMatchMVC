using NUnit.Framework;
using JGM.Game;

namespace JGM.GameTests
{
    public class GridModelTest
    {
        [Test]
        public void When_InitializingGridModel_Expect_CorrectInitialization()
        {
            int rows = 7;
            int columns = 8;
            GridModel gridModel = new GridModel(rows, columns);

            Assert.AreEqual(rows, gridModel.rows);
            Assert.AreEqual(columns, gridModel.columns);
        }

        [Test]
        public void When_InitCell_Expect_CellInitializedAtCoordinate()
        {
            int rows = 4;
            int columns = 5;
            GridModel gridModel = new GridModel(rows, columns);
            Coordinate coordinate = new Coordinate(2, 3);
            CellAsset cellAsset = new CellAsset(null, null);
            int type = 1;

            gridModel.InitCell(coordinate, cellAsset, type);

            CellModel cellModel = gridModel.GetCell(coordinate);
            Assert.IsNotNull(cellModel);
            Assert.AreEqual(coordinate, cellModel.coordinate);
            Assert.AreEqual(cellAsset.sprite, cellModel.sprite);
            Assert.AreEqual(cellAsset.overrideController, cellModel.overrideController);
            Assert.AreEqual(type, cellModel.type);
            Assert.IsTrue(cellModel.needsToAnimate);
        }

        [Test]
        public void When_SetCell_Expect_CellValuesUpdated()
        {
            int rows = 6;
            int columns = 9;
            GridModel gridModel = new GridModel(rows, columns);
            Coordinate coordinate = new Coordinate(2, 3);
            CellAsset cellAsset = new CellAsset(null, null);
            int type = 1;
            bool needsToAnimate = false;
            gridModel.InitCell(coordinate, new CellAsset(null, null), 0);

            gridModel.SetCell(coordinate, cellAsset, type, needsToAnimate);

            CellModel cellModel = gridModel.GetCell(coordinate);
            Assert.AreEqual(coordinate, cellModel.coordinate);
            Assert.AreEqual(cellAsset.sprite, cellModel.sprite);
            Assert.AreEqual(cellAsset.overrideController, cellModel.overrideController);
            Assert.AreEqual(type, cellModel.type);
            Assert.AreEqual(needsToAnimate, cellModel.needsToAnimate);
        }

        [Test]
        public void When_EmptyCell_Expect_CellSpriteToBeNull()
        {
            int rows = 5;
            int columns = 5;
            GridModel gridModel = new GridModel(rows, columns);
            Coordinate coordinate = new Coordinate(2, 3);
            gridModel.InitCell(coordinate, new CellAsset(null, null), 1);

            gridModel.EmptyCell(coordinate);

            CellModel cellModel = gridModel.GetCell(coordinate);
            Assert.IsNull(cellModel.sprite);
        }

        [Test]
        public void When_GettingCell_Expect_CorrectCellReturned()
        {
            int rows = 12;
            int columns = 13;
            GridModel gridModel = new GridModel(rows, columns);
            Coordinate coordinate = new Coordinate(2, 3);
            CellAsset cellAsset = new CellAsset(null, null);
            int type = 1;
            gridModel.InitCell(coordinate, cellAsset, type);

            CellModel cellModel = gridModel.GetCell(coordinate);

            Assert.AreEqual(coordinate, cellModel.coordinate);
            Assert.AreEqual(cellAsset.sprite, cellModel.sprite);
            Assert.AreEqual(cellAsset.overrideController, cellModel.overrideController);
            Assert.AreEqual(type, cellModel.type);
            Assert.IsTrue(cellModel.needsToAnimate);
        }
    }
}