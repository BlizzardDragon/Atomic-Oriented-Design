using FrameworkUnity.OOP.Interfaces.Listeners;
using Zenject;

namespace AtomicOrientedDesign.Shooter
{
    public class MetaUpgradeApplier : IStartGameListener
    {
        private MetaUpgradeService _service;
        private SceneContext _sceneContext;

        [Inject]
        public void Construct(MetaUpgradeService service, SceneContext sceneContext)
        {
            _sceneContext = sceneContext;
            _service = service;
        }

        public void OnStartGame() => _service.ApplyMetaUpgrades(_sceneContext);
    }
}