using Zenject;

namespace AtomicOrientedDesign.Shooter
{
    public class IceProtectionAbilityUpgrade : Upgrade
    {
        private AbilitySystem _abilitySystem;

        public IceProtectionAbilityUpgrade(IceProtectionAbilityUpgradeConfig config) : base(config)
        {
        }


        [Inject]
        public void Construct(AbilitySystem abilitySystem) => _abilitySystem = abilitySystem;

        protected override void OnUpgrade(int level)
        {
            var ability = _abilitySystem.GetAblility(AbilityType.IceProtection);
            ability.SetLevel(level);
        }
    }
}