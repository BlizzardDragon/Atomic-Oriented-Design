using System;
using Zenject;

namespace AtomicOrientedDesign.Shooter
{
    public class GameMenuPopupViewObserver : IInitializable, IDisposable
    {
        private GameMenuPopupView _view;
        private SceneLoader _sceneLoader;


        [Inject]
        public void Construct(SceneLoader sceneLoader, GameMenuPopupView view)
        {
            _sceneLoader = sceneLoader;
            _view = view;
        }

        public void Initialize()
        {
            _view.Restart.onClick.AddListener(Restart);
            _view.MainMenu.onClick.AddListener(MainMenu);
            _view.Exit.onClick.AddListener(Quit);
        }

        public void Dispose()
        {
            _view.Restart.onClick.RemoveListener(Restart);
            _view.MainMenu.onClick.RemoveListener(MainMenu);
            _view.Exit.onClick.RemoveListener(Quit);
        }

        private void Restart() => _sceneLoader.Restart();
        private void MainMenu() => _sceneLoader.LoadMainMenu();
        private void Quit() => _sceneLoader.Quit();
    }
}