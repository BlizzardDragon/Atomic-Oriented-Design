using UnityEngine;
using UnityEngine.UI;

namespace AtomicOrientedDesign.Shooter
{
    [RequireComponent(typeof(Image))]
    public class PlayerExperienceView : MonoBehaviour
    {
        [SerializeField] private Image _image;


        public void SetSprite(Sprite sprite) => _image.sprite = sprite;
        public void SetColor(Color color) => _image.color = color;
        public void SetFillAmount(float value) => _image.fillAmount = value;
    }
}