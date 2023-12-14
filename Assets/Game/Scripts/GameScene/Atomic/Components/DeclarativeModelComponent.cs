using Declarative;

namespace AtomicOrientedDesign.Shooter
{
    public interface IDeclarativeModelComponent
    {
        DeclarativeModel DeclarativeModel { get; }
    }

    public class DeclarativeModelComponent : IDeclarativeModelComponent
    {
        private DeclarativeModel _declarativeModel;

        public DeclarativeModel DeclarativeModel => _declarativeModel;

        public DeclarativeModelComponent(DeclarativeModel declarativeModel)
        {
            _declarativeModel = declarativeModel;
        }
    }
}