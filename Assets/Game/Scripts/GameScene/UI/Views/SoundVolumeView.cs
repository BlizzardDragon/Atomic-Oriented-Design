using System;
using UnityEngine;
using UnityEngine.UI;

namespace AtomicOrientedDesign.Shooter
{
    public class SoundVolumeView : MonoBehaviour
    {
        public Slider Slider => _slider;

        [SerializeField] private Slider _slider;

        public event Action OnActivate;
        public event Action OnDeactivate;


        private void OnEnable() => OnActivate?.Invoke();
        private void OnDisable() => OnDeactivate?.Invoke();
    }
}