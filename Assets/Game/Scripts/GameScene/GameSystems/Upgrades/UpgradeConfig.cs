using Sirenix.OdinInspector;
using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    public abstract class UpgradeConfig : ScriptableObject
    {
        [PropertyOrder (1)]
        public string Name;

        [PropertyOrder (2)]
        [Min(1)]
        public int MaxLevel;

        [PropertyOrder (3)]
        [PreviewField]
        public Sprite Icon;

        [PropertyOrder (10)]
        [Space]
        public DescriptionTable DescriptionTable;


        public abstract Upgrade CreateUpgrade();
        private void OnValidate() => DescriptionTable.SetTableLength(MaxLevel);
    }
}
