using Zenject;

namespace AtomicOrientedDesign.Shooter
{
    public class GameMenuPopupPresentationModel : IActivateDeactivatePresentationModel
    {
        private TimeScaleManager _timeScaleManager;


        [Inject]
        public void Construct(TimeScaleManager timeScaleManager) => _timeScaleManager = timeScaleManager;

        public void OnActivate()
        {
            _timeScaleManager.StopTime(nameof(GameMenuPopupPresentationModel));
        }

        public void OnDeactivate()
        {
            _timeScaleManager.TryPlayTime(nameof(GameMenuPopupPresentationModel));
        }
    }
}
