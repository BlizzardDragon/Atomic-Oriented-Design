using System;
using Atomic;
using Declarative;
using Game.GameEngine.Animation;
using Lessons.Gameplay.Atomic2;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    [Serializable]
    public partial class EnemyModel_Core : IDisable
    {
        [Section][SerializeField] public TransformSection Transform;
        [Section][SerializeField] public LifeSection Life;

        [Section][SerializeField] public DroppedExperienceSection DroppedExperience;
        [Section][SerializeField] public AttackSection Attack;
        [Section][SerializeField] public TargetSection Target;
        [Section][SerializeField] public MovementSection Movement;
        [Section][SerializeField] public TriggerSection Trigger;
        [Section][SerializeField] public AnimatorSection Animator;
        [Section][SerializeField] public EnemyStatesSection EnemyStates;


        public void OnDisable() => Animator.Unsubscribe?.Invoke();

        [Serializable]
        public class DroppedExperienceSection
        {
            public AtomicVariable<int> ExperienceAmount = new();
            public AtomicAction<ExperienceArguments> SpawnExperience;


            [Construct]
            public void Construct(TransformSection transform, LifeSection life)
            {
                life.OnDeath.Subscribe(() =>
                {
                    var args = new ExperienceArguments
                    {
                        Position = transform.Transform.position,
                        Rotation = transform.Transform.rotation,
                        Amount = ExperienceAmount.Value
                    };

                    SpawnExperience.Invoke(args);
                });
            }
        }

        [Serializable]
        public class AttackSection
        {
            public AtomicVariable<float> AttackPeriod = new();
            public AtomicVariable<int> Damage = new();

            public AtomicEvent OnAttacked = new();
            public AtomicEvent AttackNotReady = new();
            public AtomicEvent OnDamageDealt = new();

            private TargetAchievementObserver _targetAchievementObserver = new();
            private DealDamageObserver _dealDamageObserver = new();
            private Timer _timer = new();


            [Construct]
            public void Construct(EnemyModel model, TargetSection target, AnimatorSection message)
            {
                AttackPeriod.Value = model.Config.CharacterConfig.AttackPeriod;
                Damage.Value = model.Config.CharacterConfig.Damage;

                _targetAchievementObserver.Construct(
                    AttackNotReady,
                    target.OnTargetAchieved,
                    OnAttacked,
                    AttackPeriod,
                    _timer);

                _dealDamageObserver.Construct(message.DealDamage, OnDamageDealt, Damage, target.TargetEnetity);
            }

            public class TargetAchievementObserver
            {
                public void Construct(
                    AtomicEvent attackNotReady,
                    AtomicEvent<Entity> onTargetAchieved,
                    AtomicEvent onAttacked,
                    AtomicVariable<float> attackPeriod,
                    Timer timer)
                {
                    onTargetAchieved.Subscribe(_ =>
                    {
                        if (timer.Time >= attackPeriod.Value)
                        {
                            timer.Reset();
                            onAttacked?.Invoke();
                        }
                        else
                        {
                            attackNotReady?.Invoke();
                        }
                    });
                }
            }

            [Serializable]
            private class DealDamageObserver
            {
                [Construct]
                public void Construct(
                    AtomicEvent dealDamage,
                    AtomicEvent onDamageDealt,
                    AtomicVariable<int> damage,
                    AtomicVariable<Entity> targetEnetity)
                {
                    dealDamage.Subscribe(() =>
                        targetEnetity.Value.Get<TakeDamageComponent>().TakeDamage(damage.Value));

                    onDamageDealt?.Invoke();
                }
            }
        }

        [Serializable]
        public partial class TargetSection
        {
            public AtomicEvent MoveToTargetRequest = new();

            public AtomicVariable<Entity> TargetEnetity = new();
            public AtomicVariable<Transform> TargetTransform = new();
            public AtomicVariable<float> DistanceAttack = new();
            public AtomicEvent<Entity> OnTargetAchieved = new();
            public AtomicEvent TargetIsDead = new();
            public AtomicEvent<Entity> TargetFound = new();

            private ChangeTargetObserver _changeTargetObserver = new();
            private TargetAchievementMechanics _targetAchievementMechanics = new();


            [Construct]
            public void Construct(
                TransformSection transform,
                MovementSection movement,
                EnemyModel model,
                LifeSection life)
            {
                DistanceAttack.Value = model.Config.DistanceAttack;

                _changeTargetObserver.Construct(
                    TargetFound,
                    TargetEnetity,
                    TargetTransform,
                    TargetIsDead,
                    life.OnDeath);

                _targetAchievementMechanics.Construct(
                    MoveToTargetRequest,
                    TargetTransform,
                    transform.Transform,
                    DistanceAttack,
                    OnTargetAchieved,
                    TargetEnetity,
                    movement.MoveRequest);
            }
        }

        [Serializable]
        public class MovementSection
        {
            [SerializeField] private RotationType _rotationType = RotationType.Slerp;

            public AtomicVariable<float> MoveSpeed;
            public AtomicEvent<Vector3> MoveRequest = new();

            public RotationEngine RotationEngine = new();
            public MoveEngine MoveEngine = new();


            [Construct]
            public void Construct(EnemyModel model, TransformSection transform)
            {
                MoveSpeed.Value = model.Config.CharacterConfig.MoveSpeed;
                AtomicVariable<float> rotationSpeedLerp = new(model.Config.CharacterConfig.RotationSpeedLerp);
                AtomicVariable<float> rotationSpeedTowards = new(model.Config.CharacterConfig.RotationSpeedTowards);
                MoveEngine.Construct(transform.Transform, MoveSpeed);
                RotationEngine.Construct(transform.Transform, rotationSpeedLerp, rotationSpeedTowards);

                MoveRequest.Subscribe(direction =>
                {
                    RotationEngine.UpdateRotation(direction, _rotationType);
                    MoveEngine.Move(direction);
                });
            }
        }

        [Serializable]
        public class TriggerSection
        {
            [SerializeField] private AtomicVariable<Collider> _trigger;


            [Construct]
            public void Construct(LifeSection life)
            {
                AtomicEvent onDeath = life.OnDeath;

                onDeath.Subscribe(() => _trigger.Value.enabled = false);
            }
        }

        [Serializable]
        public class AnimatorSection
        {
            public AtomicEvent<string> MessageReceived = new();
            public AtomicEvent Unsubscribe = new();

            public AtomicEvent AttackFinished = new();
            public AtomicEvent DealDamage = new();

            private const string ATTACK_FINISHED = "AttackFinished";
            private const string DEAL_DAMAGE = "DealDamage";


            [Construct]
            public void Construct(AnimatorObservable animatorObservable, TargetSection target, LifeSection life)
            {
                AtomicVariable<Entity> targetEnetity = target.TargetEnetity;

                animatorObservable.OnStringReceived += message => HandleMassage(message, targetEnetity);

                life.OnDeath.Subscribe(() => Unsubscribe?.Invoke());

                Unsubscribe.Subscribe(() =>
                    animatorObservable.OnStringReceived -= message => HandleMassage(message, targetEnetity));
            }

            private void HandleMassage(string message, AtomicVariable<Entity> targetEnetity)
            {
                if (targetEnetity.Value == null) return;

                if (message == ATTACK_FINISHED)
                {
                    AttackFinished?.Invoke();
                }
                else if (message == DEAL_DAMAGE)
                {
                    DealDamage?.Invoke();
                }
            }
        }

        [Serializable]
        public class EnemyStatesSection
        {
            public StateMachine<EnemyStateType> StateMachine = new();

            [Section] public EnemyIdleState IdleState = new();
            [Section] public EnemyRunState RunState = new();
            [Section] public MeleeAttackState MeleeAttackState = new();
            [Section] public PostAttackState PostAttackState = new();
            [Section] public DeathState DeathState = new();
            [Section] public ScatterState ScatterState = new();


            [Construct]
            public void ConstructFSM(EnemyModel model)
            {
                model.onStart += () => StateMachine.Enter();

                StateMachine.Construct(
                    (EnemyStateType.Idle, IdleState),
                    (EnemyStateType.Run, RunState),
                    (EnemyStateType.MeleeAttack, MeleeAttackState),
                    (EnemyStateType.PostAttack, PostAttackState),
                    (EnemyStateType.Death, DeathState),
                    (EnemyStateType.Scatter, ScatterState)
                );
            }

            [Construct]
            public void ConstructStates(TargetSection target, MovementSection movement)
            {
                RunState.Construct(target.MoveToTargetRequest);
                ScatterState.Construct(movement.MoveEngine, movement.RotationEngine);
            }

            [Construct]
            public void ConstructTransitions(
                TargetSection target,
                LifeSection life,
                AttackSection attack,
                AnimatorSection animator)
            {
                AtomicEvent attackNotReady = attack.AttackNotReady;
                AtomicEvent<Entity> targetFound = target.TargetFound;
                AtomicEvent onAttacked = attack.OnAttacked;
                AtomicEvent onDamageDealt = attack.OnDamageDealt;
                AtomicEvent attackFinished = animator.AttackFinished;
                AtomicEvent targetIsDead = target.TargetIsDead;
                AtomicEvent onDeath = life.OnDeath;

                AtomicVariable<Entity> targetEnetity = target.TargetEnetity;
                AtomicVariable<bool> isAlive = life.IsAlive;


                attackNotReady.Subscribe(() => StateMachine.SwitchState(EnemyStateType.Idle));
                targetFound.Subscribe(_ => StateMachine.SwitchState(EnemyStateType.Run));

                onAttacked.Subscribe(() =>
                {
                    if (!isAlive) return;
                    StateMachine.SwitchState(EnemyStateType.MeleeAttack);
                });

                onDamageDealt.Subscribe(() =>
                {
                    StateMachine.SwitchState(EnemyStateType.PostAttack);
                });

                attackFinished.Subscribe(() =>
                {
                    StateMachine.SwitchState(EnemyStateType.Run);
                });

                targetIsDead.Subscribe(() =>
                {
                    if (!isAlive) return;
                    StateMachine.SwitchState(EnemyStateType.Scatter);
                });

                onDeath.Subscribe(() => StateMachine.SwitchState(EnemyStateType.Death));
            }
        }
    }
}