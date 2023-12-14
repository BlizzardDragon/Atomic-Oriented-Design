using System;
using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    [Serializable]
    public struct CharacterConfig
    {
        [SerializeField] public int Damage;
        [SerializeField] public int HitPoints;
        [SerializeField] public int MaxHitPoints;
        [SerializeField] public float MoveSpeed;
        [SerializeField] public float RotationSpeedLerp;
        [SerializeField] public float RotationSpeedTowards;
        [SerializeField] public float AttackPeriod;
        [SerializeField] public Teams Team;
    }
}