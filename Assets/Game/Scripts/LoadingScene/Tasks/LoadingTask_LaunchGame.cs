using Cysharp.Threading.Tasks;
using FrameworkUnity.OOP.Zenject;
using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    [CreateAssetMenu(
        fileName = "LoadingTask_LaunchGame",
        menuName = "Configs/Tasks/New LoadingTask_LaunchGame"
    )]
    public sealed class LoadingTask_LaunchGame : LoadingTask
    {
        public override UniTask<Result> Do()
        {
            Debug.Log("LAUNCH GAME");

            var ctx = GameObject.FindWithTag(nameof(GameManagerZenject)).GetComponent<GameManagerZenject>();

            //Запуск игры:
            ctx.PrepareGame();
            ctx.StartGame();

            return UniTask.FromResult(new Result
            {
                Success = true
            });
        }
    }
}