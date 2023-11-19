using UnityEngine;

namespace Utils
{
    public abstract class MethodHolder: ScriptableObject
    {
        public abstract void InvokeMethod(GameObject gameObject);
    }
}