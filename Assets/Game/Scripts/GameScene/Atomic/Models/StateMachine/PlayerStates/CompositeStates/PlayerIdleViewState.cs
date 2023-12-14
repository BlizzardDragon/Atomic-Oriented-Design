using System;
using Declarative;
using UnityEngine;
using static AtomicOrientedDesign.Shooter.PlayerModel_Core;

namespace AtomicOrientedDesign.Shooter
{
    [Serializable]
    public class PlayerIdleViewState : CompositeState
    {
        public ReloadState ReloadState = new();
        public TakeDamageState TakeDamageState = new();


        [Construct]
        public void ConstructSelf()
        {
            SetStates(ReloadState, TakeDamageState);
        }

        [Construct]
        public void ConstructSubStates(
            Animator animator,
            BulletStorageSection bulletStorage,
            TargetSearchSection targetSearch,
            LifeSection life)
        {
            ReloadState.Construct(PlayerStateType.Idle, animator, bulletStorage, targetSearch);
            TakeDamageState.Construct(animator, life.TakeDamageEvent);
        }
    }
}