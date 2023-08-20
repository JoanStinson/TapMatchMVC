using TMPro;
using UnityEditor;
using UnityEngine;

namespace JGM.GameEditor
{
    [InitializeOnLoad]
    public class MinimumTextSizeFixer : Editor
    {
        private const int MinimumTextSize = 16;

        [MenuItem("Tools/UI/Fix Minimum Text Size &2")]
        public static void FixMinimumTextSize()
        {
            var allPrefabs = UIPrefabsGetter.GetAllPrefabs();

            foreach (var prefabAsset in allPrefabs)
            {
                GameObject go = (GameObject)prefabAsset;
                TextMeshProUGUI[] components = go.GetComponentsInChildren<TextMeshProUGUI>(true);

                foreach (TextMeshProUGUI tmp in components)
                {
                    if (tmp.enableAutoSizing)
                    {
                        if (tmp.fontSizeMin < MinimumTextSize)
                        {
                            tmp.fontSizeMin = MinimumTextSize;
                            PrefabUtility.SavePrefabAsset(go);
                        }
                    }
                    else
                    {
                        if (tmp.fontSize < MinimumTextSize)
                        {
                            tmp.fontSizeMin = MinimumTextSize;
                            PrefabUtility.SavePrefabAsset(go);
                        }
                    }
                }
            }
        }
    }
}