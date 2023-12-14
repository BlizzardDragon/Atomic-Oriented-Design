using System;
using Lessons.Gameplay.Atomic2;
using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    public class EntityTriggerEnter : MonoBehaviour
    {
        public event Action<Entity> OnEntityTriggerEnter;


        private void OnTriggerEnter(Collider other)
        {
            if (other.attachedRigidbody)
            {
                if (other.attachedRigidbody.TryGetComponent(out Entity entity))
                {
                    OnEntityTriggerEnter?.Invoke(entity);
                }
            }
        }
    }
}