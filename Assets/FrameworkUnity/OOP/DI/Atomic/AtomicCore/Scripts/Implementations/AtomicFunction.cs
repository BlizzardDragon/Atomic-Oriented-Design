using System;
using Sirenix.OdinInspector;

namespace Atomic
{
    [Serializable]
    public sealed class AtomicFunction<T> : IAtomicFunction<T>
    {
        private Func<T> func;

        public AtomicFunction(Func<T> func)
        {
            this.func = func;
        }

        public AtomicFunction()
        {
        }

        [ShowInInspector, ReadOnly]
        public T Value
        {
            get { return this.func != null ? this.func.Invoke() : default; }
        }

        public void Use(Func<T> func)
        {
            this.func = func;
        }

        public T Invoke()
        {
            return this.func != null ? this.func.Invoke() : default;
        }
    }

    [Serializable]
    public sealed class AtomicFunction<T, R> : IAtomicFunction<T, R>
    {
        private Func<T, R> func;

        public AtomicFunction()
        {
        }

        public AtomicFunction(Func<T, R> func)
        {
            this.func = func;
        }

        public void Use(Func<T, R> func)
        {
            this.func = func;
        }

        [Button]
        public R Invoke(T args)
        {
            return this.func.Invoke(args);
        }
    }
}