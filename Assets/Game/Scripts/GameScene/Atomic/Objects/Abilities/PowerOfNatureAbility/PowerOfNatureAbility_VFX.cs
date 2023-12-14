using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    public sealed class PowerOfNatureAbility_VFX : MonoBehaviour
    {
        [SerializeField] private PowerOfNatureAbilityModel _model;
        [SerializeField] private ParticleSystem _healing;


        private void OnEnable() => _model.HealingEvent.Subscribe(PlayHealingVFX);
        private void OnDisable() => _model.HealingEvent.Unsubscribe(PlayHealingVFX);

        private void PlayHealingVFX(int _) => _healing.Play();
    }
}