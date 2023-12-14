using UnityEngine;
using Zenject;

namespace AtomicOrientedDesign.Shooter
{
    public class ProjectInstaller : MonoInstaller<ProjectInstaller>
    {
        [SerializeField] MetaUpgradeConfig[] _metaUpgradeConfigs;


        public override void InstallBindings()
        {
            Debug.Log("Install Project Bindings");

            BindSaveLoadSystem();
            
            Container.Bind<SceneLoader>().AsSingle();
            Container.Bind<MoneyStorage>().AsSingle();

            foreach (var config in _metaUpgradeConfigs)
            {
                Container.Bind<MetaUpgradeConfig>().FromInstance(config).AsCached();
            }
        }

        private void BindSaveLoadSystem()
        {
            Container.BindInterfacesAndSelfTo<GameRepository>().AsCached();
            Container.BindInterfacesAndSelfTo<SaveLoadManager>().AsSingle();

            Container.BindInterfacesAndSelfTo<SoundVolumeSaveLoader>().AsSingle();
            Container.Bind<SoundVolumeService>().AsSingle();

            Container.BindInterfacesAndSelfTo<MetaUpgradeLoader>().AsSingle();
            Container.Bind<MetaUpgradeService>().AsSingle();
        }
    }
}
