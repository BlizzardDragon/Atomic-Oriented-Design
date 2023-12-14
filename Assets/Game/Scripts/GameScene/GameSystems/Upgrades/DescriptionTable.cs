using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    [Serializable]
    public class DescriptionTable
    {
        public DescriptionElement[] Table;


        public string GetDescription(int level)
        {
            if (level < 1)
            {
                throw new ArgumentOutOfRangeException($"Level({level}) cannot be less than 1!");
            }

            return Table[level - 1].Description;
        }

        public void SetTableLength(int length)
        {
            if (length < 1) return;

            var oldTable = Table;
            Table = new DescriptionElement[length];

            for (int i = 0; i < length; i++)
            {
                if (i < oldTable.Length)
                {
                    Table[i].Description = oldTable[i].Description;
                }

                Table[i].Level = i + 1;
            }
        }

        [Serializable]
        public struct DescriptionElement
        {
            [ReadOnly]
            public int Level;

            [TextArea]
            public string Description;
        }
    }
}