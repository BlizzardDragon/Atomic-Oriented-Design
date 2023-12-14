using Declarative;
using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    public class FireGrenadeModel : DeclarativeModel
    {
        [Section] public FireGrenadeModel_Core Core;
        [Section] public FireGrenadeModel_View View;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(
                Core.MoveFromTo.TargetPosition + Vector3.one * 0.1f, Core.Explosion.ExplosionRadius);
        }
    }
}