using UnityEngine;

namespace JGM.Game
{
    public class AssetLibrary<T> : ScriptableObject
    {
        public T[] Assets => m_assets;

        [SerializeField]
        private T[] m_assets;

        public void SetAssets(T[] assets)
        {
            m_assets = assets;
        }
    }
}