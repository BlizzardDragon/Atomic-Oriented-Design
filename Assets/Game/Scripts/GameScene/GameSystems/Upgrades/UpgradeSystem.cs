using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace AtomicOrientedDesign.Shooter
{
    public class UpgradeSystem : MonoBehaviour
    {
        [SerializeField] private UpgradeConfig[] _upgradeConfigs;
        private DiContainer _container;
        private List<Upgrade> _upgrades = new();


        [Inject]
        public void Construct(DiContainer container) => _container = container;

        private void Awake()
        {
            foreach (var config in _upgradeConfigs)
            {
                Upgrade upgrade = config.CreateUpgrade();
                _container.Inject(upgrade);
                _upgrades.Add(upgrade);
            }
        }

        public void RemoveUpgrade(Upgrade upgrade) => _upgrades.Remove(upgrade);

        public Upgrade[] GetRandomUpgrades(int count)
        {
            if (count < 1)
            {
                throw new ArgumentException($"Count({count}) must be greater than 0!");
            }

            if (_upgrades.Count < 1)
            {
                throw new ArgumentException($"Upgrades count({_upgrades.Count}) must be greater than 0!");
            }

            if (_upgrades.Count >= count)
            {
                var indexList = new List<int>();
                for (int i = 0; i < _upgrades.Count; i++)
                {
                    indexList.Add(i);
                }

                var targetIndexList = new List<int>();
                for (int i = 0; i < count; i++)
                {
                    int upgradeIndex = indexList[Random.Range(0, indexList.Count)];
                    indexList.Remove(upgradeIndex);
                    targetIndexList.Add(upgradeIndex);
                }

                var targetUpgrades = new Upgrade[count];
                for (int i = 0; i < count; i++)
                {
                    targetUpgrades[i] = _upgrades[targetIndexList[i]];
                }

                return targetUpgrades;
            }
            else
            {
                var targetUpgrades = new Upgrade[count];
                for (int i = 0; i < targetUpgrades.Length; i++)
                {
                    targetUpgrades[i] = _upgrades[Random.Range(0, _upgrades.Count)];
                }

                return targetUpgrades;
            }
        }
    }
}