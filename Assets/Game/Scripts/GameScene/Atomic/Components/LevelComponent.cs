using System;
using Atomic;

namespace AtomicOrientedDesign.Shooter
{
    public interface ILevelComponent
    {
        int Level { get; }
        event Action<int> OnLevelChanged;
        void SetLevel(int level);
    }

    public sealed class LevelComponent : ILevelComponent
    {
        public int Level => _level.Value;
        private readonly AtomicVariable<int> _level;

        public event Action<int> OnLevelChanged
        {
            add => _level.Subscribe(value);
            remove => _level.Unsubscribe(value);
        }

        public LevelComponent(AtomicVariable<int> level)
        {
            _level = level;
        }

        public void SetLevel(int level) => _level.Value = level;
    }
}