using Zenject;

namespace AtomicOrientedDesign.Shooter
{
    public class PhotonCoreAbilityUpgrade : Upgrade
    {
        private AbilitySystem _abilitySystem;

        public PhotonCoreAbilityUpgrade(PhotonCoreAbilityUpgradeConfig config) : base(config)
        {
        }


        [Inject]
        public void Construct(AbilitySystem abilitySystem) => _abilitySystem = abilitySystem;

        protected override void OnUpgrade(int level)
        {
            var ability = _abilitySystem.GetAblility(AbilityType.PhotonCore);
            ability.SetLevel(level);
        }
    }
}