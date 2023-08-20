using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace JGM.Game
{
    [CreateAssetMenu(fileName = "New Font Library", menuName = "Libraries/Font Library")]
    public class FontLibrary : ScriptableObject
    {
        [Serializable]
        public struct FontEntry
        {
            public string fontName;
            public TMP_FontAsset fontAsset;
        }

        [SerializeField]
        private List<FontEntry> m_fontEntries = new List<FontEntry>();

        private Dictionary<string, TMP_FontAsset> m_fontDictionary = new Dictionary<string, TMP_FontAsset>();

        private void OnEnable()
        {
            m_fontDictionary.Clear();
            foreach (var entry in m_fontEntries)
            {
                if (!m_fontDictionary.ContainsKey(entry.fontName))
                {
                    m_fontDictionary.Add(entry.fontName, entry.fontAsset);
                }
            }
        }

        public TMP_FontAsset GetFontAsset(string newLanguageFontName)
        {
            if (m_fontDictionary.TryGetValue(newLanguageFontName, out TMP_FontAsset fontAsset))
            {
                return fontAsset;
            }
            else
            {
                Debug.LogWarning($"Font asset not found for '{newLanguageFontName}'.");
                return null;
            }
        }
    }
}