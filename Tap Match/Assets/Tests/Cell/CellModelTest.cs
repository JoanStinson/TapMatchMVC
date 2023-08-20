using NUnit.Framework;
using UnityEngine;
using JGM.Game;
using UnityEditor;
using JGM.GameEditor;

namespace JGM.GameTests
{
    public class CellModelTest
    {
        [Test]
        public void When_InitializingCellModel_Expect_CorrectInitialization()
        {
            Coordinate coordinate = new Coordinate(0, 0);
            CellAsset cellAsset = new CellAsset(null, null);
            int type = 1;

            CellModel cellModel = new CellModel(coordinate, cellAsset, type);

            Assert.AreEqual(coordinate, cellModel.coordinate);
            Assert.AreEqual(cellAsset.sprite, cellModel.sprite);
            Assert.AreEqual(cellAsset.overrideController, cellModel.overrideController);
            Assert.AreEqual(type, cellModel.type);
            Assert.IsTrue(cellModel.needsToAnimate);
        }

        [Test]
        public void When_SettingValues_Expect_CorrectValuesSet()
        {
            Coordinate coordinate = new Coordinate(1, 2);
            CellAsset cellAsset = new CellAsset(null, null);
            int type = 2;
            bool needsToAnimate = false;
            CellModel cellModel = new CellModel(new Coordinate(0, 0), new CellAsset(null, null), 0);

            cellModel.SetValues(coordinate, cellAsset, type, needsToAnimate);

            Assert.AreEqual(coordinate, cellModel.coordinate);
            Assert.AreEqual(cellAsset.sprite, cellModel.sprite);
            Assert.AreEqual(cellAsset.overrideController, cellModel.overrideController);
            Assert.AreEqual(type, cellModel.type);
            Assert.AreEqual(needsToAnimate, cellModel.needsToAnimate);
        }

        [Test]
        public void When_EmptyingCell_Expect_CellSpriteToBeNull()
        {
            CellModel cellModel = new CellModel(new Coordinate(0, 0), new CellAsset(null, null), 1);

            cellModel.EmptyCell();

            Assert.IsNull(cellModel.sprite);
        }

        [Test]
        public void When_CheckingIsEmptyForEmptyCell_Expect_True()
        {
            CellModel emptyCellModel = new CellModel(new Coordinate(0, 0), new CellAsset(null, null), 1);

            Assert.IsTrue(emptyCellModel.IsEmpty());
        }

        [Test]
        public void When_CheckingIsEmptyForNonEmptyCell_Expect_False()
        {
            Sprite dummySprite = AssetDatabase.LoadAssetAtPath<Sprite>(ImageSourceNullFixer.defaultWhiteAssetSpritePath);
            CellModel nonEmptyCellModel = new CellModel(new Coordinate(0, 0), new CellAsset(dummySprite, null), 1);

            Assert.IsFalse(nonEmptyCellModel.IsEmpty());
        }
    }
}