using FrameworkUnity.OOP.Interfaces.Listeners;
using Zenject;

namespace AtomicOrientedDesign.Shooter
{
    public class PlayerExperienceViewObserver : IInitGameListener, IDeInitGameListener
    {
        private PlayerEntity _entity;
        private PlayerExperienceView _viewImage;
        private ExperienceComponent _experienceComponent;


        [Inject]
        public void Construct(PlayerEntity entity, PlayerExperienceView viewImage)
        {
            _entity = entity;
            _viewImage = viewImage;
        }

        public void OnInitGame()
        {
            _experienceComponent = _entity.Get<ExperienceComponent>();
            _experienceComponent.OnCurrentExperienceChanged += UpdateExperienceScale;
            UpdateExperienceScale(_experienceComponent.CurrentExperience);
        }

        public void OnDeInitGame() => _experienceComponent.OnCurrentExperienceChanged -= UpdateExperienceScale;

        private void UpdateExperienceScale(int currentValue)
        {
            float fillAmount = (float)currentValue / _experienceComponent.RequiredExperience;
            _viewImage.SetFillAmount(fillAmount);
        }
    }
}
