using Sirenix.OdinInspector;
using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    public sealed class IceProtectionAbility_VFX : MonoBehaviour
    {
        [SerializeField] private IceProtectionAbilityModel _model;
        [SerializeField] private Transform _iceNovaScale;
        [SerializeField] private ParticleSystem _shield;
        [SerializeField] private ParticleSystem _iceNova;
        private float _explosionRadius;


        private void OnEnable()
        {
            _model.ExplosionRadius.Subscribe(OnRadiusChanged);
            _model.ActivateShieldEvent.Subscribe(ActivateShield);
            _model.OnDamageBlocked.Subscribe(DeactivateShield);
            _model.ExplosionRequest.Subscribe(IceNovaExplosion);
        }

        private void OnDisable()
        {
            _model.ExplosionRadius.Unsubscribe(OnRadiusChanged);
            _model.ActivateShieldEvent.Unsubscribe(ActivateShield);
            _model.OnDamageBlocked.Unsubscribe(DeactivateShield);
            _model.ExplosionRequest.Unsubscribe(IceNovaExplosion);
        }

        [Button]
        private void IceNovaExplosion()
        {
            _iceNovaScale.localScale = Vector3.one * _explosionRadius;
            _iceNova.Play();
        }

        private void OnRadiusChanged(float radius) => _explosionRadius = radius;
        private void ActivateShield() => _shield.Play();
        private void DeactivateShield() => _shield.Stop();
    }
}