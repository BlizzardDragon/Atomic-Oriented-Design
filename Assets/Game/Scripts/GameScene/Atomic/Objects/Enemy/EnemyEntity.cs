using Lessons.Gameplay.Atomic2;
using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    public class EnemyEntity : Entity
    {
        [SerializeField] private EnemyModel _model;

        internal void Init()
        {
            Add(new SendMessageComponent(_model.Core.Animator.MessageReceived));
            Add(new TakeDamageComponent(_model.Core.Life.TakeDamageRequest));
            Add(new TeamComponent(_model.Team));
            Add(new TargetComponent(_model.Core.Target.TargetEnetity));
            Add(new LifeComponent(_model.Core.Life.IsAlive, _model.Core.Life.OnDeath));
            Add(new MoveSpeedComponent(_model.Core.Movement.MoveSpeed));
            Add(new AnimatorComponent(_model.View.Animator));
        }
    }
}