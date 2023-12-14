using UnityEngine;
using UnityEngine.UI;

namespace AtomicOrientedDesign.Shooter
{
    public class ImageToggleView : MonoBehaviour
    {
        public bool IsActive
        {
            set => _image.sprite = value ? _enable : _disable;
        }

        [SerializeField] private Image _image;
        [SerializeField] private Sprite _enable;
        [SerializeField] private Sprite _disable;


        [ContextMenu("Enable")]
        private void Enable() => _image.sprite = _enable;

        [ContextMenu("Disable")]
        private void Disable() => _image.sprite = _disable;
    }
}