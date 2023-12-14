using FrameworkUnity.OOP.Interfaces.Listeners;
using UnityEngine;
using Zenject;

namespace AtomicOrientedDesign.Shooter
{
    public class PlayerMoveController : IStartGameListener, ILoseGameListener
    {
        private PlayerEntity _entity;
        private MoveInput _moveInput;


        [Inject]
        public void Construct(PlayerEntity playerEntity, MoveInput moveInput)
        {
            _entity = playerEntity;
            _moveInput = moveInput;
        }

        public void OnStartGame() => _moveInput.OnMove += Move;
        public void OnLoseGame() => _moveInput.OnMove -= Move;

        private void Move(Vector3 direction) => _entity.Get<MoveComponent>().Move(direction);
    }
}