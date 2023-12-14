using Atomic;
using Declarative;
using Lessons.Gameplay.Atomic2;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    public sealed class PhotonCoreAbilityModel : AbilityModel, IAbility
    {
        // Data:
        [Header("Transform")]
        public AtomicVariable<Transform> CurrentTransform;

        [Header("Entity")]
        public AtomicVariable<Entity> PhotonCore;

        [ReadOnly]
        public AtomicVariable<Entity> PlayerEntity;

        [Header("Serch")]
        [ReadOnly]
        public AtomicVariable<float> SerchRadius;
        public LayerMask TargetLayerMask;

        [Header("Events")]
        public AtomicEvent SearchRequest;
        public AtomicEvent FireRequest;
        public AtomicEvent OnTimeIsOver;

        [Header("Target")]
        [ReadOnly]
        public AtomicVariable<Entity> Target;

        [Header("Config")]
        public PhotonCoreAbilityConfig Config;

        // Logic: 
        private CallbackTimer _callbackTimer;
        private CompanionAttackMechanics _companionAttack;
        private CheckIsAliveEntityMechanics _checkAliveEntity;
        private SearchRandomEntityMechanics _searchRandomEntity;


        [Construct]
        private void Construct()
        {
            SerchRadius.Value = Config.SerchRadius;
            TargetLayerMask.value = Config.TargetLayerMask;

            _callbackTimer = new CallbackTimer(OnTimeIsOver, Config.GetLevelData(Level).ReloadTime);

            _companionAttack = new CompanionAttackMechanics(PhotonCore, Target, FireRequest);

            _checkAliveEntity = new CheckIsAliveEntityMechanics(
                OnTimeIsOver, SearchRequest, FireRequest, Target);

            _searchRandomEntity = new SearchRandomEntityMechanics(
                SearchRequest, Target, SerchRadius, CurrentTransform, TargetLayerMask);
        }

        public void SetLevel(int level) => Level.Value = level;
        public GameObject GetGameObject() => gameObject;

        protected override void OnLevelUp(int level)
        {
            var levelData = Config.GetLevelData(level);

            _callbackTimer.SetCallbackTime(levelData.ReloadTime);
            PhotonCore.Value.Get<DamageComponent>().SetDamage(levelData.Damage);
        }

        private void OnDrawGizmos()
        {
            Vector3 offset = Vector3.up * 0.001f;
            Vector3 drawPos = CurrentTransform.Value ? CurrentTransform.Value.position + offset : offset;
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(drawPos, SerchRadius);
        }
    }
}