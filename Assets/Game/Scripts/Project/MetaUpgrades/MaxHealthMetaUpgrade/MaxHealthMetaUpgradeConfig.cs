using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    [CreateAssetMenu(
        fileName = "MaxHealthMetaUpgradeConfig",
        menuName = "Configs/MetaUpgrades/new MaxHealthMetaUpgradeConfig",
        order = 0)]
    public class MaxHealthMetaUpgradeConfig : MetaUpgradeConfig
    {
        [PropertyOrder(21)]
        [Space]
        [SerializeField] private ExtraHealthData[] _extraHealthTable;


        public int GetExtraHealth(int level)
        {
            if (level < 0)
            {
                throw new ArgumentOutOfRangeException($"The level {level} cannot be less than zero!");
            }
            else if (level == 0)
            {
                return 0;
            }
            else if (level > MaxLevel)
            {
                throw new ArgumentOutOfRangeException($"Level {level} exceeded maximum!");
            }

            return _extraHealthTable[level - 1].ExtraHealth;
        }

        public override MetaUpgrade CreateMetaUpgrade() => new MaxHealthMetaUpgrade(this);

        protected override void OnValidate()
        {
            base.OnValidate();

            var oldTable = _extraHealthTable;
            _extraHealthTable = new ExtraHealthData[MaxLevel];

            for (int i = 0; i < _extraHealthTable.Length; i++)
            {
                if (i < oldTable.Length)
                {
                    _extraHealthTable[i].ExtraHealth = oldTable[i].ExtraHealth;
                }

                _extraHealthTable[i].Level = i + 1;
            }
        }


        [Serializable]
        public struct ExtraHealthData
        {
            [ReadOnly]
            public int Level;
            public int ExtraHealth;
        }
    }
}