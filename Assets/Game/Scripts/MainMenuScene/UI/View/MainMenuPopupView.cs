using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    public class MainMenuPopupView : MonoBehaviour
    {
        [SerializeField] private MainMenuButtonType _type;

        public MainMenuButtonType Type => _type;


        public void Activate() => gameObject.SetActive(true);
        public void Deactivate() => gameObject.SetActive(false);
    }
}