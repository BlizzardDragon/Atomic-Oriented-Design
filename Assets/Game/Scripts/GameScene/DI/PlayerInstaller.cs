using FrameworkUnity.OOP.Zenject;
using Lessons.Gameplay.Atomic2;
using Zenject;

namespace AtomicOrientedDesign.Shooter
{
    public class PlayerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<PlayerModel>().FromComponentInHierarchy().AsSingle();
            Container.Bind<Entity>().To<PlayerEntity>().FromComponentInHierarchy().AsSingle();
            Container.Bind<EntityTriggerEnter>().FromComponentInHierarchy().AsSingle();
            Container.Bind<RigidbodyTriggerEnter>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<EntityTriggerObserverZenject>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<EntityMagnetTriggerObserverZenject>().AsSingle().NonLazy();
        }

        public override void Start()
        {
            base.Start();
            var context = Container.Resolve<GameManagerContext>();
            context.AddListener(Container.Resolve<EntityTriggerObserverZenject>());
            context.AddListener(Container.Resolve<EntityMagnetTriggerObserverZenject>());
        }
    }
}
