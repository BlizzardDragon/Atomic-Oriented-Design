using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

namespace AtomicOrientedDesign.Shooter
{
    public class MetaUpgradeListPresenter : MonoBehaviour
    {
        [SerializeField] private TMP_Text _description;
        [SerializeField] private Transform _metaUpgradeViewParent;
        [SerializeField] private MetaUpgradeView _prefab;

        private MetaUpgradeService _service;

        private List<MetaUpgradeViewPresenter> _viewPresenters = new();


        [Inject]
        public void Construct(MetaUpgradeService service) => _service = service;

        private void Start() => Setup();

        public void Setup()
        {
            var metaUpgrades = _service.GetMetaUpgrades();

            foreach (var upgrade in metaUpgrades)
            {
                var metaUpgradeView = Instantiate(_prefab, _metaUpgradeViewParent);
                _viewPresenters.Add(new MetaUpgradeViewPresenter(
                    _description,
                    metaUpgradeView,
                    upgrade,
                    _service));
            }
        }

        private void OnDestroy()
        {
            foreach (var presenter in _viewPresenters)
            {
                presenter.Dispose();
            }
        }
    }
}