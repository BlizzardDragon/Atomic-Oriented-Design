using Atomic;
using Declarative;
using Lessons.Gameplay.Atomic2;

namespace AtomicOrientedDesign.Shooter
{
    public sealed class TargetedShootingMechanics : IEnable, IDisable
    {
        private readonly AtomicVariable<Entity> _targetEntity;
        private readonly CallbackTimer _callbackTimer;

        private readonly AtomicVariable<float> _multiplierStep;
        private readonly AtomicVariable<float> _minMultiplier;

        private ShotScatterMultiplierComponent _multiplierComponent;

        private float _currentMultiplier;

        private const int MAX_MULTIPLIER = 1;

        public TargetedShootingMechanics(
            AtomicVariable<Entity> targetEntity,
            CallbackTimer callbackTimer,
            AtomicVariable<float> multiplierStep,
            AtomicVariable<float> minMultiplier)
        {
            _targetEntity = targetEntity;
            _callbackTimer = callbackTimer;
            _multiplierStep = multiplierStep;
            _minMultiplier = minMultiplier;
        }


        public void OnEnable() => _callbackTimer.OnTimeIsOver += ResetMultiplier;

        public void OnDisable()
        {
            _callbackTimer.OnTimeIsOver -= ResetMultiplier;
            _targetEntity.Value.Get<FireEventComponent>().FireEvent -= OnFireEvent;
        }

        public void Construct()
        {
            _targetEntity.Value.Get<FireEventComponent>().FireEvent += OnFireEvent;
            _multiplierComponent = _targetEntity.Value.Get<ShotScatterMultiplierComponent>();
        }

        private void ResetMultiplier() => _multiplierComponent.SetMultiplier(_minMultiplier);

        private void OnFireEvent(BulletArguments arguments)
        {
            _callbackTimer.Reset();

            if (_currentMultiplier == MAX_MULTIPLIER) return;

            _currentMultiplier += _multiplierStep;

            if (_currentMultiplier > MAX_MULTIPLIER)
            {
                _currentMultiplier = MAX_MULTIPLIER;
            }

            _multiplierComponent.SetMultiplier(_currentMultiplier);
        }
    }
}
