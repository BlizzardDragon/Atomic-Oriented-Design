using UnityEngine;

namespace Entities
{
    [DisallowMultipleComponent]
    public sealed class EntityProxy : EntityBase
    {
        public EntityBase source;

        public override T GetReference<T>(string name) where T : class
        {
            return this.source.GetReference<T>(name);
        }

        public override bool TryGetReference<T>(string name, out T reference) where T : class
        {
            return source.TryGetReference(name, out reference);
        }

        public override bool ContainsReference(string name)
        {
            return source.ContainsReference(name);
        }
    }
}