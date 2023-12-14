using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AtomicOrientedDesign.Shooter
{
    public class LoadingProgressBar : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Image _fill;

        public void SetProgress(float progress)
        {
            _text.text = $"{progress * 100:F0}%";
            _fill.fillAmount = progress;
        }
    }
}