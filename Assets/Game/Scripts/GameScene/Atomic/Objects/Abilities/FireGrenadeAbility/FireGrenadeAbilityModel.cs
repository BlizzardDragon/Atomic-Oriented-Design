using System;
using Atomic;
using Declarative;
using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    public sealed class FireGrenadeAbilityModel : AbilityModel, IAbility
    {
        [Section][SerializeField] public TransformSection TransformSection;
        [Section][SerializeField] public RepeatingSpawnSection RepeatingSpawn;
        [Section][SerializeField] public SetupSection Setup;


        [Construct]
        public void Construct()
        {
            Setup.Level = Level;
        }

        public void SetLevel(int level) => Level.Value = level;
        public GameObject GetGameObject() => gameObject;

        protected override void OnLevelUp(int level)
        {
            var levelData = Setup.Config.GetLevelData(level);
            RepeatingSpawn.CallbackTimer.SetCallbackTime(levelData.ReloadTime);
            RepeatingSpawn.SpawnNubmer.Value = levelData.SpawnNubmer;
        }

        [Serializable]
        public class SetupSection
        {
            public AtomicVariable<int> Level;
            public RandomPositionXZInRadius RandomPositionXZInRadiusFunction = new();
            public FireGrenadeAbilityConfig Config;


            [Construct]
            public void Construct(TransformSection transformSection, RepeatingSpawnSection repeatingSpawn)
            {
                var startLevelData = Config.GetLevelData(Level);
                var reloadTime = startLevelData.ReloadTime;
                repeatingSpawn.CallbackTimer.SetCallbackTime(reloadTime);
                RandomPositionXZInRadiusFunction.MinRadius.Value = startLevelData.MinFlyRadius;
                RandomPositionXZInRadiusFunction.MaxRadius.Value = startLevelData.MaxFlyRadius;

                repeatingSpawn.OnSpawn.Subscribe(gameObj =>
                {
                    var levelData = Config.GetLevelData(Level);
                    var startPosition = transformSection.Transform.position;
                    var targetPosition = startPosition + RandomPositionXZInRadiusFunction.Invoke();
                    var direction = targetPosition - startPosition;
                    var model = gameObj.GetComponent<FireGrenadeModel>();

                    model.Core.TransformSection.Transform.position = startPosition;
                    model.Core.TransformSection.Transform.rotation = Quaternion.LookRotation(direction);
                    model.Core.MoveFromTo.Speed.Value = levelData.Speed;
                    model.Core.MoveFromTo.TargetPosition.Value = targetPosition;
                    model.Core.Explosion.Damage.Value = levelData.Damage;
                    model.Core.Explosion.ExplosionRadius.Value = levelData.ExplosionRadius;
                });
            }
        }
    }
}