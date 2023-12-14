using System;
using Lessons.Gameplay.Atomic2;
using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    [Serializable]
    public class EntitySection
    {
        [SerializeField] private Entity _entity;
        public Entity Entity => _entity;
    }
}