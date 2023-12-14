using System.Linq;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    [CreateAssetMenu(
        fileName = "LoadingTaskParallel",
        menuName = "Configs/Tasks/New LoadingTaskParallel"
    )]
    public class LoadingTaskParallel : LoadingTask
    {
        [SerializeField] private LoadingTask[] _loadingTasks;


        public async override UniTask<Result> Do()
        {
            var length = _loadingTasks.Length;
            UniTask<Result>[] tasks = new UniTask<Result>[length];

            for (int i = 0; i < length; i++)
            {
                UniTask<Result> task = _loadingTasks[i].Do();
                tasks[i] = task;
            }

            Result[] results = await UniTask.WhenAll(tasks);

            return new Result
            {
                Success = results.All(it => it.Success)
            };
        }
    }
}