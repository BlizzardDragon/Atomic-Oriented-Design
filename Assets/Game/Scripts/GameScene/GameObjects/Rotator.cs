using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private Vector3 _velocity;

    void Update() => transform.Rotate(_velocity * Time.deltaTime);
}
