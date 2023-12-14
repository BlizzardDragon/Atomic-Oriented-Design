using Zenject;

namespace AtomicOrientedDesign.Shooter
{
    public class MagnetUpgrade : Upgrade
    {
        private PlayerEntity _playerEntity;
        private readonly MagnetUpgradeConfig _config;

        public MagnetUpgrade(MagnetUpgradeConfig config) : base(config)
        {
            _config = config;
        }


        [Inject]
        public void Construct(PlayerEntity playerEntity) => _playerEntity = playerEntity;

        protected override void OnUpgrade(int level)
        {
            _playerEntity.Get<MagnetComponent>().IncreaseRadius(_config.GetRadius(level));
        }
    }
}