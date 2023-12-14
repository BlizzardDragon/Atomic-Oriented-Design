using System.Collections;
using Lessons.Gameplay.Atomic2;
using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    public class FreezeEffect : MonoBehaviour
    {
        private float _startMoveSpeed;
        private MoveSpeedComponent _moveSpeedComponent;
        private AnimatorComponent _animatorComponent;
        private LifeComponent _lifeComponent;


        public void StartFreezing(float delay, float freezeTime, float minSpeed)
        {
            var entity = GetComponent<Entity>();
            _moveSpeedComponent = entity.Get<MoveSpeedComponent>();
            _animatorComponent = entity.Get<AnimatorComponent>();
            _lifeComponent = entity.Get<LifeComponent>();

            _lifeComponent.OnDeath += RestoreSpeed;

            StartCoroutine(SlowdownProcess(delay, freezeTime, minSpeed));
        }

        private IEnumerator SlowdownProcess(float delay, float freezeTime, float minSpeed)
        {
            _startMoveSpeed = _moveSpeedComponent.MoveSpeed;

            yield return new WaitForSeconds(delay);

            _moveSpeedComponent.SetMoveSpeed(minSpeed);
            _animatorComponent.Animator.speed = 0.2f;

            yield return new WaitForSeconds(freezeTime);

            Destroy(this);
        }

        private void RestoreSpeed()
        {
            _moveSpeedComponent.SetMoveSpeed(_startMoveSpeed);
            _animatorComponent.Animator.speed = 1;
        }

        private void OnDisable()
        {
            _lifeComponent.OnDeath -= RestoreSpeed;
            RestoreSpeed();
        }
    }
}