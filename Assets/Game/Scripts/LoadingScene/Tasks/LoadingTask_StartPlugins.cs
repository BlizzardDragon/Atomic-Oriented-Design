using Asyncoroutine;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    [CreateAssetMenu(
        fileName = "LoadingTask_StartPlugins",
        menuName = "Configs/Tasks/New LoadingTask_StartPlugins"
    )]
    public sealed class LoadingTask_StartPlugins : LoadingTask
    {
        [Header("Progress")]
        [Range(0, 1)]
        [SerializeField] private float _progress = 0.1f;

        [Header("Delay")]
        [SerializeField] private bool _simulateDelay = true;
        [SerializeField] private float _delay = 1f;


        public async override UniTask<Result> Do()
        {
            AppsFlyer.startSDK();

            var tcs = new UniTaskCompletionSource<Result>();

            FB.Init(
                onSuccess: () => tcs.TrySetResult(new Result
                {
                    Success = true,
                }),
                onError: err => tcs.TrySetResult(new Result
                {
                    Success = false,
                    Error = err,
                })
            );
            
            LoadingScreen.ReportProgress(_progress);

            if (_simulateDelay)
            {
                await new WaitForSeconds(_delay);
            }

            return await tcs.Task;
        }
    }
}