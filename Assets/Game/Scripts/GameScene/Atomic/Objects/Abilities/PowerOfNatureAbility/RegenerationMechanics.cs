using Atomic;
using Declarative;
using Lessons.Gameplay.Atomic2;

namespace AtomicOrientedDesign.Shooter
{
    public sealed class RegenerationMechanics : IEnable, IDisable
    {
        private readonly AtomicVariable<Entity> _targetEntity;
        private readonly AtomicVariable<int> _level;

        private readonly AtomicEvent<int> _healingEvent;

        private readonly CallbackTimer _callbackTimer;

        private readonly PowerOfNatureAbilityConfig _config;

        public RegenerationMechanics(
            AtomicVariable<Entity> targetEntity,
            AtomicVariable<int> level,
            AtomicEvent<int> healingEvent,
            CallbackTimer callbackTimer,
            PowerOfNatureAbilityConfig config)
        {
            _targetEntity = targetEntity;
            _level = level;
            _healingEvent = healingEvent;
            _callbackTimer = callbackTimer;
            _config = config;
        }


        public void OnEnable() => _callbackTimer.OnTimeIsOver += Regeneration;
        public void OnDisable() => _callbackTimer.OnTimeIsOver -= Regeneration;

        private void Regeneration()
        {
            _callbackTimer.Reset();
            var regenerationValue = _config.GetLevelData(_level).RegenerationValue;
            _targetEntity.Value.Get<HitPointsComponent>().HealingRequest(regenerationValue);
            _healingEvent?.Invoke(regenerationValue);
        }
    }
}