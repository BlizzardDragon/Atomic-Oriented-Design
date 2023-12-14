using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    [CreateAssetMenu(
        fileName = "FireGrenadeAbilityUpgradeConfig", 
        menuName = "Configs/Upgrades/FireGrenadeAbilityUpgradeConfig", 
        order = 0)]
    public class FireGrenadeAbilityUpgradeConfig : UpgradeConfig
    {
        public override Upgrade CreateUpgrade()
        {
            return new FireGrenadeAbilityUpgrade(this);
        }
    }
}