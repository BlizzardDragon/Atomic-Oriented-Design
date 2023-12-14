using UnityEngine;

namespace Entities
{
    public abstract class EntityBase : MonoBehaviour, IEntity
    {
        public abstract T GetReference<T>(string name) where T : class;
        public abstract bool TryGetReference<T>(string name, out T reference) where T : class;
        public abstract bool ContainsReference(string name);
    }
}