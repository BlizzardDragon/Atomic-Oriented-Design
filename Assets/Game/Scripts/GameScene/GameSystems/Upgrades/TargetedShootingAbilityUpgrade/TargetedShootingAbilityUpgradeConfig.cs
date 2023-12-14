using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    [CreateAssetMenu(
        fileName = "TargetedShootingAbilityUpgradeConfig", 
        menuName = "Configs/Upgrades/TargetedShootingAbilityUpgradeConfig", 
        order = 0)]
    public class TargetedShootingAbilityUpgradeConfig : UpgradeConfig
    {
        public override Upgrade CreateUpgrade()
        {
            return new TargetedShootingAbilityUpgrade(this);
        }
    }
}