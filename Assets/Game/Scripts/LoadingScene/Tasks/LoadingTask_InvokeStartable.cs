using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace AtomicOrientedDesign.Shooter
{
    [CreateAssetMenu(
        fileName = "LoadingTask_InvokeStartable",
        menuName = "Configs/Tasks/New LoadingTask_InvokeStartable"
    )]
    public sealed class LoadingTask_InvokeStartable : LoadingTask
    {
        private IAppStartable[] _startables;


        [Inject]
        public void Construct(IAppStartable[] startables)
        {
            _startables = startables;
        }

        public override UniTask<Result> Do()
        {
            foreach (var startable in _startables)
            {
                startable.Start();
            }

            return UniTask.FromResult(new Result
            {
                Success = true
            });
        }
    }
}