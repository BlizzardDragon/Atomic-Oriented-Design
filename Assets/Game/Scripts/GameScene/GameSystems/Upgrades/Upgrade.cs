using System;

namespace AtomicOrientedDesign.Shooter
{
    public abstract class Upgrade
    {
        public string Id => Name;
        public string Name => _config.Name;
        public int Level => _level;
        public int MaxLevel => _config.MaxLevel;
        public bool IsMaxLevel => _level >= _config.MaxLevel;
        public UpgradeConfig Config => _config;

        private int _level;
        private readonly UpgradeConfig _config;

        public Upgrade(UpgradeConfig config)
        {
            _config = config;
            _level = 0;
        }


        public void LevelUp()
        {
            if (IsMaxLevel)
            {
                throw new Exception("Max level reached!");
            }

            _level++;
            OnUpgrade(_level);
        }

        protected abstract void OnUpgrade(int level);
    }
}
