using UnityEngine;
using Zenject;

namespace AtomicOrientedDesign.Shooter
{
    public class MainMenuInstaller : MonoInstaller
    {
        [SerializeField] private MainMenuButtonView[] _popupButtonViews;
        [SerializeField] private MainMenuButtonView[] _loadButtonViews;


        public override void InstallBindings()
        {
            Container.Bind<MainMenuPopupManager>().AsSingle();
            Container.Bind<MainMenuPopupView>().FromComponentsInHierarchy().AsCached();

            Container.Bind<MetaUpgradeListPresenter>().FromComponentInHierarchy().AsSingle();

            Container.Bind<MainMenuPopupPresentationModel>().AsTransient();
            Container.Bind<MainMenuLoadPresentationModel>().AsTransient();
        }

        private void Awake()
        {
            foreach (var button in _popupButtonViews)
            {
                button.Construct(Container.Resolve<MainMenuPopupPresentationModel>());
            }

            foreach (var button in _loadButtonViews)
            {
                button.Construct(Container.Resolve<MainMenuLoadPresentationModel>());
            }
        }
    }
}
