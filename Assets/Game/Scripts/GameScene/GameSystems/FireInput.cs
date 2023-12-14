using System;
using FrameworkUnity.OOP.Interfaces.Listeners;
using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    public class FireInput : IUpdateGameListener
    {
        private bool _isFire;
        public event Action OnFire;


        public void OnUpdate(float deltaTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _isFire = true;
            }
            else if (Input.GetMouseButton(0))
            {
                _isFire = true;
            }
            else
            {
                _isFire = false;
            }

            if (_isFire)
            {
                OnFire?.Invoke();
            }
        }
    }
}