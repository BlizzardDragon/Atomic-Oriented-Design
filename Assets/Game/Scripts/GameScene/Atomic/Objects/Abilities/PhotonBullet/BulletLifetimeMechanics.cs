using Atomic;
using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    public sealed class BulletLifetimeMechanics
    {
        private readonly AtomicVariable<float> _lifetime;
        private readonly GameObject _bullet;

        public BulletLifetimeMechanics(AtomicVariable<float> lifetime, GameObject bullet)
        {
            _lifetime = lifetime;
            _bullet = bullet;
        }

        public void Update(float deltaTime)
        {
            _lifetime.Value -= deltaTime;
            
            if (_lifetime.Value <= 0)
            {
                GameObject.Destroy(_bullet);
            }
        }
    }
}