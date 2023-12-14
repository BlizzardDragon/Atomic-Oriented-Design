using Atomic;

namespace AtomicOrientedDesign.Shooter
{
    public interface ITeamComponent
    {
        Teams Team { get; }
    }

    public class TeamComponent : ITeamComponent
    {
        public Teams Team => _team.Value;

        private readonly AtomicVariable<Teams> _team;

        public TeamComponent(AtomicVariable<Teams> team)
        {
            _team = team;
        }
    }
}