using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    [CreateAssetMenu(
        fileName = "MagnetMetaUpgradeConfig",
        menuName = "Configs/MetaUpgrades/new MagnetMetaUpgradeConfig",
        order = 0)]
    public class MagnetMetaUpgradeConfig : MetaUpgradeConfig
    {
        [PropertyOrder(21)]
        [Space]
        [SerializeField] private ExtraRadiusData[] _extraRadiusTable;


        public float GetExtraRadius(int level)
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

            return _extraRadiusTable[level - 1].ExtraMagnetRadius;
        }

        public override MetaUpgrade CreateMetaUpgrade() => new MagnetMetaUpgrade(this);

        protected override void OnValidate()
        {
            base.OnValidate();

            var oldTable = _extraRadiusTable;
            _extraRadiusTable = new ExtraRadiusData[MaxLevel];

            for (int i = 0; i < _extraRadiusTable.Length; i++)
            {
                if (i < oldTable.Length)
                {
                    _extraRadiusTable[i].ExtraMagnetRadius = oldTable[i].ExtraMagnetRadius;
                }

                _extraRadiusTable[i].Level = i + 1;
            }
        }


        [Serializable]
        public struct ExtraRadiusData
        {
            [ReadOnly]
            public int Level;
            public float ExtraMagnetRadius;
        }
    }
}