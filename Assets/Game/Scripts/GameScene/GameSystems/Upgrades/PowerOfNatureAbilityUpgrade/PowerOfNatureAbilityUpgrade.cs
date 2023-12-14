using Zenject;

namespace AtomicOrientedDesign.Shooter
{
    public class PowerOfNatureAbilityUpgrade : Upgrade
    {
        private AbilitySystem _abilitySystem;

        public PowerOfNatureAbilityUpgrade(PowerOfNatureAbilityUpgradeConfig config) : base(config)
        {
        }


        [Inject]
        public void Construct(AbilitySystem abilitySystem) => _abilitySystem = abilitySystem;

        protected override void OnUpgrade(int level)
        {
            var ability = _abilitySystem.GetAblility(AbilityType.PowerOfNature);
            ability.SetLevel(level);
        }
    }
}