using Atomic;
using Declarative;
using Zenject;

namespace AtomicOrientedDesign.Shooter
{
    public class EnemyModel : DeclarativeModel
    {
        public EnemyConfig Config;
        public AtomicVariable<Teams> Team = new();

        [Section] public EnemyModel_Core Core;
        [Section] public EnemyModel_View View;


        [Inject]
        public void Construct(ExperienceSpawner spawner)
        {
            Core.DroppedExperience.SpawnExperience = new SpawnExperienceAction(spawner);
        }

        [Construct]
        public void Construct()
        {
            Core.Life.HitPoints.Value = Config.CharacterConfig.HitPoints;
            Core.Life.MaxHitPoints.Value = Config.CharacterConfig.MaxHitPoints;
            Team.Value = Config.CharacterConfig.Team;
        }
    }
}