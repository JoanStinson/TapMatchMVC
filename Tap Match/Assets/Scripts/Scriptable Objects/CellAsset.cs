using System;
using UnityEngine;

namespace JGM.Game
{
    [Serializable]
    public class CellAsset
    {
        public Sprite sprite;
        public AnimatorOverrideController overrideController;

        public CellAsset(Sprite sprite, AnimatorOverrideController overrideController)
        {
            this.sprite = sprite;
            this.overrideController = overrideController;
        }
    }
}