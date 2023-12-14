using Cysharp.Threading.Tasks;
using FrameworkUnity.OOP.Zenject;
using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    [CreateAssetMenu(
        fileName = "LoadingTask_InitGame",
        menuName = "Configs/Tasks/New LoadingTask_InitGame"
    )]
    public sealed class LoadingTask_InitGame : LoadingTask
    {
        public override UniTask<Result> Do()
        {
            Debug.Log("INIT GAME");

            //Инициализация игровой системы:
            var ctx = GameObject.FindWithTag(nameof(GameManagerZenject)).GetComponent<GameManagerZenject>();

            ctx.InitGame();

            return UniTask.FromResult(new Result
            {
                Success = true
            });
        }
    }
}