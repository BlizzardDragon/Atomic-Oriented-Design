using System;
using Sirenix.OdinInspector;

namespace Atomic
{
    [Serializable]
    public sealed class AtomicAction : IAtomicAction
    {
        private Action action;

        public AtomicAction()
        {
        }

        public AtomicAction(Action action)
        {
            this.action = action;
        }

        public void Use(Action action)
        {
            this.action = action;
        }

        [Button]
        public void Invoke()
        {
            this.action?.Invoke();
        }
    }

    [Serializable]
    public class AtomicAction<T> : IAtomicAction<T>
    {
        private Action<T> action;

        public AtomicAction()
        {
        }

        public AtomicAction(Action<T> action)
        {
            this.action = action;
        }
        
        public void Use(Action<T> action)
        {
            this.action = action;
        }

        [Button]
        public virtual void Invoke(T direction)
        {
            this.action?.Invoke(direction);
        }
    }
    
    [Serializable]
    public sealed class AtomicAction<T1, T2> : IAtomicAction<T1, T2>
    {
        private Action<T1, T2> action;

        public AtomicAction()
        {
        }

        public AtomicAction(Action<T1, T2> action)
        {
            this.action = action;
        }
        
        public void Use(Action<T1, T2> action)
        {
            this.action = action;
        }

        [Button]
        public void Invoke(T1 args1, T2 args2)
        {
            this.action?.Invoke(args1, args2);
        }
    }

}