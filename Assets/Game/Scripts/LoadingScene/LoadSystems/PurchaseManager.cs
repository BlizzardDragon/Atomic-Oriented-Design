using System;

namespace AtomicOrientedDesign.Shooter
{
    public class PurchaseManager
    {
        public void Initialize(Action<bool> callback)
        {
            callback(true);
        }
    }
}