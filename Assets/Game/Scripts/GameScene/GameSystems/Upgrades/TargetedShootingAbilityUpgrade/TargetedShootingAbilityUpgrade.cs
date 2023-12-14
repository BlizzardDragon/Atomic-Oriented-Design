using Zenject;

namespace AtomicOrientedDesign.Shooter
{
    public class TargetedShootingAbilityUpgrade : Upgrade
    {
        private AbilitySystem _abilitySystem;

        public TargetedShootingAbilityUpgrade(TargetedShootingAbilityUpgradeConfig config) : base(config)
        {
        }


        [Inject]
        public void Construct(AbilitySystem abilitySystem) => _abilitySystem = abilitySystem;

        protected override void OnUpgrade(int level)
        {
            var ability = _abilitySystem.GetAblility(AbilityType.TargetedShooting);
            ability.SetLevel(level);
        }
    }
}