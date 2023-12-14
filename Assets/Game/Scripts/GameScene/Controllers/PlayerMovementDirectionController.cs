using FrameworkUnity.OOP.Interfaces.Listeners;
using UnityEngine;
using Zenject;

namespace AtomicOrientedDesign.Shooter
{
    public class PlayerMovementDirectionController : IStartGameListener, ILoseGameListener
    {
        private PlayerEntity _playerEntity;
        private MoveInput _moveInput;
        private MovementDirectionComponent _movementDirection;


        [Inject]
        public void Construct(PlayerEntity playerEntity, MoveInput moveInput)
        {
            _playerEntity = playerEntity;
            _moveInput = moveInput;
        }

        public void OnStartGame()
        {
            _movementDirection = _playerEntity.Get<MovementDirectionComponent>();
            _moveInput.OnMove += OnMove;
        }

        public void OnLoseGame() => _moveInput.OnMove -= OnMove;

        private void OnMove(Vector3 direction) => _movementDirection.SetDirection(direction);
    }
}