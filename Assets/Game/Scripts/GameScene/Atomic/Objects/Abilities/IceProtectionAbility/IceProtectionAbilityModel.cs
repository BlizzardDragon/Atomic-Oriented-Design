using Atomic;
using Declarative;
using Lessons.Gameplay.Atomic2;
using UnityEngine;
using Zenject;

namespace AtomicOrientedDesign.Shooter
{
    public sealed class IceProtectionAbilityModel : PeriodicAbilityModel, IAbility
    {
        // Data:
        [Header("Target")]
        public AtomicVariable<Entity> TargetEntity;
        public AtomicVariable<BlockDamageComponent> BlockDamageComponent;
        public AtomicVariable<Transform> TargetTransform;

        [Header("Explosion")]
        public AtomicVariable<float> ExplosionRadius;
        public AtomicVariable<int> Damage;
        public AtomicEvent ExplosionRequest;
        public AtomicEvent<Entity[]> AffectedEntitiesEvent;

        [Header("Freeze")]
        public AtomicVariable<float> FreezeTime;
        public AtomicVariable<float> DelayBeforeFreezing;
        public AtomicVariable<float> MinSpeed;

        [Header("Shield")]
        public AtomicEvent ActivateShieldEvent;
        public AtomicEvent OnDamageBlocked;

        [Header("Config")]
        public IceProtectionAbilityConfig Config;

        // Logic: 
        private BlockDamageMechanics _blockDamage;
        private ShieldRecoveryMechanics _shieldRecovery;
        private ExplosionMechanics _explosion;
        private FreezingMechanics _freezing;


        [Inject]
        public void Construct(PlayerEntity playerEntity)
        {
            TargetEntity.Value = playerEntity;

            TargetTransform.Value = playerEntity.Get<TransformComponent>().EntityTransform;
            BlockDamageComponent.Value = playerEntity.Get<BlockDamageComponent>();
            
            _blockDamage.Init();
            _shieldRecovery.ActivateShield();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            ActivateShieldEvent.Subscribe(StopTimer.Invoke);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            ActivateShieldEvent.Unsubscribe(StopTimer.Invoke);
        }

        [Construct]
        protected override void Construct()
        {
            base.Construct();
            FreezeTime.Value = Config.FreezeTime;
            DelayBeforeFreezing.Value = Config.DelayBeforeFreezing;
            MinSpeed.Value = Config.MinSpeed;
            OnLevelUp(Level);

            _callbackTimer.SetCallbackTime(Config.GetLevelData(Level).ShieldReloadTime);

            _blockDamage = new BlockDamageMechanics(ExplosionRequest, OnDamageBlocked, BlockDamageComponent);
            
            _shieldRecovery = new ShieldRecoveryMechanics(
                OnTimeIsOver, ActivateShieldEvent, BlockDamageComponent);

            _explosion = new ExplosionMechanics(
                ExplosionRequest, AffectedEntitiesEvent, ExplosionRadius,
                Damage, TargetTransform, Config.TargetLayerMask);

            _freezing = new FreezingMechanics(
                FreezeTime, DelayBeforeFreezing, MinSpeed, AffectedEntitiesEvent);
        }

        public void SetLevel(int level) => Level.Value = level;
        public GameObject GetGameObject() => gameObject;

        protected override void OnLevelUp(int level)
        {
            var levelData = Config.GetLevelData(level);

            ExplosionRadius.Value = levelData.ExplosionRadius;
            Damage.Value = levelData.Damage;
        }

        private void OnDrawGizmos()
        {
            Vector3 offset = Vector3.up * 0.001f;
            Vector3 drawPos = TargetTransform.Value ? TargetTransform.Value.position + offset : offset;
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(drawPos, ExplosionRadius);
        }
    }
}