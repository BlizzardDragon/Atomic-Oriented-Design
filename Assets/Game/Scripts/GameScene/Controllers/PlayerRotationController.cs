using FrameworkUnity.OOP.Interfaces.Listeners;
using UnityEngine;
using Zenject;

namespace AtomicOrientedDesign.Shooter
{
    public class PlayerRotationController : IStartGameListener, ILoseGameListener
    {
        private PlayerEntity _entity;
        private MoveInput _moveInput;
        private RotationComponent _rotationComponent;


        [Inject]
        public void Construct(PlayerEntity playerEntity, MoveInput moveInput)
        {
            _entity = playerEntity;
            _moveInput = moveInput;
        }

        public void OnStartGame()
        {
            _rotationComponent = _entity.Get<RotationComponent>();
            _moveInput.OnMove += Rotate;
        }

        public void OnLoseGame() => _moveInput.OnMove -= Rotate;

        public void Rotate(Vector3 direction) => _rotationComponent.RotationRequest(direction);
    }
}