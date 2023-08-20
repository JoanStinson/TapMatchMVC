using UnityEngine;
using System.Collections.Generic;

namespace JGM.Game
{
    public class ComponentPool<T> where T : Component
    {
        private readonly Stack<T> m_poolStack = new Stack<T>();

        public ComponentPool(int poolSize, Transform poolParent)
        {
            for (int i = 0; i < poolSize; i++)
            {
                var pooledGO = new GameObject();
                pooledGO.SetName($"Pooled {typeof(T)} {i + 1}");
                pooledGO.transform.SetParent(poolParent);
                pooledGO.SetActive(false);
                var pooledComponent = pooledGO.AddComponent<T>();
                m_poolStack.Push(pooledComponent);
            }
        }

        public T Get()
        {
            if (m_poolStack.Count == 0)
            {
                Debug.LogWarning($"ComponentPool<{typeof(T)}> is empty. All pooled components are in use.");
                return null;
            }

            T component = m_poolStack.Pop();
            component.gameObject.SetActive(true);
            return component;
        }

        public void Return(T component)
        {
            component.gameObject.SetActive(false);
            m_poolStack.Push(component);
        }
    }
}