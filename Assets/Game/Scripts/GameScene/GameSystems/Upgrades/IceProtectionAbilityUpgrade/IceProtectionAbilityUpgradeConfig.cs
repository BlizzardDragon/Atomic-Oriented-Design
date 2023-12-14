using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    [CreateAssetMenu(
        fileName = "IceProtectionAbilityUpgradeConfig", 
        menuName = "Configs/Upgrades/IceProtectionAbilityUpgradeConfig", 
        order = 0)]
    public class IceProtectionAbilityUpgradeConfig : UpgradeConfig
    {
        public override Upgrade CreateUpgrade()
        {
            return new IceProtectionAbilityUpgrade(this);
        }
    }
}