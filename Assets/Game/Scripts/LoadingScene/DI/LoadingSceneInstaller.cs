using Zenject;

namespace AtomicOrientedDesign.Shooter
{
    public class LoadingSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ApplicationLoader>().FromComponentInHierarchy().AsSingle();
            Container.Bind<PurchaseManager>().AsSingle();
        }
    }
}