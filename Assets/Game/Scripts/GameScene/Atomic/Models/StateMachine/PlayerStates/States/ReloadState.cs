using System;
using Atomic;
using UnityEngine;
using static AtomicOrientedDesign.Shooter.PlayerModel_Core;

namespace AtomicOrientedDesign.Shooter
{
    [Serializable]
    public class ReloadState : UpdateState
    {
        private PlayerStateType _stateType;
        private Animator _animator;
        private IBulletAmmo _bulletAmmo;
        private AtomicVariable<bool> _targetNotFound;
        private const string RELOAD = "Reload";


        public void Construct(
            PlayerStateType stateType,
            Animator animator,
            BulletStorageSection bulletStorage,
            TargetSearchSection targetSearch)
        {
            _stateType = stateType;
            _animator = animator;
            _bulletAmmo = bulletStorage.BulletAmmo;
            _targetNotFound = targetSearch.TargetIsNotFound;
        }

        protected override void OnEnter()
        {
            CheckReload();
        }

        protected override void OnExit() => _animator.SetBool(RELOAD, false);
        protected override void OnUpdate(float deltaTime) => CheckReload();

        private void CheckReload()
        {
            if (_stateType == PlayerStateType.Idle)
            {
                if (!_bulletAmmo.IsNotEmpty || (!_bulletAmmo.IsFull && _targetNotFound.Value))
                {
                    _animator.SetBool(RELOAD, true);
                }
                else
                {
                    _animator.SetBool(RELOAD, false);
                }
            }
            else if (_stateType == PlayerStateType.Run)
            {
                if (!_bulletAmmo.IsFull)
                {
                    _animator.SetBool(RELOAD, true);
                }
                else
                {
                    _animator.SetBool(RELOAD, false);
                }
            }
        }
    }
}