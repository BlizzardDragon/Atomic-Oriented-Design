using System;

namespace AtomicOrientedDesign.Shooter
{
    public class ScoreManager
    {
        private int _killsScore = 0;

        public event Action<int> OnScoreChanged;


        public void AddKillsScore()
        {
            _killsScore++;
            UpdateKillsScore();
        }

        public void UpdateKillsScore() => OnScoreChanged?.Invoke(_killsScore);
    }
}