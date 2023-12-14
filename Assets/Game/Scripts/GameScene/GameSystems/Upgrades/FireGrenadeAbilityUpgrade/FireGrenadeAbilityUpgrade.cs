using Zenject;

namespace AtomicOrientedDesign.Shooter
{
    public class FireGrenadeAbilityUpgrade : Upgrade
    {
        private AbilitySystem _abilitySystem;

        public FireGrenadeAbilityUpgrade(FireGrenadeAbilityUpgradeConfig config) : base(config)
        {
        }


        [Inject]
        public void Construct(AbilitySystem abilitySystem) => _abilitySystem = abilitySystem;

        protected override void OnUpgrade(int level)
        {
            var ability = _abilitySystem.GetAblility(AbilityType.FireGrenade);
            ability.SetLevel(level);
        }
    }
}