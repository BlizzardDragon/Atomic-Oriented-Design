using TMPro;
using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    public class PlayerHitPointsView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _hitPoints;
        
        public void SetHitPoints(string hitPoints) => _hitPoints.text = "HIT POINTS: " + hitPoints;
    }
}