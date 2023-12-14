using Atomic;
using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    [CreateAssetMenu(fileName = "BulletConfig", menuName = "Configs/BulletConfig", order = 3)]
    public class BulletConfig : ScriptableObject
    {
        [SerializeField] public float Speed;
        [SerializeField] public float LifeTime;
    }
}