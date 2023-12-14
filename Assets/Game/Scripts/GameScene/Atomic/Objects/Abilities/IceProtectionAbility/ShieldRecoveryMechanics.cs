using Atomic;
using Declarative;

namespace AtomicOrientedDesign.Shooter
{
    public sealed class ShieldRecoveryMechanics : IEnable, IDisable
    {
        private readonly AtomicEvent _shieldRecoveryEvent;   
        private readonly AtomicEvent _activateShieldEvent;   
        private readonly AtomicVariable<BlockDamageComponent> _blockDamage;

        public ShieldRecoveryMechanics(
            AtomicEvent shieldRecoveryEvent,
            AtomicEvent activateShieldEvent,
            AtomicVariable<BlockDamageComponent> blockDamage)
        {
            _shieldRecoveryEvent = shieldRecoveryEvent;
            _activateShieldEvent = activateShieldEvent;
            _blockDamage = blockDamage;
        }


        public void OnEnable() => _shieldRecoveryEvent.Subscribe(ActivateShield);
        public void OnDisable() => _shieldRecoveryEvent.Unsubscribe(ActivateShield);

        public void ActivateShield()
        {
            _blockDamage.Value.SetBlockStatus(true);
            _activateShieldEvent?.Invoke();
        }
    }
}