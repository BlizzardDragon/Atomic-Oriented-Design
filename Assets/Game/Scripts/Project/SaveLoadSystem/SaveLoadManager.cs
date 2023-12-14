using System;
using Zenject;

namespace AtomicOrientedDesign.Shooter
{
    public class SaveLoadManager : IDisposable
    {
        private GameRepository _repository;
        private ISaveLoader[] _saveLoaders;


        [Inject]
        public void Construct(ISaveLoader[] saveLoaders, GameRepository repository)
        {
            _saveLoaders = saveLoaders;
            _repository = repository;
        }

        public void Dispose() => Save();
        
        public void Load()
        {
            _repository.LoadState();

            foreach (var saveLoader in _saveLoaders)
            {
                saveLoader.LoadData(_repository);
            }
        }
        
        public void Save()
        {
            foreach (var saveLoader in _saveLoaders)
            {
                saveLoader.SaveData(_repository);
            }

            _repository.SaveState();
        }
        
        public void ClearData() => _repository.ClearData();


        // //TODO: TIMER

        // private void OnApplicationFocus(bool focusStatus)
        // {
        //     if (!_repository.HasData()) return;

        //     if (!focusStatus)
        //     {
        //         Save();
        //     }
        // }

        // private void OnApplicationPause(bool pauseStatus)
        // {
        //     if (!_repository.HasData()) return;

        //     if (pauseStatus)
        //     {
        //         Save();
        //     }
        // }

        // private void OnApplicationQuit()
        // {
        //     if (!_repository.HasData()) return;

        //     Save();
        // }
    }
}
