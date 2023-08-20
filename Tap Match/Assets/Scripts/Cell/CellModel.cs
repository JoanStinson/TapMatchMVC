﻿using UnityEngine;

namespace JGM.Game
{
    public class CellModel
    {
        public Coordinate coordinate { get; private set; }
        public Sprite sprite { get; private set; }
        public AnimatorOverrideController animatorController { get; private set; }
        public int type { get; private set; }
        public bool needsToAnimate { get; set; }

        public CellModel(Coordinate coordinate, CellAsset cellAsset, int type)
        {
            SetValues(coordinate, cellAsset, type, true);
        }

        public void SetValues(Coordinate coordinate, CellAsset cellAsset, int type, bool needsToAnimate)
        {
            this.coordinate = coordinate;
            this.sprite = cellAsset.sprite;
            this.animatorController = cellAsset.animatorController;
            this.type = type;
            this.needsToAnimate = needsToAnimate;
        }

        public void EmptyCell()
        {
            sprite = null;
        }

        public bool IsEmpty()
        {
            return sprite == null;
        }
    }
}