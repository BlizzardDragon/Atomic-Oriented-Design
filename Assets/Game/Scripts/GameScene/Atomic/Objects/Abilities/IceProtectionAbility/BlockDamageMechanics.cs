using Atomic;
using Declarative;

namespace AtomicOrientedDesign.Shooter
{
    public sealed class BlockDamageMechanics : IDisable
    {
        private readonly AtomicEvent _blockDamageRequest;
        private readonly AtomicEvent _onDamageBlocked;
        private readonly AtomicVariable<BlockDamageComponent> _blockDamage;

        public BlockDamageMechanics(
            AtomicEvent blockDamageRequest,
            AtomicEvent onDamageBlocked,
            AtomicVariable<BlockDamageComponent> blockDamage)
        {
            _blockDamageRequest = blockDamageRequest;
            _onDamageBlocked = onDamageBlocked;
            _blockDamage = blockDamage;
        }

        public void Init()
        {
            _blockDamage.Value.BlockDamageRequest += BlockDamageRequest;
            _blockDamage.Value.PostBlockDamageEvent += TryBlockDamage;
        }

        public void OnDisable()
        {
            _blockDamage.Value.BlockDamageRequest -= BlockDamageRequest;
            _blockDamage.Value.PostBlockDamageEvent -= TryBlockDamage;
        }

        private void BlockDamageRequest(int _) => _blockDamageRequest?.Invoke();

        private void TryBlockDamage()
        {
            if (_blockDamage.Value.DamageIsBlocked)
            {
                _blockDamage.Value.SetBlockStatus(false);
                _onDamageBlocked?.Invoke();
            }
        }
    }
}