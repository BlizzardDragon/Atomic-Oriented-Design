using FrameworkUnity.OOP.Interfaces.Listeners;
using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    public class RestartView : MonoBehaviour, ILoseGameListener
    {
        [SerializeField] private GameObject _loseScreen;

        public void OnLoseGame() => _loseScreen.SetActive(true);
    }
}
