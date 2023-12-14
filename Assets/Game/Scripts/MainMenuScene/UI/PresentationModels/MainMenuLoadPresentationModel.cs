using Zenject;

namespace AtomicOrientedDesign.Shooter
{
    public class MainMenuLoadPresentationModel : IMainMenuButtonsPresentationModel
    {
        private SceneLoader _sceneLoader;


        [Inject]
        public void Construct(SceneLoader sceneLoader) => _sceneLoader = sceneLoader;

        public void OnClick(MainMenuButtonType popupType)
        {
            if (popupType == MainMenuButtonType.Start)
            {
                _sceneLoader.LoadGameScene();
            }
            else if (popupType == MainMenuButtonType.Exit)
            {
                _sceneLoader.Quit();
            }
        }
    }
}