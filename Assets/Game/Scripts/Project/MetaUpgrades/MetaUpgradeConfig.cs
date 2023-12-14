using FrameworkUnity.Common;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    public abstract class MetaUpgradeConfig : ScriptableObject
    {
        [PropertyOrder (1)]
        public string Name;

        [PropertyOrder (2)]
        [PreviewField]
        public Sprite Icon;

        [PropertyOrder (3)]
        [Min(1)]
        public int MaxLevel;
        
        [PropertyOrder (4)]
        [Min(0)]
        public int StartCost;
        
        [PropertyOrder (5)]
        public float PowMultiplier;

        [PropertyOrder (10)]
        [TextArea]
        public string Description;

        [PropertyOrder (20)]
        [Space]
        [SerializeField] private LevelCostTable _levelCostTable;

        
        public int GetCost(int level) => _levelCostTable.GetCost(level);

        protected virtual void OnValidate()
        {
            _levelCostTable.CalculateTable(MaxLevel, StartCost, PowMultiplier);
        }

        public abstract MetaUpgrade CreateMetaUpgrade();
    }
}