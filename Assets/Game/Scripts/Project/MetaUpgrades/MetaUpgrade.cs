using System;
using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    public abstract class MetaUpgrade
    {
        public string Name => _config.Name;
        public int Level => _level;
        public bool LevelIsMax => _level >= MaxLevel;
        public int MaxLevel => _config.MaxLevel;
        public int UpgradeCost => _level < MaxLevel ? _config.GetCost(_level + 1) : int.MaxValue;
        public Sprite Sprite => _config.Icon;
        public string Description => _config.Description;
        public MetaUpgradeConfig Config => _config;

        private int _level;
        private MetaUpgradeConfig _config;

        public event Action<int> OnLevelUp;

        public MetaUpgrade(MetaUpgradeConfig config)
        {
            _config = config;
            _level = 0;
        }

        public void LevelUp()
        {
            if (_level >= MaxLevel)
            {
                throw new Exception($"Maximum level ({MaxLevel}) reached!");
            }

            _level++;
            OnLevelUp?.Invoke(_level);
        }

        public void SetLevel(int level)
        {
            if (level < 0 || level > MaxLevel)
            {
                throw new Exception($"Level {level} is wrong!");
            }

            _level = level;
        }

        public void Apply()
        {
            if (_level > 0)
            {
                OnApply();
            }
        }

        protected abstract void OnApply();
    }
}
