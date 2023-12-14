using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/PlayerConfig", order = 0)]
    public class PlayerConfig : ScriptableObject
    {
        [SerializeField] public int ClipSize;
        [SerializeField] public float BulletRegenerateTime;
        [SerializeField] public float AttackRadius;
        [SerializeField] public float ShotScatterAngle;
        [SerializeField] public float  ThresholdFireAngle;
        [SerializeField] public float  MagnetSpeed;
        [SerializeField] public float  StartMagnetRadius;
        [SerializeField] public LayerMask EnemyLayerMask;

        [SerializeField] public CharacterConfig CharacterConfig;
    }
}
