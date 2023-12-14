using Atomic;
using Declarative;
using UnityEngine;
using Zenject;

namespace AtomicOrientedDesign.Shooter
{
    public class PlayerModel : DeclarativeModel
    {
        [SerializeField] private Transform _gun;
        [SerializeField] private Transform _abilitiesParent;
        public PlayerConfig PlayerConfig;
        public PlayerExperienceConfig ExperienceConfig;
        public Transform Gun => _gun;
        public Transform AbilitiesParent => _abilitiesParent;
        public AtomicVariable<Teams> Team = new();

        [Section] public PlayerModel_Core Core;
        [Section] public PlayerModel_View View;


        public void SetParentVFX(Transform transformVFX) => View.ParentVFX = transformVFX;

        [Construct]
        public void Construct()
        {
            Core.Life.HitPoints.Value = PlayerConfig.CharacterConfig.HitPoints;
            Core.Life.MaxHitPoints.Value = PlayerConfig.CharacterConfig.MaxHitPoints;
            Core.Movement.MoveSpeed.Value = PlayerConfig.CharacterConfig.MoveSpeed;
            Core.Movement.RotationSpeedLerp.Value = PlayerConfig.CharacterConfig.RotationSpeedLerp;
            Core.Movement.RotationSpeedTowards.Value = PlayerConfig.CharacterConfig.RotationSpeedTowards;
            Core.TargetSearch.AttackRadius.Value = PlayerConfig.AttackRadius;
            Core.TargetSearch.TargetLayerMask.Value = PlayerConfig.EnemyLayerMask;
            Core.Attack.ShotScatterAngle.Value = PlayerConfig.ShotScatterAngle;
            Core.Attack.ThresholdFireAngle.Value = PlayerConfig.ThresholdFireAngle;
            Core.Level.ExperienceConfig = ExperienceConfig;
            Core.Magnet.Speed.Value = PlayerConfig.MagnetSpeed;
            Core.Magnet.Radius.Value = PlayerConfig.StartMagnetRadius;
            Team.Value = PlayerConfig.CharacterConfig.Team;
            Core.TargetSearch.SetTransform(Core.Transform.Transform);
        }

        [Inject]
        public void Construct(BulletSpawner bulletSpawner, DestroyService destroyService)
        {
            Core.Fire.FireAction = new FireBulletAction(bulletSpawner);
            Core.Destroy.DestroyService = destroyService;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(Gun.position, Gun.position + Gun.forward * PlayerConfig.AttackRadius);
        }
    }
}
