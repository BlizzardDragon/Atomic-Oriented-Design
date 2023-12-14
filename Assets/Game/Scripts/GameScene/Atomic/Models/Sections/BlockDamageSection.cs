using System;
using Atomic;
using Declarative;

namespace AtomicOrientedDesign.Shooter
{
    public sealed partial class PlayerModel_Core
    {
        [Serializable]
        public class BlockDamageSection
        {
            public AtomicVariable<bool> DamageIsBlocked = new();
            
            public AtomicAction<int> TakeDamageRequest = new();

            public AtomicEvent<int> BlockDamageRequest = new();
            public AtomicEvent PostBlockDamageEvent = new();


            [Construct]
            public void Construct(LifeSection life)
            {
                TakeDamageRequest.Use(damage =>
                {
                    BlockDamageRequest.Invoke(damage);
                    
                    if (DamageIsBlocked == false)
                    {
                        life.TakeDamageRequest.Invoke(damage);
                    }
                    
                    PostBlockDamageEvent?.Invoke();
                });
            }
        }
    }
}