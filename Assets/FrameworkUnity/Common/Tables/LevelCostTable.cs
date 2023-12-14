using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace FrameworkUnity.Common
{
    [Serializable]
    public class LevelCostTable
    {
        [SerializeField] private LevelCost[] _levelCosts;


        public int GetCost(int level) => _levelCosts[level - 1].Cost;

        public void CalculateTable(int maxLevel, int startCost, float powMultiplier)
        {
            _levelCosts = new LevelCost[maxLevel];
            var baseCost = startCost - Mathf.Pow(startCost, 1 * powMultiplier);

            for (int i = 0; i < _levelCosts.Length; i++)
            {
                var level = i + 1;
                _levelCosts[i].Level = level;
                _levelCosts[i].Cost = Mathf.RoundToInt(baseCost + Mathf.Pow(startCost, level * powMultiplier));
            }
        }

        [Serializable]
        public struct LevelCost
        {
            [ReadOnly]
            public int Level;
            public int Cost;
        }
    }
}