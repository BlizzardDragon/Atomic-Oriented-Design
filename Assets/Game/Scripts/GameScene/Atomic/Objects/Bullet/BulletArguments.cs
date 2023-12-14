using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    public struct BulletArguments
    {
        public Vector3 Position;
        public Quaternion Rotation;
        public int Damage;
        public Teams Team;
    }
}