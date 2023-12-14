using Zenject;

namespace AtomicOrientedDesign.Shooter
{
    public class MainMenuPopupManager
    {
        private MainMenuButtonType _currentPopup;
        private MainMenuPopupView[] _popups;


        [Inject]
        public void Construct(MainMenuPopupView[] popups) => _popups = popups;

        public void ShowPopup(MainMenuButtonType type)
        {
            if (_currentPopup == type)
            {
                HidePopups();
                return;
            }

            _currentPopup = type;

            foreach (var popup in _popups)
            {
                if (popup.Type == type)
                {
                    popup.Activate();
                }
                else
                {
                    popup.Deactivate();
                }
            }
        }

        public void HidePopups()
        {
            foreach (var popup in _popups)
            {
                popup.Deactivate();
            }

            _currentPopup = MainMenuButtonType.Non;
        }
    }
}
