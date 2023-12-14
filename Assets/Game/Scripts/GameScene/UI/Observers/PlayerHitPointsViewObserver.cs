using FrameworkUnity.OOP.Interfaces.Listeners;
using Zenject;

namespace AtomicOrientedDesign.Shooter
{
    public class PlayerHitPointsViewObserver : IInitGameListener, IDeInitGameListener
    {
        private PlayerHitPointsView _hitPointsView;
        private PlayerEntity _entity;
        private HitPointsComponent _hitPointsComponent;


        [Inject]
        public void Construct(PlayerEntity entity , PlayerHitPointsView hitPointsView)
        {
            _entity = entity;
            _hitPointsView = hitPointsView;
        }

        public void OnInitGame()
        {
            _hitPointsComponent = _entity.Get<HitPointsComponent>();
            _hitPointsView.SetHitPoints(_hitPointsComponent.HitPoints.ToString());
            _hitPointsComponent.OnHitPointsChanged += hp => _hitPointsView.SetHitPoints(hp.ToString());
        }

        public void OnDeInitGame()
        {
            _hitPointsComponent.OnHitPointsChanged -= hp => _hitPointsView.SetHitPoints(hp.ToString());
        }
    }
}