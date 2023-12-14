using System;
using Zenject;

namespace AtomicOrientedDesign.Shooter
{
    public class SoundVolumeViewPresenter : IInitializable, IDisposable
    {
        private SoundVolumeService _service;
        private SoundVolumeView _volumeView;


        [Inject]
        public void Constants(SoundVolumeService service, SoundVolumeView volumeView)
        {
            _service = service;
            _volumeView = volumeView;
        }

        public void Initialize()
        {
            SetSliderValue();
            _volumeView.Slider.onValueChanged.AddListener(UpdateVolume);
        }

        public void Dispose()
        {
            _volumeView.Slider.onValueChanged.RemoveListener(UpdateVolume);
        }

        private void SetSliderValue() => _volumeView.Slider.value = _service.Volume;
        private void UpdateVolume(float volume) => _service.SetVolume(volume);
    }
}