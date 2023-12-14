using System.Threading.Tasks;
using Asyncoroutine;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    [CreateAssetMenu(
        fileName = "LoadingTask_FinishLoading",
        menuName = "Configs/Tasks/New LoadingTask_FinishLoading"
    )]
    public sealed class LoadingTask_FinishLoading : LoadingTask
    {
        [SerializeField] private float _delay = 1f;


        public override async UniTask<Result> Do()
        {
            Debug.Log("Finish Loading");

            LoadingScreen.ReportProgress(1f);
            await new WaitForSeconds(_delay);
            LoadingScreen.Hide();

            return await Task.FromResult(new Result
            {
                Success = true
            });
        }
    }
}