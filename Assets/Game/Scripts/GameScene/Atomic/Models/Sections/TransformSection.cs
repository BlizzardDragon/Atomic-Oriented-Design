using System;
using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    [Serializable]
    public class TransformSection
    {
        [SerializeField] private Transform _transform;
        public Transform Transform => _transform;
        public GameObject GameObject => _transform.gameObject;
    }
}