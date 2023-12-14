using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{

    [CreateAssetMenu(fileName = "EnemyConfig", menuName = "Configs/EnemyConfig", order = 1)]
    public class EnemyConfig : ScriptableObject
    {
        public float DistanceAttack;
        [SerializeField] public CharacterConfig CharacterConfig;
    }
}