using System;
using Declarative;
using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    [Serializable]
    public class FireGrenadeModel_View
    {
        [SerializeField] private GameObject _explosionPrefab;


        [Construct]
        public void Construct(FireGrenadeModel_Core core)
        {
            core.Explosion.OnExplosion.Subscribe(() =>
            {
                Vector3 position = core.TransformSection.Transform.position + Vector3.up * 0.5f;
                GameObject.Instantiate(_explosionPrefab, position, Quaternion.identity);
            });
        }
    }
}