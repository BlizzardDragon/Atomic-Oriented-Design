using Declarative;
using System;

namespace AtomicOrientedDesign.Shooter
{
    public class LateUpdateMechanics : ILateUpdate
    {
        private Action<float> _update;

        public void LateUpdate(float deltaTime) => _update?.Invoke(deltaTime);
        public void OnUpdate(Action<float> update) => _update = update;
    }
}