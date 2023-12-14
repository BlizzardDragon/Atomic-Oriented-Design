using System;

namespace Atomic
{
    [Serializable]
    public sealed class AtomicProperty<T> : IAtomicVariable<T>
    {
        public T Value
        {
            get { return this.getter.Invoke(); }
            set { this.setter.Invoke(value); }
        }

        private Func<T> getter;
        private Action<T> setter;

        public AtomicProperty()
        {
        }

        public AtomicProperty(Func<T> getter, Action<T> setter)
        {
            this.getter = getter;
            this.setter = setter;
        }

        public void Use(Func<T> getter, Action<T> setter)
        {
            this.getter = getter;
            this.setter = setter;
        }
    }
}