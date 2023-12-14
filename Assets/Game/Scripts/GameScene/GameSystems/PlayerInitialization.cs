using Cinemachine;
using FrameworkUnity.OOP.Interfaces.Listeners;
using UnityEngine;
using Zenject;

namespace AtomicOrientedDesign.Shooter
{
    public class PlayerInitialization : MonoBehaviour, IStartGameListener
    {
        [SerializeField] private Transform _world;
        [SerializeField] private Transform _parentVFX;
        [SerializeField] private CinemachineVirtualCamera _virtualCamera;
        [SerializeField] private FollowToTransform _followToTransform;
        private PlayerEntity _playerEntity;
        private Transform _playerTransform;


        [Inject]
        public void Construct(PlayerEntity playerEntity)
        {
            _playerEntity = playerEntity;
        }

        public void OnStartGame()
        {
            var playerModel = _playerEntity.Get<DeclarativeModelComponent>().DeclarativeModel as PlayerModel;
            playerModel.SetParentVFX(_parentVFX);

            _playerTransform = _playerEntity.Get<TransformComponent>().EntityTransform;
            _virtualCamera.Follow = _playerTransform;
            _followToTransform.SetTarget(_playerTransform);
            _playerTransform.SetParent(_world);
            _playerTransform.position = _world.position;
        }
    }
}