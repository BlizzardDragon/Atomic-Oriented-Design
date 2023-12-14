using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    public interface IAbility
    {
        void SetLevel(int level);
        GameObject GetGameObject();
    }
}