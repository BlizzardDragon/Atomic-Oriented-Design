using System;
using Zenject;

namespace AtomicOrientedDesign.Shooter
{
    public class SaveSoundVolumeObserver : IInitializable, IDisposable
    {
        private SaveLoadManager _saveLoadManager;
        private SoundVolumeView _volumeView;


        [Inject]
        public void Constants(SaveLoadManager saveLoadManager, SoundVolumeView volumeView)
        {
            _saveLoadManager = saveLoadManager;
            _volumeView = volumeView;
        }

        public void Initialize() => _volumeView.OnDeactivate += SaveVolume;
        public void Dispose() => _volumeView.OnDeactivate -= SaveVolume;

        private void SaveVolume() => _saveLoadManager.Save();
    }
}