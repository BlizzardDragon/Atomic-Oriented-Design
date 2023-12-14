using Zenject;

namespace AtomicOrientedDesign.Shooter
{
    public class MetaUpgradeLoader : SaveLoader<MetaUpgradeService, MetaUpgradeData>
    {
        private MetaUpgradeConfig[] _configs;


        [Inject]
        public void Construct(MetaUpgradeConfig[] configs)
        {
            _configs = configs;
        }

        protected override MetaUpgradeData ConvertToData(MetaUpgradeService service)
        {
            var metaUpgrades = service.GetMetaUpgrades();

            var data = new UpgradeData[metaUpgrades.Length];
            for (int i = 0; i < data.Length; i++)
            {
                data[i].Name = metaUpgrades[i].Name;
                data[i].Level = metaUpgrades[i].Level;
            }

            return new MetaUpgradeData { Data = data };
        }

        protected override void SetupByDefault(MetaUpgradeService service)
        {
            var metaUpgrades = new MetaUpgrade[_configs.Length];

            for (int i = 0; i < _configs.Length; i++)
            {
                metaUpgrades[i] = _configs[i].CreateMetaUpgrade();
            }

            service.SetupUpgrades(metaUpgrades);
        }

        protected override void SetupData(MetaUpgradeData metaUpgradeData, MetaUpgradeService service)
        {
            var data = metaUpgradeData.Data;
            var metaUpgrades = new MetaUpgrade[_configs.Length];

            for (int i = 0; i < _configs.Length; i++)
            {
                var metaUpgrade = _configs[i].CreateMetaUpgrade();

                foreach (var item in data)
                {
                    if (item.Name == metaUpgrade.Name)
                    {
                        metaUpgrade.SetLevel(item.Level);
                        continue;
                    }
                }

                metaUpgrades[i] = metaUpgrade;
            }

            service.SetupUpgrades(metaUpgrades);
        }
    }
}