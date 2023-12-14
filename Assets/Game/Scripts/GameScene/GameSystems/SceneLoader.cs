using UnityEngine;
using UnityEngine.SceneManagement;

namespace AtomicOrientedDesign.Shooter
{
    public class SceneLoader
    {
        public void Restart() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        public void LoadMainMenu() => SceneManager.LoadScene(Constants.MAIN_MENU_INDEX);
        public void LoadGameScene() => SceneManager.LoadScene(Constants.GAME_SCENE_INDEX);
        public void Quit() => Application.Quit();
    }
}
