using System.Collections.Generic;
using UnityEngine;

namespace Entities
{
    [DisallowMultipleComponent]
    public abstract class Entity : EntityBase
    {
        private readonly Dictionary<string, object> references = new();
        private bool initialized;

        public sealed override T GetReference<T>(string name)
        {
            this.CheckForInitialize();

            if (this.references.TryGetValue(name, out var value))
            {
                return value as T;
            }

            return default;
        }

        public sealed override bool TryGetReference<T>(string name, out T reference)
        {
            this.CheckForInitialize();

            if (this.references.TryGetValue(name, out var value))
            {
                reference = value as T;
                return true;
            }

            reference = default;
            return false;
        }

        public sealed override bool ContainsReference(string name)
        {
            this.CheckForInitialize();
            return this.references.ContainsKey(name);
        }
        
        private void CheckForInitialize()
        {
            if (!this.initialized)
            {
                this.BindReferences(this.references);
                this.initialized = true;
            }
        }

        protected abstract void BindReferences(Dictionary<string, object> container);
    }
}