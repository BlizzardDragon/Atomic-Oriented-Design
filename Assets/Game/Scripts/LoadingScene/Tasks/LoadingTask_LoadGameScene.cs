using System.Collections;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AtomicOrientedDesign.Shooter
{
    [CreateAssetMenu(
        fileName = "LoadingTask_LoadGameScene",
        menuName = "Configs/Tasks/New LoadingTask_LoadGameScene"
    )]
    public sealed class LoadingTask_LoadGameScene : LoadingTask
    {
        public async override UniTask<Result> Do()
        {
            await LoadGameScene();
            return await UniTask.FromResult(new Result
            {
                Success = true
            });
        }

        private IEnumerator LoadGameScene()
        {
            var operation = SceneManager.LoadSceneAsync(Constants.MAIN_MENU_INDEX);
            while (!operation.isDone)
            {
                LoadingScreen.ReportProgress(operation.progress / 2);
                yield return null;
            }
        }
    }
}