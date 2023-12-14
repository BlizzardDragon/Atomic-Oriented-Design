using System;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "FireGrenadeAbilityConfig", 
    menuName = "Configs/Ability/FireGrenadeAbilityConfig", 
    order = 0)]
public class FireGrenadeAbilityConfig : ScriptableObject
{
    [Min(1)]
    public int MaxLevel;
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
    }


    [Serializable]
    public struct LevelData
    {
        [Space]
        [ReadOnly]
        public int Level;
        public int SpawnNubmer;
        public float ReloadTime;
        public int Damage;
        public float Speed;
        public float ExplosionRadius;
        public float MinFlyRadius;
        public float MaxFlyRadius;
    }
}