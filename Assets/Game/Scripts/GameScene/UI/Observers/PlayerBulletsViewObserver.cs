using FrameworkUnity.OOP.Interfaces.Listeners;
using Zenject;

namespace AtomicOrientedDesign.Shooter
{
    public class PlayerBulletsViewObserver : IInitGameListener, IDeInitGameListener
    {
        private PlayerBulletsView _view;
        private PlayerEntity _entity;
        private BulletAmmoComponent _bulletAmmoComponent;


        [Inject]
        public void Construct(PlayerEntity entity, PlayerBulletsView view)
        {
            _view = view;
            _entity = entity;
        }

        public void OnInitGame()
        {
            _bulletAmmoComponent = _entity.Get<BulletAmmoComponent>();

            _view.UpdateBulletsView(
                _bulletAmmoComponent.NumberBullets.ToString(), _bulletAmmoComponent.ClipSize.ToString());
            
            _bulletAmmoComponent.OnNumberBulletsChanged += UpdateView;
        }

        public void OnDeInitGame() => _bulletAmmoComponent.OnNumberBulletsChanged -= UpdateView;

        private void UpdateView(int NumberBullets)
        {
            _view.UpdateBulletsView(NumberBullets.ToString(), _bulletAmmoComponent.ClipSize.ToString());
        }
    }
}