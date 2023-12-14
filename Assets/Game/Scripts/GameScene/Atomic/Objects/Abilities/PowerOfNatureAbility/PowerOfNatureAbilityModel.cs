using Atomic;
using Declarative;
using Lessons.Gameplay.Atomic2;
using UnityEngine;
using Zenject;

namespace AtomicOrientedDesign.Shooter
{
    public sealed class PowerOfNatureAbilityModel : AbilityModel, IAbility
    {
        // Data:
        [Header("Entity")]
        public AtomicVariable<Entity> TargetEntity;

        [Header("Events")]
        public AtomicEvent<int> HealingEvent;

        [Header("Config")]
        public PowerOfNatureAbilityConfig Config;

        // Logic: 
        private CallbackTimer _callbackTimer = new();
        private RegenerationMechanics _targetedShooting;


        [Inject]
        public void Construct(PlayerEntity playerEntity)
        {
            TargetEntity.Value = playerEntity;
        }

        [Construct]
        private void Construct()
        {
            _callbackTimer.SetCallbackTime(Config.GetLevelData(Level).RegenerationTime);

            _targetedShooting = new RegenerationMechanics(
                TargetEntity, Level, HealingEvent, _callbackTimer, Config);
        }

        public void SetLevel(int level) => Level.Value = level;
        public GameObject GetGameObject() => gameObject;

        protected override void OnLevelUp(int level)
        {
            var levelData = Config.GetLevelData(level);
            var hitPointsComponent = TargetEntity.Value.Get<HitPointsComponent>();

            _callbackTimer.SetCallbackTime(levelData.RegenerationTime);
            hitPointsComponent.SetMaxHitPoints(hitPointsComponent.MaxHitPoints + levelData.ExtraHitPoints);
            hitPointsComponent.HealingRequest(levelData.ExtraHitPoints);
        }
    }
}