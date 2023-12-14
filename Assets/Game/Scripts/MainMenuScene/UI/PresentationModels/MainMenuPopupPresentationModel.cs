using Zenject;

namespace AtomicOrientedDesign.Shooter
{
    public class MainMenuPopupPresentationModel : IMainMenuButtonsPresentationModel
    {
        private MainMenuPopupManager _popupManager;


        [Inject]
        public void Construct(MainMenuPopupManager popupManager) => _popupManager = popupManager;

        public void OnClick(MainMenuButtonType popupType) => _popupManager.ShowPopup(popupType);
    }
}