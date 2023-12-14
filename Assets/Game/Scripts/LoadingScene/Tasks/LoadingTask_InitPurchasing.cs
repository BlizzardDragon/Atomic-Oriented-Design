using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Unity.Services.Core;
using Asyncoroutine;
using UnityEngine;
using Zenject;

namespace AtomicOrientedDesign.Shooter
{
    [CreateAssetMenu(
        fileName = "LoadingTask_InitPurchasing",
        menuName = "Configs/Tasks/New LoadingTask_InitPurchasing"
    )]
    public sealed class LoadingTask_InitPurchasing : LoadingTask
    {
        [Header("Error")]
        [SerializeField] private string _error = "Встроенные покупки не загружены!";

        [Header("Progress")]
        [Range(0, 1)]
        [SerializeField] private float _progress = 0.1f;

        [Header("Delay")]
        [SerializeField] private bool _simulateDelay = true;
        [SerializeField] private float _delay = 1f;

        private PurchaseManager _purchaseManager;


        [Inject]
        public void Construct(PurchaseManager purchaseManager)
        {
            _purchaseManager = purchaseManager;
        }

        public async override UniTask<Result> Do()
        {
            Debug.Log("<color=magenta>Init Purchase</color>");
            
            await UnityServices.InitializeAsync();

            var tcs = new TaskCompletionSource<Result>();

            _purchaseManager.Initialize(result =>
            {
                tcs.SetResult(new Result
                {
                    Success = result,
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