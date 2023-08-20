using UnityEngine;

namespace JGM.Game
{
    public class AssetLibrary<T> : ScriptableObject
    {
        public T[] Assets => m_assets;

        [SerializeField]
        private T[] m_assets;
    }
}