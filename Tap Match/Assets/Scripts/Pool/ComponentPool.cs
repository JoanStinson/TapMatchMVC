using UnityEngine;

namespace JGM.Game
{
    public class ComponentPool<T> where T : Component
    {
        protected readonly T[] m_pool;

        public ComponentPool(int poolSize, Transform poolParent)
        {
            m_pool = new T[poolSize];
            for (int i = 0; i < poolSize; ++i)
            {
                //TODO use conditional for this
                var pooledGO = new GameObject($"Pooled {typeof(T)} {i + 1}");
                pooledGO.transform.SetParent(poolParent);
                pooledGO.SetActive(false);
                var pooledComponent = pooledGO.AddComponent<T>();
                m_pool[i] = pooledComponent;
            }
        }

        public void Get(out T component)
        {
            component = null;
            for (int i = 0; i < m_pool.Length; ++i)
            {
                if (!m_pool[i].gameObject.activeSelf)
                {
                    component = m_pool[i];
                    component.gameObject.SetActive(true);
                    return;
                }
            }
        }

        public void Destroy()
        {
            for (int i = 0; i < m_pool.Length; ++i)
            {
                GameObject.Destroy(m_pool[i].gameObject);
            }
        }
    }
}