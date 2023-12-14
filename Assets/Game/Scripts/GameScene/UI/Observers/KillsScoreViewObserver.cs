using FrameworkUnity.OOP.Interfaces.Listeners;
using Zenject;

namespace AtomicOrientedDesign.Shooter
{
    public class KillsScoreViewObserver : IStartGameListener, IDeInitGameListener
    {
        private ScoreManager _scoreManager;
        private KillsScoreView _scoreView;


        [Inject]
        public void Construct(ScoreManager scoreManager, KillsScoreView scoreView)
        {
            _scoreManager = scoreManager;
            _scoreView = scoreView;
        }

        public void OnStartGame()
        {
            _scoreManager.OnScoreChanged += UpdateView;
            _scoreManager.UpdateKillsScore();
        }

        public void OnDeInitGame() => _scoreManager.OnScoreChanged -= UpdateView;

        private void UpdateView(int score) => _scoreView.SetKillsScore(score.ToString());
    }
}