using System;
using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    public class RigidbodyTriggerEnter : MonoBehaviour
    {
        public event Action<Transform> OnColliderEnter;

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.attachedRigidbody)
            {
                OnColliderEnter?.Invoke(collider.attachedRigidbody.transform);
            }
        }
    }
}