using System;
using TMPro;

namespace AtomicOrientedDesign.Shooter
{
    public class MetaUpgradeViewPresenter : IDisposable
    {
        private readonly TMP_Text _description;
        private readonly MetaUpgradeView _metaUpgradeView;
        private readonly MetaUpgrade _metaUpgrade;
        private readonly MetaUpgradeService _service;

        private const string MAX_LEVEL = "Max level";
        private const string COST = "Cost";
        private const string DESCRIPTION = "Description";

        public MetaUpgradeViewPresenter(
            TMP_Text description,
            MetaUpgradeView metaUpgradeView,
            MetaUpgrade metaUpgrade,
            MetaUpgradeService service)
        {
            _description = description;
            _metaUpgradeView = metaUpgradeView;
            _metaUpgrade = metaUpgrade;
            _service = service;

            _metaUpgradeView.Setup(_metaUpgrade.Name, _metaUpgrade.MaxLevel, _metaUpgrade.Sprite);
            _metaUpgradeView.SetLevel(_metaUpgrade.Level);

            _metaUpgradeView.Button.onClick.AddListener(TryBuyUpgrade);
            _metaUpgradeView.OnPointerEnter += UpdateDescription;
            _metaUpgradeView.OnPointerExit += ClearDescription;
            _metaUpgrade.OnLevelUp += UpdateLevel;
        }


        public void Dispose()
        {
            _metaUpgradeView.Button.onClick.RemoveListener(TryBuyUpgrade);
            _metaUpgradeView.OnPointerEnter -= UpdateDescription;
            _metaUpgradeView.OnPointerExit -= ClearDescription;
            _metaUpgrade.OnLevelUp -= UpdateLevel;
        }

        private void TryBuyUpgrade() => _service.TryBuyUpgrade(_metaUpgrade);

        private void UpdateLevel(int level)
        {
            _metaUpgradeView.SetLevel(level);
            UpdateDescription(null);
        }

        private void UpdateDescription(MetaUpgradeView _)
        {
            if (_metaUpgrade.LevelIsMax)
            {
                _description.text = $"{MAX_LEVEL}!";
            }
            else
            {
                string cost = _metaUpgrade.UpgradeCost.ToString();
                _description.text = $"{COST}: {cost}; {DESCRIPTION}: {_metaUpgrade.Description}.";
            }
        }

        private void ClearDescription(MetaUpgradeView _) => _description.text = "";
    }
}