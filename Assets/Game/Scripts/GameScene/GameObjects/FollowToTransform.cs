using FrameworkUnity.OOP.Interfaces.Listeners;
using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    public class FollowToTransform : MonoBehaviour, IUpdateGameListener
    {
        [SerializeField] private Transform _target;


        public void OnUpdate(float _) => transform.position = _target.position;
        public void SetTarget(Transform target) => _target = target;
    }
}