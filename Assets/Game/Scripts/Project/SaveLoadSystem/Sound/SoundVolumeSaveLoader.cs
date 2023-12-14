namespace AtomicOrientedDesign.Shooter
{
    public class SoundVolumeSaveLoader : SaveLoader<SoundVolumeService, SoundVolumeData>
    {
        protected override SoundVolumeData ConvertToData(SoundVolumeService service)
        {
            return new SoundVolumeData { Volume = service.Volume };
        }

        protected override void SetupByDefault(SoundVolumeService service)
        {
            service.SetVolume(Constants.DEFAULT_VOLUME);
        }

        protected override void SetupData(SoundVolumeData data, SoundVolumeService service)
        {
            service.SetVolume(data.Volume);
        }
    }
}