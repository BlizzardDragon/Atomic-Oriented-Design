using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    [CreateAssetMenu(
        fileName = "PowerOfNatureAbilityUpgradeConfig", 
        menuName = "Configs/Upgrades/PowerOfNatureAbilityUpgradeConfig", 
        order = 0)]
    public class PowerOfNatureAbilityUpgradeConfig : UpgradeConfig
    {
        public override Upgrade CreateUpgrade()
        {
            return new PowerOfNatureAbilityUpgrade(this);
        }
    }
}