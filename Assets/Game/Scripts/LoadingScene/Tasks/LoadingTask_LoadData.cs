using Cysharp.Threading.Tasks;
using Asyncoroutine;
using UnityEngine;
using Zenject;

namespace AtomicOrientedDesign.Shooter
{
    [CreateAssetMenu(
        fileName = "LoadingTask_LoadData",
        menuName = "Configs/Tasks/New LoadingTask_LoadData"
    )]
    public class LoadingTask_LoadData : LoadingTask
    {
        [Header("Progress")]
        [Range(0, 1)]
        [SerializeField] private float progress = 0.9f;

        [Header("Delay")]
        [SerializeField] private bool _simulateDelay = true;
        [SerializeField] private float _delay = 1f;

        private SaveLoadManager _saveLoadManager;


        [Inject]
        public void Construct(SaveLoadManager saveLoadManager)
        {
            _saveLoadManager = saveLoadManager;
        }

        public async override UniTask<Result> Do()
        {
            Debug.Log("<color=green>Load GameData</color>");

            _saveLoadManager.Load();
            LoadingScreen.ReportProgress(progress);

            if (_simulateDelay)
            {
                await new WaitForSeconds(_delay);
            }

            return await UniTask.FromResult(new Result
            {
                Success = true
            });
        }
    }
}