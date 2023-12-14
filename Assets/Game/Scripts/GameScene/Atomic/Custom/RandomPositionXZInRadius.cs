using Atomic;
using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    public class RandomPositionXZInRadius : IAtomicFunction<Vector3>
    {
        public AtomicVariable<float> MinRadius = new();
        public AtomicVariable<float> MaxRadius = new();

        public Vector3 Invoke()
        {
            var vector2 = Random.insideUnitCircle.normalized * Random.Range(MinRadius, MaxRadius);
            return new Vector3(vector2.x, 0, vector2.y);
        }
    }
}