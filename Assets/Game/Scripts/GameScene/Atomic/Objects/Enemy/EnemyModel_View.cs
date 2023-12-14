using System;
using Declarative;
using Game.GameEngine.Animation;
using UnityEngine;
using static AtomicOrientedDesign.Shooter.EnemyModel_Core;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace AtomicOrientedDesign.Shooter
{
    [Serializable]
    public class EnemyModel_View : IEnable, IDisable
    {
        [Section] public Animator Animator;
        [Section][SerializeField] AnimatorObservable _eventDispatcher;
        public AnimatorMachine AnimatorMachine;

        [HideInInspector] public Transform ParentVFX;

        [SerializeField] private AudioSource _deathAS;
        [SerializeField] private AudioSource _takeDamageAS;
        [SerializeField] private AudioClip _takeDamageSFX;
        [SerializeField] private AudioClip _deathSFX;

        [SerializeField] private ParticleSystem _deathBloodPoolVFX;
        [SerializeField] private ParticleSystem _deathExplosionVFX;
        [SerializeField] private ParticleSystem _takeDamage;
        [SerializeField] private ParticleSystem _bloodDripping;
        [SerializeField] private ParticleSystem[] _bloodStreams;

        private static readonly int TakeDamage = Animator.StringToHash(AMIMATION_TAKE_DAMAGE);
        private const string AMIMATION_TAKE_DAMAGE = "Body.TakeDamage";
        private const string MESSAGE_DEATH = "Death";
        private const int VOLUME_DEATH = 5;

        [Section] public EnemyIdleViewState IdleViewState = new();
        [Section] public EnemyRunViewState RunViewState = new();


        [Construct]
        public void Construct()
        {
            AnimatorMachine.Construct(Animator, _eventDispatcher);
        }

        [Construct]
        public void ConstructStates()
        {
            AnimatorMachine.AddState((int)AnimatorStateType.Idle, IdleViewState);
            AnimatorMachine.AddState((int)AnimatorStateType.Run, RunViewState);

            AnimatorMachine.AddState(
                (int)AnimatorStateType.Death,
                new DelegateState(() => AnimatorMachine.ApplyRootMotion(), () => AnimatorMachine.ResetRootMotion()));
        }

        [Construct]
        public void ConstructTransitions(EnemyStatesSection enemyStates)
        {
            var coreFSM = enemyStates.StateMachine;

            AnimatorMachine.SetTransitions(new AnimatorMachine.StateTransition[]
            {
                new((int)AnimatorStateType.Idle, () => coreFSM.CurrentState == EnemyStateType.Idle),
                new((int)AnimatorStateType.Run, () => coreFSM.CurrentState == EnemyStateType.Run),
                new((int)AnimatorStateType.MeleeAttack, () => coreFSM.CurrentState == EnemyStateType.MeleeAttack),
                new((int)AnimatorStateType.Idle, () => coreFSM.CurrentState == EnemyStateType.PostAttack),
                new((int)AnimatorStateType.Run, () => coreFSM.CurrentState == EnemyStateType.Scatter),
                new((int)AnimatorStateType.Death, () => coreFSM.CurrentState == EnemyStateType.Death),
            });
        }

        [Construct]
        public void ConstructFX()
        {
            _eventDispatcher.OnStringReceived += message =>
            {
                if (message == MESSAGE_DEATH)
                {
                    _deathBloodPoolVFX.Play();

                    foreach (var particle in _bloodStreams)
                    {
                        particle.Stop();
                    }

                    _bloodDripping.Stop();
                }
            };
        }

        public void OnEnable()
        {
            AnimatorMachine.OnStateEntered += PlayFX;
        }

        public void OnDisable()
        {
            AnimatorMachine.OnStateEntered -= PlayFX;
        }

        private void PlayFX(AnimatorStateInfo state, int stateId, int layerIndex)
        {
            if (stateId == (int)AnimatorStateType.Death)
            {
                _deathExplosionVFX.Play();
                _deathAS.pitch = Random.Range(0.9f, 1.1f);
                _deathAS.PlayOneShot(_deathSFX, VOLUME_DEATH);
            }
            else if (state.fullPathHash == TakeDamage)
            {
                _takeDamageAS.pitch = Random.Range(0.9f, 1.1f);
                _takeDamageAS.PlayOneShot(_takeDamageSFX);
                _takeDamage.Play();

                if (!_bloodDripping.isPlaying)
                {
                    _bloodDripping.Play();
                }

                int randomIndex = Random.Range(0, _bloodStreams.Length);
                _bloodStreams[randomIndex].Play();
            }
        }

        private void Spawn(GameObject prefab, Transform spawnPosition)
        {
            Object.Instantiate(prefab, spawnPosition.position, spawnPosition.rotation, ParentVFX);
        }
    }
}