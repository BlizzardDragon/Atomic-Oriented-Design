using System;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "IceProtectionAbilityConfig",
    menuName = "Configs/Ability/IceProtectionAbilityConfig",
    order = 0)]
public class IceProtectionAbilityConfig : ScriptableObject
{
    [Header("Level")]
    [Min(1)]
    public int MaxLevel;
    
    [Header("Shield")]
    public float EndReloadTime;
    public float ReloadTimeMultiplier;

    [Header("IceNova")]
    public float FreezeTime;
    public float DelayBeforeFreezing;
    public float MinSpeed;
    public LayerMask TargetLayerMask;
    
    [Space]
    public LevelData[] LevelsData;


    public LevelData GetLevelData(int level)
    {
        foreach (var levelData in LevelsData)
        {
            if (levelData.Level == level) return levelData;
        }

        throw new ArgumentException($"Wrong level({level})");
    }

    private void OnValidate()
    {
        var oldData = LevelsData;
        LevelsData = new LevelData[MaxLevel];

        for (int i = 0; i < LevelsData.Length; i++)
        {
            if (i < oldData.Length)
            {
                LevelsData[i] = oldData[i];
            }

            LevelsData[i].Level = i + 1;
        }

        for (int i = LevelsData.Length - 1; i >= 0; i--)
        {
            if (i == LevelsData.Length - 1)
            {
                LevelsData[i].ShieldReloadTime = EndReloadTime;
            }
            else
            {
                LevelsData[i].ShieldReloadTime = LevelsData[i + 1].ShieldReloadTime * ReloadTimeMultiplier;
            }
        }
    }


    [Serializable]
    public struct LevelData
    {
        [Space]
        [ReadOnly]
        public int Level;

        [ReadOnly]
        public float ShieldReloadTime;
        public int Damage;
        public float ExplosionRadius;
    }
}