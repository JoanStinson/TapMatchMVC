using System.Diagnostics;
using UnityEngine;

namespace JGM.Game
{
    public static class MonoBehaviourExtensions
    {
        [Conditional("UNITY_EDITOR")]
        public static void SetName(this GameObject gameObject, string name)
        {
            gameObject.name = name;
        }
    }
}
