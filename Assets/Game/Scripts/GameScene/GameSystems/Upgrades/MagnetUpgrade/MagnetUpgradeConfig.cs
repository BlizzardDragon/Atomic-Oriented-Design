using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    [CreateAssetMenu(
        fileName = "MagnetUpgradeConfig", 
        menuName = "Configs/Upgrades/MagnetUpgradeConfig", 
        order = 0)]
    public class MagnetUpgradeConfig : UpgradeConfig
    {
        [PropertyOrder (5)]
        [Space]
        public ExtraRadiusData[] ExtraRadiusTable;


        public override Upgrade CreateUpgrade()
        {
            return new MagnetUpgrade(this);
        }

        public float GetRadius(int level)
        {
            if (level < 1)
            {
                throw new ArgumentOutOfRangeException($"Level({level}) cannot be less than 1!");
            }

            return ExtraRadiusTable[level - 1].Radius;
        }

        private void OnValidate() => SetTableLength(MaxLevel);

        private void SetTableLength(int length)
        {
            if (length < 1) return;

            var oldTable = ExtraRadiusTable;
            ExtraRadiusTable = new ExtraRadiusData[length];

            for (int i = 0; i < length; i++)
            {
                if (i < oldTable.Length)
                {
                    ExtraRadiusTable[i].Radius = oldTable[i].Radius;
                }

                ExtraRadiusTable[i].Level = i + 1;
            }
        }

        [Serializable]
        public struct ExtraRadiusData
        {
            [Space]
            [ReadOnly]
            public int Level;
            public float Radius;
        }
    }
}