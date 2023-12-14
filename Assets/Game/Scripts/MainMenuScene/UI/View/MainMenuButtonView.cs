using UnityEngine;
using UnityEngine.UI;

namespace AtomicOrientedDesign.Shooter
{
    public class MainMenuButtonView : MonoBehaviour
    {
        [SerializeField] private MainMenuButtonType _type;
        [SerializeField] private Button _button;

        private IMainMenuButtonsPresentationModel _presentationModel;


        public void Construct(IMainMenuButtonsPresentationModel presentationModel)
        {
            _presentationModel = presentationModel;
        }

        private void OnEnable() => _button.onClick.AddListener(OnClick);
        private void OnDisable() => _button.onClick.RemoveListener(OnClick);

        private void OnClick() => _presentationModel.OnClick(_type);
    }
}