using System;
using TMPro;
using UnityEngine;

namespace JGM.Game
{
    [CreateAssetMenu(fileName = "New Font Library", menuName = "Libraries/Font Library")]
    public class FontLibrary : AssetLibrary<TMP_FontAsset>
    {
        public TMP_FontAsset GetFontAsset(string newLanguageFontName)
        {
            throw new NotImplementedException();
        }
    }
}