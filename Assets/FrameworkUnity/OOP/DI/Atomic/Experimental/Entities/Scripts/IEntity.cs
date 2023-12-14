namespace Entities
{
    public interface IEntity
    {
        T GetReference<T>(string name) where T : class;
        bool TryGetReference<T>(string name, out T reference) where T : class;
        bool ContainsReference(string name);
    }
}