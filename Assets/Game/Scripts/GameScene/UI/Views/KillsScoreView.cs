using TMPro;
using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    public class KillsScoreView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _score;
        
        public void SetKillsScore(string score) => _score.text = "KILLS: " + score;
    }
}