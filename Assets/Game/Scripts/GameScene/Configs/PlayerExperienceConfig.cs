using System;
using AtomicOrientedDesign.Shooter;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerExperienceConfig", menuName = "Configs/PlayerExperienceConfig", order = 0)]
public class PlayerExperienceConfig : ScriptableObject, IExperienceConfig
{
    [SerializeField] private int _startExperience = 50;
    [SerializeField] private float _levelMultiplier = 0.2f;
    
    [Space]
    [SerializeField] private int _displayedTableLength = 20;
    [SerializeField] private ExperienceData[] _table;


    public int GetRequiredExperience(int level)
    {
        if (level <= 0)
        {
            return 0;
        }
        else
        {
            return (int)(_startExperience * (1 + ((level * _levelMultiplier) - _levelMultiplier)));
        }
    }

    private void OnValidate()
    {
        _table = new ExperienceData[_displayedTableLength];

        for (int i = 0; i < _displayedTableLength; i++)
        {
            _table[i].Level = i + 1;
            _table[i].Experience = GetRequiredExperience(i + 1);
        }
    }

    [Serializable]
    public struct ExperienceData
    {
        public int Level;
        public int Experience;
    }
}