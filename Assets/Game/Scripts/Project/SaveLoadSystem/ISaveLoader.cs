namespace AtomicOrientedDesign.Shooter
{
    public interface ISaveLoader
    {
        void SaveData(IGameRepository repository);
        void LoadData(IGameRepository repository);
    }
}
