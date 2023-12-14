using System;
using FrameworkUnity.OOP.Interfaces.Listeners;
using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    public class MoveInput : IUpdateGameListener
    {
        public event Action<Vector3> OnMove;

        public void OnUpdate(float deltaTime)
        {
            Vector3 direction = Vector3.zero;

            if (Input.GetKey(KeyCode.A))
            {
                direction += Vector3.left;
            }
            if (Input.GetKey(KeyCode.D))
            {
                direction += Vector3.right;
            }
            if (Input.GetKey(KeyCode.W))
            {
                direction += Vector3.forward;
            }
            if (Input.GetKey(KeyCode.S))
            {
                direction += Vector3.back;
            }

            OnMove?.Invoke(direction.normalized);
        }
    }
}