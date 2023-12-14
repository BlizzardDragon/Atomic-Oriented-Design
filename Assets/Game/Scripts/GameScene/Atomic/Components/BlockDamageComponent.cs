using System;
using Atomic;

namespace AtomicOrientedDesign.Shooter
{
    public interface IBlockDamageComponent
    {
        bool DamageIsBlocked { get; }
        event Action<int> BlockDamageRequest;
        event Action PostBlockDamageEvent;
        void SetBlockStatus(bool damageIsBlocked);
    }

    public class BlockDamageComponent : IBlockDamageComponent
    {
        public bool DamageIsBlocked => _damageIsBlocked;

        private AtomicVariable<bool> _damageIsBlocked;

        private AtomicEvent<int> _blockDamageRequest;
        private AtomicEvent _postBlockDamageEvent;

        public event Action PostBlockDamageEvent
        {
            add => _postBlockDamageEvent.Subscribe(value);
            remove => _postBlockDamageEvent.Unsubscribe(value);
        }

        public event Action<int> BlockDamageRequest
        {
            add => _blockDamageRequest.Subscribe(value);
            remove => _blockDamageRequest.Unsubscribe(value);
        }

        public BlockDamageComponent(
            AtomicVariable<bool> damageIsBlocked,
            AtomicEvent<int> blockDamageRequest,
            AtomicEvent postBlockDamageEvent)
        {
            _damageIsBlocked = damageIsBlocked;
            _blockDamageRequest = blockDamageRequest;
            _postBlockDamageEvent = postBlockDamageEvent;
        }

        public void SetBlockStatus(bool damageIsBlocked) => _damageIsBlocked.Value = damageIsBlocked;
    }
}