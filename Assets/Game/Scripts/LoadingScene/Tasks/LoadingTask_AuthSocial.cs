using Cysharp.Threading.Tasks;
using UnityEngine;
using Asyncoroutine;

namespace AtomicOrientedDesign.Shooter
{
    [CreateAssetMenu(
        fileName = "LoadingTask_AuthSocial",
        menuName = "Configs/Tasks/New LoadingTask_AuthSocial"
    )]
    public sealed class LoadingTask_AuthSocial : LoadingTask
    {
        [Header("Error")]
        [SerializeField] private string _error = "Ошибка авторизации!";

        [Header("Progress")]
        [Range(0, 1)]
        [SerializeField] private float _progress = 0.2f;
        
        [Header("Delay")]
        [SerializeField] private bool _simulateDelay = true;
        [SerializeField] private float _delay = 1f;


        public async override UniTask<Result> Do()
        {
            Debug.Log("<color=cyan>Auth Social</color>");

            var tcs = new UniTaskCompletionSource<Result>();

            Social.localUser.Authenticate(success =>
            {
                tcs.TrySetResult(new Result
                {
                    Success = success,
                    Error = _error,
                });

                LoadingScreen.ReportProgress(_progress);
            });

            if (_simulateDelay)
            {
                await new WaitForSeconds(_delay);
            }

            return await tcs.Task;
        }
    }
}