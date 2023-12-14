using UnityEngine;
using Zenject;

namespace AtomicOrientedDesign.Shooter
{
    //1. Загрузка AppsFlyer & Facebook
    //2. Авторизация в Gaming Services
    //3. Инициализация встроенных покупок
    //4. Загрузка данных игрока
    //TODO 5. Загрузка попапов из Addressables
    //6. Загрузка сцены игры

    public sealed class ApplicationLoader : MonoBehaviour
    {
        [SerializeField] private LoadingTask[] _loadingTasks;
        private DiContainer _container;


        [Inject]
        public void Construct(DiContainer container) => _container = container;

        private async void Start()
        {
            foreach (var task in _loadingTasks)
            {
                _container.Inject(task);
                var result = await task.Do();

                if (!result.Success)
                {
                    LoadingScreen.ReportError(result.Error);
                    break;
                }
            }
        }
    }
}
