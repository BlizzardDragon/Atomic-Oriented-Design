using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    public class DestroyService
    {
        public void DestroyGameObject(GameObject gameObject) => Object.Destroy(gameObject);
        public void DestroyObject(Object obj) => Object.Destroy(obj);
    }
}