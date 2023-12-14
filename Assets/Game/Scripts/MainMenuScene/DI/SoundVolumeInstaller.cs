using Zenject;

namespace AtomicOrientedDesign.Shooter
{
    public class SoundVolumeInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<SoundVolumeView>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<SaveSoundVolumeObserver>().AsSingle();
            Container.BindInterfacesAndSelfTo<SoundVolumeViewPresenter>().AsSingle();
        }
    }
}
