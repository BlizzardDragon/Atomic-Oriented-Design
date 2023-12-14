using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace AtomicOrientedDesign.Shooter
{
    public class GameMenuPopupView : MonoBehaviour
    {
        [SerializeField] private Button _continue;
        [SerializeField] private Button _restart;
        [SerializeField] private Button _mainMenu;
        [SerializeField] private Button _exit;

        public Button Continue => _continue;
        public Button Restart => _restart;
        public Button MainMenu => _mainMenu;
        public Button Exit => _exit;

        private IActivateDeactivatePresentationModel _presentationModel;


        [Inject]
        public void Construct(IActivateDeactivatePresentationModel presentationModel)
        {
            _presentationModel = presentationModel;
        }

        private void OnEnable() => _presentationModel.OnActivate();
        private void OnDisable() => _presentationModel.OnDeactivate();
    }
}