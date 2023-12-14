using System;
using Atomic;
using Declarative;
using Game.GameEngine.Animation;
using UnityEngine;
using static AtomicOrientedDesign.Shooter.PlayerModel_Core;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace AtomicOrientedDesign.Shooter
{
    [Serializable]
    public class PlayerModel_View : IEnable, IDisable
    {
        [Section][SerializeField] private Animator _animator;
        [SerializeField] AnimatorObservable _eventDispatcher;
        public AnimatorMachine AnimatorMachine;

        [SerializeField] private Transform _takeDamage;
        [SerializeField] private Transform _shellEjection;
        [SerializeField] private Transform _leftFoot;
        [SerializeField] private Transform _rightFoot;
        [HideInInspector] public Transform ParentVFX;

        [SerializeField] private ParticleSystem _deathVFX;
        [SerializeField] private ParticleSystem _fellVFX;
        [SerializeField] private GameObject _shotVFXPrefab;
        [SerializeField] private GameObject _takeDamageVFXPrefab;
        [SerializeField] private GameObject _stepVFXPrefab;
        [SerializeField] private GameObject _shellEjectionVFXPrefab;

        [SerializeField] private AudioSource _mainAS;
        [SerializeField] private AudioSource _shotAS;
        [SerializeField] private AudioSource _stepAS;
        [SerializeField] private AudioClip _shotSFX;
        [SerializeField] private AudioClip _takeDamageSFX;
        [SerializeField] private AudioClip _deathSFX;
        [SerializeField] private AudioClip _fellSFX;
        [SerializeField] private AudioClip _stepSFX;
        [SerializeField] private AudioClip _reloadSFX;
        [SerializeField] private AudioClip _ammoSFX;

        private static readonly int TakeDamage = Animator.StringToHash(AMIMATION_TAKE_DAMAGE);
        private const string AMIMATION_TAKE_DAMAGE = "Body.TakeDamage";
        private const string MESSAGE_LEFT_FOOT = "LeftFoot";
        private const string MESSAGE_RIGHT_FOOT = "RightFoot";
        private const string MESSAGE_FELL = "Fell";
        private const string MESSAGE_RELOAD = "Reload";
        private const int VOLUME_STEP = 4;
        private const int VOLUME_FELL = 5;
        private const int VOLUME_RELOAD = 5;
        private const int VOLUME_DEATH = 10;
        private const int VOLUME_AMMO = 2;
        [Section] public PlayerIdleViewState IdleViewState = new();
        [Section] public PlayerRunViewState RunViewState = new();


        [Construct]
        public void Construct()
        {
            AnimatorMachine.Construct(_animator, _eventDispatcher);
        }

        [Construct]
        public void ConstructStates()
        {
            AnimatorMachine.AddState((int)AnimatorStateType.Idle, IdleViewState);
            AnimatorMachine.AddState((int)AnimatorStateType.Run, RunViewState);
        }

        [Construct]
        public void ConstructTransitions(CharacterStatesSection characterStates)
        {
            var coreFSM = characterStates.StateMachine;

            AnimatorMachine.SetTransitions(new AnimatorMachine.StateTransition[]
            {
                new((int)AnimatorStateType.Death, () => coreFSM.CurrentState == PlayerStateType.Death),
                new((int)AnimatorStateType.Run, () => coreFSM.CurrentState == PlayerStateType.Run),
                new((int)AnimatorStateType.Idle, () => coreFSM.CurrentState == PlayerStateType.Idle),
                new((int)AnimatorStateType.Shoot, () => coreFSM.CurrentState == PlayerStateType.Shoot),
            });
        }

        [Construct]
        public void ConstructFX(PlayerModel model, FireSection fire, ContactSection contact)
        {
            Transform gun = model.Gun;
            AtomicEvent<BulletArguments> fireEvent = fire.FireEvent;
            AtomicEvent<int> onAmmoCollected = contact.OnAmmoCollected;

            fireEvent.Subscribe(_ =>
            {
                _shotAS.pitch = Random.Range(0.9f, 1.1f);
                _shotAS.PlayOneShot(_shotSFX);
                Spawn(_shotVFXPrefab, gun);
                Spawn(_shellEjectionVFXPrefab, _shellEjection);
            });

            onAmmoCollected.Subscribe(_ => _mainAS.PlayOneShot(_ammoSFX, VOLUME_AMMO));

            _eventDispatcher.OnStringReceived += message =>
            {
                if (message == MESSAGE_LEFT_FOOT)
                {
                    _stepAS.pitch = Random.Range(0.9f, 1.1f);
                    _stepAS.PlayOneShot(_stepSFX, VOLUME_STEP);
                    Spawn(_stepVFXPrefab, _leftFoot);
                }
                else if (message == MESSAGE_RIGHT_FOOT)
                {
                    _stepAS.pitch = Random.Range(0.9f, 1.1f);
                    _stepAS.PlayOneShot(_stepSFX, VOLUME_STEP);
                    Spawn(_stepVFXPrefab, _rightFoot);
                }
                else if (message == MESSAGE_FELL)
                {
                    _fellVFX.Play();
                    _deathVFX.Play();
                    _mainAS.PlayOneShot(_fellSFX, VOLUME_FELL);
                }
                else if (message == MESSAGE_RELOAD)
                {
                    _mainAS.PlayOneShot(_reloadSFX, VOLUME_RELOAD);
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
                _mainAS.PlayOneShot(_deathSFX, VOLUME_DEATH);
            }
            else if (state.fullPathHash == TakeDamage)
            {
                _mainAS.PlayOneShot(_takeDamageSFX);
                Spawn(_takeDamageVFXPrefab, _takeDamage);
            }
        }

        private void Spawn(GameObject prefab, Transform spawnPosition)
        {
            Object.Instantiate(prefab, spawnPosition.position, spawnPosition.rotation, ParentVFX);
        }
    }
}