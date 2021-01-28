using UnityEngine;

namespace Level
{
    public abstract class Decoration : ScriptableObject
    {
        public abstract void Setup(Transform actor);
        public abstract void Act(Transform actor);
    }
}