using Atomic;
using Declarative;
using Lessons.Gameplay.Atomic2;
using UnityEngine;
using Zenject;

namespace AtomicOrientedDesign.Shooter
{
    public sealed class TargetedShootingAbilityModel : AbilityModel, IAbility
    {
        // Data:
        [Header("Entity")]
        public AtomicVariable<Entity> TargetEntity;

        [Header("Data")]
        public AtomicVariable<float> MultiplierStep;
        public AtomicVariable<float> MinMultiplier;

        [Header("Config")]
        public TargetedShootingAbilityConfig Config;

        // Logic: 
        private CallbackTimer _callbackTimer = new();
        private TargetedShootingMechanics _targetedShooting;


        [Inject]
        public void Construct(PlayerEntity playerEntity)
        {
            TargetEntity.Value = playerEntity;
            _targetedShooting.Construct();
        }

        [Construct]
        private void Construct()
        {
            _callbackTimer.SetCallbackTime(Config.GetLevelData(Level).AimingTime);

            _targetedShooting = new TargetedShootingMechanics(
                TargetEntity, _callbackTimer, MultiplierStep, MinMultiplier);
        }
        
        public void SetLevel(int level) => Level.Value = level;
        public GameObject GetGameObject() => gameObject;

        protected override void OnLevelUp(int level)
        {
            var levelData = Config.GetLevelData(level);
            _callbackTimer.SetCallbackTime(levelData.AimingTime);
            MinMultiplier.Value = levelData.MinMultiplier;
            MultiplierStep.Value = levelData.MultiplierStep;
        }

    }
}