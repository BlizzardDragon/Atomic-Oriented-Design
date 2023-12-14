using FrameworkUnity.OOP.Interfaces.Listeners;
using Zenject;

namespace AtomicOrientedDesign.Shooter
{
    public class PlayerFireController : IStartGameListener, ILoseGameListener
    {
        private FireInput _fireInput;
        private PlayerEntity _entity;


        [Inject]
        public void Construct(FireInput fireInput, PlayerEntity playerEntity)
        {
            _fireInput = fireInput;
            _entity = playerEntity;
        }

        public void OnStartGame() => _fireInput.OnFire += Fire;
        public void OnLoseGame() => _fireInput.OnFire -= Fire;

        private void Fire() => _entity.Get<FireRequestComponent>().FireRequest();
    }
}