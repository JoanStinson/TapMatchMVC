using System;
using System.Collections.Generic;
using UnityEditor;

namespace JGM.GameEditor
{
    public static class UIPrefabsGetter
    {
        private const string m_uiPrefabsPath = "Assets/Prefabs/UI";

        public static List<Object> GetAllPrefabs()
        {
            string[] temp = AssetDatabase.GetAllAssetPaths();
            List<Object> result = new List<Object>();

            foreach (string prefabPath in temp)
            {
                if (prefabPath.Contains(m_uiPrefabsPath) && prefabPath.Contains(".prefab"))
                {
                    Object prefabObject = AssetDatabase.LoadMainAssetAtPath(prefabPath);
                    result.Add(prefabObject);
                }
            }

            return result;
        }
    }
}