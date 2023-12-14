using TMPro;
using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    public class PlayerLevelView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _level;
        
        public void SetLevel(string level) => _level.text = "LV " + level;
    }
}