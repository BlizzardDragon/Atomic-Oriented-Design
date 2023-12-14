using Game.GameEngine.Animation;
using Zenject;

namespace AtomicOrientedDesign.Shooter
{
    public class EnemyInstaller : MonoInstaller
    {
        private EnemyEntity _enemyEntity;


        public override void InstallBindings()
        {
            _enemyEntity = GetComponent<EnemyEntity>();

            Container.BindInterfacesAndSelfTo<EnemyAnimatioMessageObserver>().AsSingle();
            Container.Bind<EnemyEntity>().FromInstance(_enemyEntity).AsSingle();
            Container.Bind<AnimatorObservable>().FromComponentInHierarchy().AsSingle();
        }

        private void Awake() => _enemyEntity.Init();

        public override void Start()
        {
            base.Start();
            Container.Resolve<EnemyAnimatioMessageObserver>().Init();
        }
    }
}