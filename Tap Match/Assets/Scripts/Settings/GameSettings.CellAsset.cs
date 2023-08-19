using System;
using UnityEngine;

namespace JGM.Game
{
    public partial class GameSettings
    {
        [Serializable]
        public class CellAsset
        {
            public Sprite sprite;
            public AnimatorOverrideController animatorController;

            public CellAsset(Sprite sprite, AnimatorOverrideController animatorController)
            {
                this.sprite = sprite;
                this.animatorController = animatorController;
            }
        }
    }
}