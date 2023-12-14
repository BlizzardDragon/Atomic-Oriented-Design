using System;
using FrameworkUnity.OOP.Zenject.Installers;
using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    public class GameSystemsInstaller : BaseGameSystemsInstaller
    {
        [Header("Instal game")]
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private GameObject _experiencePrefab;
        [SerializeField] private Transform _bulletParent;
        [SerializeField] private Transform _experienceParent;


        protected override void InstallGameSystems()
        {
            BindPlayer();
            BindGameSystems();
            BindControllers();
            BindObservers();
            BindUI();
            BindPresentationModels();
            BindFactories();
        }

        private void BindPlayer()
        {
            Container.BindInterfacesAndSelfTo<PlayerEntity>().FromComponentInNewPrefab(_playerPrefab).AsSingle();
            Container.BindInterfacesAndSelfTo<MoveInput>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerMovementDirectionController>().AsSingle();
        }

        private void BindGameSystems()
        {
            Container.Bind<BulletSpawner>().AsSingle();
            Container.Bind<ExperienceSpawner>().AsSingle();
            Container.Bind<EnemySpawner>().FromComponentInHierarchy().AsSingle();

            Container.BindInterfacesAndSelfTo<AbilitySystem>().AsSingle();
            Container.BindInterfacesTo<MetaUpgradeApplier>().AsSingle();
            Container.Bind<UpgradeSystem>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<ScoreManager>().AsSingle();
            Container.Bind<TimeScaleManager>().AsSingle();
            Container.Bind<DestroyService>().AsSingle();
        }

        private void BindControllers()
        {
            Container.BindInterfacesAndSelfTo<GameOverController>().AsSingle();
            Container.BindInterfacesAndSelfTo<LoseGameController>().AsSingle();
            Container.BindInterfacesAndSelfTo<TimeScaleController>().AsSingle();
        }

        private void BindObservers()
        {
            Container.BindInterfacesAndSelfTo<KillsScoreViewObserver>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerBulletsViewObserver>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerExperienceViewObserver>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerHitPointsViewObserver>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerLevelViewObserver>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameMenuPopupViewObserver>().AsSingle();

            Container.BindInterfacesAndSelfTo<EnemySpawnObserver>().AsSingle();
        }

        private void BindUI()
        {
            Container.Bind<KillsScoreView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<PlayerBulletsView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<PlayerExperienceView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<PlayerHitPointsView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<PlayerLevelView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<GameMenuPopupView>().FromComponentsInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<CardView>().FromComponentsInHierarchy().AsCached();
        }

        private void BindPresentationModels()
        {
            Container.BindInterfacesTo<GameMenuPopupPresentationModel>().AsCached();
        }

        private void BindFactories()
        {
            Container.BindFactory<Vector3, Quaternion, Teams, int, BulletModel, BulletModel.Factory>()
                .FromComponentInNewPrefab(_bulletPrefab)
                .UnderTransform(_bulletParent)
                .AsSingle();

            Container.BindFactory<Vector3, Quaternion, int, ExperienceModel, ExperienceModel.Factory>()
                .FromComponentInNewPrefab(_experiencePrefab)
                .UnderTransform(_experienceParent)
                .AsSingle();
        }
    }
}
