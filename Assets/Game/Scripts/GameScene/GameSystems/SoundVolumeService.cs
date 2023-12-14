using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    public class SoundVolumeService
    {
        public float Volume => _volume;
        private float _volume;
        

        public void SetVolume(float volume)
        {
            _volume = volume;
            AudioListener.volume = volume;
        }
    }
}
