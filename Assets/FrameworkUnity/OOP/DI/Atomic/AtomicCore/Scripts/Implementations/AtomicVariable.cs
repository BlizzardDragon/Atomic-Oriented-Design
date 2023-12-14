using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Atomic
{
    [Serializable]
    public class AtomicVariable<T> : IAtomicVariable<T>, IAtomicObservable<T>, IDisposable
    {
        public T Value
        {
            get { return this.value; }
            set
            {
                SetValue(value);
            }
        }

        protected virtual void SetValue(T value)
        {
            this.value = value;
            this.onChanged?.Invoke(value);
        }

        public void Subscribe(Action<T> listener)
        {
            this.onChanged += listener;
        }

        public void Unsubscribe(Action<T> listener)
        {
            this.onChanged -= listener;
        }

        private Action<T> onChanged;

        [OnValueChanged("OnValueChanged")]
        [SerializeField]
        private T value;

        public AtomicVariable()
        {
            this.value = default;
        }

        public AtomicVariable(T value)
        {
            this.value = value;
        }
        
        public static implicit operator T(AtomicVariable<T> value)
        {
            return value.value;
        }

#if UNITY_EDITOR
        private void OnValueChanged(T value)
        {
            this.onChanged?.Invoke(value);
        }
#endif
        public void Dispose()
        {
            AtomicExtensions.Dispose(ref this.onChanged);
        }
    }
}