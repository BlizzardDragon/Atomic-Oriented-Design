using Zenject;

namespace AtomicOrientedDesign.Shooter
{
    public class MagnetMetaUpgrade : MetaUpgrade
    {
        private PlayerEntity _player;

        public MagnetMetaUpgrade(MagnetMetaUpgradeConfig config) : base(config)
        {

        }


        [Inject]
        public void Construct(PlayerEntity player) => _player = player;

        protected override void OnApply()
        {
            var magnetComponent = _player.Get<MagnetComponent>();

            var config = Config as MagnetMetaUpgradeConfig;
            float extraRadius = config.GetExtraRadius(Level);

            magnetComponent.IncreaseRadius(extraRadius);
        }
    }
}