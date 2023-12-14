using System.Collections.Generic;
using FrameworkUnity.OOP.Interfaces.Listeners;
using UnityEngine;
using Zenject;

namespace AtomicOrientedDesign.Shooter
{
    public class PlayerUpgradePopupPresenter : MonoBehaviour, IStartGameListener, IDeInitGameListener
    {
        [SerializeField] private GameObject _upgradePopup;
        [SerializeField] private List<CardView> _cardViews;

        private UpgradeSystem _upgradeSystem;
        private PlayerEntity _player;

        private Upgrade[] _upgrades;


        [Inject]
        public void Construct(UpgradeSystem upgradeSystem, PlayerEntity player)
        {
            _upgradeSystem = upgradeSystem;
            _player = player;
        }

        public void OnStartGame()
        {
            _player.Get<LevelComponent>().OnLevelChanged += ShowPopup;

            foreach (var cardView in _cardViews)
            {
                cardView.OnClicked += OnButtonClicked;
            }
        }

        public void OnDeInitGame()
        {
            _player.Get<LevelComponent>().OnLevelChanged -= ShowPopup;

            foreach (var cardView in _cardViews)
            {
                cardView.OnClicked -= OnButtonClicked;
            }
        }

        private void OnButtonClicked(CardView view)
        {
            int index = _cardViews.IndexOf(view);
            var upgrade = _upgrades[index];

            upgrade.LevelUp();

            if (upgrade.IsMaxLevel)
            {
                _upgradeSystem.RemoveUpgrade(upgrade);
            }

            _upgrades = null;
            _upgradePopup.SetActive(false);
        }

        private void ShowPopup(int _)
        {
            _upgrades = _upgradeSystem.GetRandomUpgrades(_cardViews.Count);

            for (int i = 0; i < _cardViews.Count; i++)
            {
                _cardViews[i].SetName(_upgrades[i].Name);
                _cardViews[i].SetIcon(_upgrades[i].Config.Icon);
                _cardViews[i].SetLevel((_upgrades[i].Level + 1).ToString());
                _cardViews[i].SetDescription(
                    _upgrades[i].Config.DescriptionTable.GetDescription(_upgrades[i].Level + 1));
            }

            _upgradePopup.SetActive(true);
        }
    }
}
