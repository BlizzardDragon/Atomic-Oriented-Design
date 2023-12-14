using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    [CreateAssetMenu(
        fileName = "PhotonCoreAbilityUpgradeConfig", 
        menuName = "Configs/Upgrades/PhotonCoreAbilityUpgradeConfig", 
        order = 0)]
    public class PhotonCoreAbilityUpgradeConfig : UpgradeConfig
    {
        public override Upgrade CreateUpgrade()
        {
            return new PhotonCoreAbilityUpgrade(this);
        }
    }
}