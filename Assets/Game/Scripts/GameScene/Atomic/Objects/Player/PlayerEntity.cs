using FrameworkUnity.OOP.Interfaces.Listeners;
using Lessons.Gameplay.Atomic2;
using Zenject;

namespace AtomicOrientedDesign.Shooter
{
    public class PlayerEntity : Entity, IInitGameListener
    {
        private PlayerModel _model;


        [Inject]
        public void Construct(PlayerModel playerModel)
        {
            _model = playerModel;
        }

        public void OnInitGame()
        {
            Add(new ContactComponent(_model.Core.Contact.OnContact));
            Add(new DeclarativeModelComponent(_model));
            Add(new FireRequestComponent(_model.Core.Fire.FireRequest));
            Add(new TransformComponent(_model.Core.Transform.Transform));
            Add(new TakeDamageComponent(_model.Core.BlockDamage.TakeDamageRequest));
            Add(new BulletAmmoComponent(_model.Core.BulletStorage.BulletAmmo));
            Add(new FireEventComponent(_model.Core.Fire.FireEvent));
            Add(new ShotScatterMultiplierComponent(_model.Core.Attack.ShotScatterMultiplier));
            Add(new AbilityUserComponent(_model.AbilitiesParent));
            Add(new LifeComponent(_model.Core.Life.IsAlive, _model.Core.Life.OnDeath));
            Add(new LevelComponent(_model.Core.Level.Level));
            Add(new MagnetComponent(_model.Core.Magnet.Radius, _model.Core.Magnet.OnTargetEnter));
            Add(new BlockDamageComponent(
                _model.Core.BlockDamage.DamageIsBlocked,
                _model.Core.BlockDamage.BlockDamageRequest,
                _model.Core.BlockDamage.PostBlockDamageEvent));
            Add(new ExperienceComponent(
                _model.Core.Level.CurrentExperience,
                _model.Core.Level.RequiredExperience));
            Add(new MovementDirectionComponent(
                _model.Core.Movement.DirectionVariable,
                _model.Core.Movement.MovementAllowed));
            Add(new HitPointsComponent(
                _model.Core.Life.HitPoints,
                _model.Core.Life.MaxHitPoints,
                _model.Core.Life.HealingRequest));
        }
    }
}