using Zenject;

namespace AtomicOrientedDesign.Shooter
{
    public class MetaUpgradeService
    {
        private MoneyStorage _moneyStorage;
        private MetaUpgrade[] _upgrades;


        [Inject]
        public void Construct(MoneyStorage moneyStorage)
        {
            _moneyStorage = moneyStorage;
        }

        public void SetupUpgrades(MetaUpgrade[] metaUpgrades) => _upgrades = metaUpgrades;
        public MetaUpgrade[] GetMetaUpgrades() => _upgrades;

        public void ApplyMetaUpgrades(Context context)
        {
            foreach (var upgrade in _upgrades)
            {
                context.Container.Inject(upgrade);
                upgrade.Apply();
            }
        }

        public void TryBuyUpgrade(MetaUpgrade upgrade)
        {
            if (upgrade.LevelIsMax) return;

            var cost = upgrade.UpgradeCost;
            if (_moneyStorage.Money >= cost)
            {
                _moneyStorage.SpendMoney(cost);
                upgrade.LevelUp();
            }
        }
    }
}
