using Zenject;

namespace AtomicOrientedDesign.Shooter
{
    public class MaxHealthMetaUpgrade : MetaUpgrade
    {
        private PlayerEntity _player;

        public MaxHealthMetaUpgrade(MaxHealthMetaUpgradeConfig config) : base(config)
        {

        }


        [Inject]
        public void Construct(PlayerEntity player) => _player = player;

        protected override void OnApply()
        {
            var hitPointsComponent = _player.Get<HitPointsComponent>();

            var config = Config as MaxHealthMetaUpgradeConfig;
            var extraHealth = config.GetExtraHealth(Level);

            hitPointsComponent.IncreaseMaxHealth(extraHealth);
            hitPointsComponent.SetHitPoints(hitPointsComponent.MaxHitPoints);
        }
    }
}