using System;
using Atomic;
using Declarative;
using Lessons.Gameplay.Atomic2;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AtomicOrientedDesign.Shooter
{
    [Serializable]
    public sealed partial class PlayerModel_Core
    {
        [Section][SerializeField] public TransformSection Transform;
        [Section][SerializeField] public LifeSection Life;
        [Section][SerializeField] public TargetSearchSection TargetSearch;
        [Section][SerializeField] public LevelSection Level;
        [Section][SerializeField] public MagnetSection Magnet;
        [Section][SerializeField] public BlockDamageSection BlockDamage;

        [Section][SerializeField] public ContactSection Contact;
        [Section][SerializeField] public AttackSection Attack;
        [Section][SerializeField] public FireSection Fire;
        [Section][SerializeField] public BulletStorageSection BulletStorage;
        [Section][SerializeField] public MovementSection Movement;
        [Section][SerializeField] public DestroySection Destroy;
        [Section][SerializeField] public CharacterStatesSection CharacterStates;


        [Serializable]
        public class ContactSection
        {
            public AtomicEvent<Entity> OnContact = new();
            public AtomicEvent<int> OnAmmoCollected = new();
            public AtomicEvent<Entity> OnExperienceCollected = new();


            [Construct]
            public void Construct(LevelSection level)
            {
                OnContact.Subscribe(entity =>
                {
                    if (entity.TryGet<AmmoContainerComponent>(out var ammoContainer))
                    {
                        OnAmmoCollected?.Invoke(ammoContainer.NubmerAmmo);
                    }
                    else if (entity.TryGet<ExperienceContainerComponent>(out var experienceComponent))
                    {
                        level.AddExperience?.Invoke(experienceComponent.ExperienceAmount);
                        OnExperienceCollected?.Invoke(entity);
                    }
                });
            }
        }

        [Serializable]
        public class AttackSection
        {
            public AtomicVariable<float> ShotScatterAngle = new();
            public AtomicVariable<float> ThresholdFireAngle = new();
            public AtomicVariable<float> ShotScatterMultiplier = new();
            public AtomicEvent TargetOutAim = new();

            private RotateGunToTargetMechanics _rotateGunToTargetMechanics = new();
            private ShotScatteringAction _shotScatteringAction = new();


            [Construct]
            public void Construct(
                PlayerModel model,
                TransformSection transformSection,
                TargetSearchSection targetSearch,
                MovementSection movement,
                FireSection fire)
            {
                Transform player = transformSection.Transform;

                _rotateGunToTargetMechanics.Construct(
                    model.Gun,
                    player,
                    targetSearch.TargetFound,
                    ThresholdFireAngle,
                    TargetOutAim,
                    fire.FireRequest,
                    movement.RotationEngine,
                    RotationType.RotateTowards);

                _shotScatteringAction.Construct(player, ShotScatterAngle, ShotScatterMultiplier);

                fire.FirePreparation.Subscribe(() => _shotScatteringAction?.Invoke());
            }

            public sealed class ShotScatteringAction : IAtomicAction
            {
                private Transform _transform;
                private AtomicVariable<float> _shotScatteringAngle;
                private AtomicVariable<float> _shotScatterMultiplier;


                public void Construct(
                    Transform transform,
                    AtomicVariable<float> shotScatteringAngle,
                    AtomicVariable<float> shotScatterMultiplier)
                {
                    _transform = transform;
                    _shotScatteringAngle = shotScatteringAngle;
                    _shotScatterMultiplier = shotScatterMultiplier;
                    _shotScatterMultiplier.Value = 1;
                }

                public void Invoke()
                {
                    float randomAngle = Random.Range(-_shotScatteringAngle.Value, _shotScatteringAngle.Value);
                    _transform.Rotate(new Vector3(0, randomAngle * _shotScatterMultiplier, 0));
                }
            }
        }

        [Serializable]
        public class FireSection
        {
            public AtomicVariable<float> ReloadTime = new();
            public AtomicVariable<int> Damage = new();

            public AtomicAction<BulletArguments> FireAction;
            public AtomicEvent FireRequest = new();
            public AtomicEvent AmmoWasEmpty = new();
            public AtomicEvent<BulletArguments> FireEvent = new();
            public AtomicEvent FirePreparation = new();

            private Timer _timer = new();

            private int _oldNumberBullets;


            [Construct]
            public void Construct(PlayerModel model, PlayerModel_Core core, BulletStorageSection bulletStorage)
            {
                PlayerConfig config = model.PlayerConfig;
                Transform gun = model.Gun;
                AtomicVariable<Teams> team = model.Team;
                AtomicVariable<bool> isAlive = core.Life.IsAlive;
                IBulletAmmo bulletAmmo = bulletStorage.BulletAmmo;
                AtomicVariable<int> numberBullets = bulletStorage.BulletAmmo.NumberBullets;
                ReloadTime.Value = config.CharacterConfig.AttackPeriod;
                Damage.Value = config.CharacterConfig.Damage;

                FireRequest.Subscribe(() =>
                {
                    if (!isAlive.Value) return;

                    if (_oldNumberBullets == 0 && numberBullets.Value == 0)
                    {
                        AmmoWasEmpty?.Invoke();
                    }
                    _oldNumberBullets = numberBullets.Value;

                    if (!bulletAmmo.IsNotEmpty) return;
                    if (_timer.Time < ReloadTime.Value) return;

                    FirePreparation?.Invoke();

                    var args = new BulletArguments
                    {
                        Position = gun.position,
                        Rotation = gun.rotation,
                        Team = team.Value,
                        Damage = Damage.Value
                    };

                    FireAction?.Invoke(args);
                    FireEvent?.Invoke(args);
                });

                FireAction.Use(_ =>
                {
                    _timer.Reset();
                    numberBullets.Value--;
                });
            }
        }

        [Serializable]
        public class BulletStorageSection
        {
            public AtomicVariable<float> BulletRestoreTime = new();
            public AtomicEvent OnBulletRestored = new();

            public BulletAmmo BulletAmmo = new();

            private BulletRestoreMechanics _bulletRestoreMechanics = new();


            [Construct]
            public void Construct(PlayerModel model, ContactSection contact, LifeSection life)
            {
                int clipSize = model.PlayerConfig.ClipSize;
                BulletAmmo.ClipSize = clipSize;
                BulletAmmo.NumberBullets.Value = BulletAmmo.ClipSize;
                BulletRestoreTime.Value = model.PlayerConfig.BulletRegenerateTime;
                AtomicEvent<int> onAmmoCollected = contact.OnAmmoCollected;
                AtomicVariable<int> numberBullets = BulletAmmo.NumberBullets;

                _bulletRestoreMechanics.Construct(
                    BulletAmmo,
                    BulletAmmo.NumberBullets,
                    BulletRestoreTime,
                    OnBulletRestored);

                life.OnDeath.Subscribe(() => _bulletRestoreMechanics.IsEnable = false);

                OnBulletRestored.Subscribe(() => BulletAmmo.NumberBullets.Value++);

                onAmmoCollected.Subscribe(value =>
                {
                    int newValue = numberBullets.Value + value;
                    numberBullets.Value = newValue > clipSize ? clipSize : newValue;
                });
            }

            public sealed class BulletRestoreMechanics : IUpdate
            {
                public bool IsEnable = true;

                private BulletAmmo _bulletAmmo;
                private AtomicVariable<int> _numberBullets;
                private AtomicVariable<float> _bulletRestoreTime;
                private AtomicEvent _onBulletRestored;

                private float _time;


                public void Construct(
                    BulletAmmo bulletAmmo,
                    AtomicVariable<int> numberBullets,
                    AtomicVariable<float> bulletRestoreTime,
                    AtomicEvent onBulletRestored)
                {
                    _bulletAmmo = bulletAmmo;
                    _numberBullets = numberBullets;
                    _bulletRestoreTime = bulletRestoreTime;
                    _onBulletRestored = onBulletRestored;
                }

                public void Update(float deltaTime)
                {
                    if (!IsEnable) return;

                    if (_numberBullets.Value < _bulletAmmo.ClipSize)
                    {
                        _time += deltaTime;
                        if (_time >= _bulletRestoreTime.Value)
                        {
                            _time = 0;
                            _onBulletRestored?.Invoke();
                        }
                    }
                }
            }
        }

        [Serializable]
        public class MovementSection
        {
            public AtomicVariable<bool> MovementAllowed = new();
            public AtomicVariable<float> MoveSpeed = new();
            public AtomicVariable<float> RotationSpeedLerp = new();
            public AtomicVariable<float> RotationSpeedTowards = new();

            public MovementDirectionVariable DirectionVariable = new();

            public MoveEngine MoveEngine = new();
            public RotationEngine RotationEngine = new();


            [Construct]
            public void Construct(TransformSection transformSection)
            {
                MovementAllowed.Value = true;
                MoveEngine.Construct(transformSection.Transform, MoveSpeed);
                RotationEngine.Construct(transformSection.Transform, RotationSpeedLerp, RotationSpeedTowards);
            }
        }

        [Serializable]
        public class DestroySection
        {
            public DestroyService DestroyService;


            [Construct]
            public void Construct(ContactSection contact, MagnetSection magnet)
            {
                contact.OnExperienceCollected.Subscribe(entity =>
                {
                    var experienceGameObject = entity.Get<TransformComponent>().EntityTransform.gameObject;
                    magnet.OnTargetDestroyed?.Invoke(experienceGameObject.transform);
                    DestroyService.DestroyGameObject(experienceGameObject);
                });
            }
        }

        [Serializable]
        public class CharacterStatesSection
        {
            public StateMachine<PlayerStateType> StateMachine;

            [Section] public PlayerIdleState IdleState = new();
            [Section] public RunState RunState = new();
            [Section] public ShootState ShootState = new();
            [Section] public DeathState DeathState = new();


            [Construct]
            public void ConstructFSM(PlayerModel model)
            {
                model.onStart += () => StateMachine.Enter();

                StateMachine.Construct(
                    (PlayerStateType.Idle, IdleState),
                    (PlayerStateType.Run, RunState),
                    (PlayerStateType.Shoot, ShootState),
                    (PlayerStateType.Death, null));
            }

            [Construct]
            public void ConstructStates(MovementSection movement)
            {
                RunState.ConstructSubStates(
                    movement.DirectionVariable,
                    movement.MoveEngine,
                    movement.RotationEngine);
            }

            [Construct]
            public void ConstructTransitions(
                MovementSection movement,
                LifeSection life,
                FireSection fire,
                AttackSection attack,
                TargetSearchSection targetSearch,
                BulletStorageSection bulletStorage)
            {
                MovementDirectionVariable directionVariable = movement.DirectionVariable;
                AtomicVariable<bool> movementAllowed = movement.MovementAllowed;
                AtomicEvent<BulletArguments> fireEvent = fire.FireEvent;
                AtomicEvent ammoWasEmpty = fire.AmmoWasEmpty;
                IBulletAmmo bulletAmmo = bulletStorage.BulletAmmo;
                AtomicEvent onDeath = life.OnDeath;
                AtomicEvent targetOutAim = attack.TargetOutAim;
                AtomicEvent targetNotFound = targetSearch.TargetNotFound;

                directionVariable.Subscribe(_ =>
                {
                    if (!movementAllowed.Value) return;

                    if (life.IsAlive.Value)
                    {
                        movement.DirectionVariable.MovementStarted.Subscribe(() => StateMachine.SwitchState(PlayerStateType.Run));
                        movement.DirectionVariable.MovementFinished.Subscribe(() => StateMachine.SwitchState(PlayerStateType.Idle));
                    }
                });

                fireEvent.Subscribe(_ => StateMachine.SwitchState(PlayerStateType.Shoot));
                ammoWasEmpty.Subscribe(() => StateMachine.SwitchState(PlayerStateType.Idle));
                targetOutAim.Subscribe(() => StateMachine.SwitchState(PlayerStateType.Idle));
                targetNotFound.Subscribe(() => StateMachine.SwitchState(PlayerStateType.Idle));
                onDeath.Subscribe(() => StateMachine.SwitchState(PlayerStateType.Death));
            }
        }
    }
}