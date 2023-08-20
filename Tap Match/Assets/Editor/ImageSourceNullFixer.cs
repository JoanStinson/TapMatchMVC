using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace JGM.GameEditor
{
    public class ImageSourceNullFixer : Editor
    {
        public const string defaultWhiteAssetSpritePath = "Assets/Art/White.png";

        [MenuItem("Tools/UI/Fix Image Source Null &1")]
        public static void FixImageSourceNull()
        {
            var allPrefabs = UIPrefabsGetter.GetAllPrefabs();
            var defaultImageSprite = AssetDatabase.LoadAssetAtPath<Sprite>(defaultWhiteAssetSpritePath);

            foreach (var prefabAsset in allPrefabs)
            {
                GameObject go = (GameObject)prefabAsset;
                var imageComponents = go.GetComponentsInChildren<Image>(true);

                foreach (var image in imageComponents)
                {
                    if (image.sprite == null)
                    {
                        image.sprite = defaultImageSprite;
                        PrefabUtility.SavePrefabAsset(go);
                    }
                }
            }
        }
    }
}