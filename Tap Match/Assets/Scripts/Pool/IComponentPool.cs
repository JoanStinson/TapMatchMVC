using UnityEngine;

namespace JGM.Game
{
    public interface IComponentPool<T> where T : Component
    {
        T Get();
        void Return(T component);
    }
}