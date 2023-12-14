using TMPro;
using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    public sealed class LoadingScreen : MonoBehaviour
    {
        private static LoadingScreen instance;

        [SerializeField] private TMP_Text _errorText;
        [SerializeField] private LoadingProgressBar _progressBar;


        private void Awake()
        {
            instance = this;
            _errorText.text = string.Empty;
            _progressBar.SetProgress(0.0f);
        }

        private void OnDestroy()
        {
            instance = null;
        }

        public static void Show()
        {
            instance.gameObject.SetActive(true);
        }

        public static void ReportProgress(float progress)
        {
            instance._progressBar.SetProgress(progress);
        }

        public static void Hide()
        {
            instance.gameObject.SetActive(false);
        }

        public static void ReportError(string message)
        {
            instance._errorText.text = message;
        }
    }
}
