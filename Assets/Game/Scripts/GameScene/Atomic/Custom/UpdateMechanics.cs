using System;
using Declarative;

namespace AtomicOrientedDesign.Shooter
{
    public class UpdateMechanics : IUpdate
    {
        private Action<float> _update;

        public void Update(float deltaTime) => _update?.Invoke(deltaTime);
        public void OnUpdate(Action<float> update) => _update = update;
    }
}