using TMPro;
using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    public class PlayerBulletsView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _bullets;


        public void UpdateBulletsView(string currentNumber, string clipSize)
        {
            _bullets.text = $"BULLETS: {currentNumber}/{clipSize}";
        }
    }
}