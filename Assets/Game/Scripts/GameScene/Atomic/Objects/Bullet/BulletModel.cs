using System;
using Atomic;
using Declarative;
using Lessons.Gameplay.Atomic2;
using UnityEngine;
using Zenject;

namespace AtomicOrientedDesign.Shooter
{
    public class BulletModel : DeclarativeModel
    {
        [SerializeField] private Transform _bullet;
        public BulletConfig BulletConfig;
        public AtomicVariable<Teams> Team = new();

        public Transform Bullet => _bullet;

        [Section][SerializeField] public MoveSection Move;
        [Section][SerializeField] public ContactSection Contact;
        [Section][SerializeField] public new DestroySection Destroy;


        [Inject]
        public void Construct(Vector3 position, Quaternion quaternion, Teams team, int damage)
        {
            _bullet.position = position;
            _bullet.rotation = quaternion;
            Team.Value = team;
            Contact.Damage.Value = damage;
        }

        [Inject]
        public void Construct(DestroyService destroyService)
        {
            Destroy.DestroyService = destroyService;
        }

        [Serializable]
        public class MoveSection
        {
            public AtomicVariable<float> Speed = new();
            private UpdateMechanics _update = new();


            [Construct]
            public void Construct(BulletModel model)
            {
                Speed.Value = model.BulletConfig.Speed;
                Transform bullet = model.Bullet;

                _update.OnUpdate(deltaTime =>
                    bullet.Translate(bullet.forward * Speed.Value * deltaTime, Space.World));
            }
        }

        [Serializable]
        public class ContactSection
        {
            public AtomicVariable<int> Damage = new();
            public AtomicEvent<Entity> OnContact = new();


            [Construct]
            public void Construct(BulletModel model, DestroySection destroy)
            {
                Teams team = model.Team.Value;

                OnContact.Subscribe(entity =>
                {
                    if (entity.TryGet(out TeamComponent teamComponent))
                    {
                        if (teamComponent.Team != team)
                        {
                            entity.Get<TakeDamageComponent>().TakeDamage(Damage.Value);
                            destroy.OnDestroy?.Invoke();
                        }
                    }
                });
            }
        }

        [Serializable]
        public class DestroySection
        {
            public AtomicVariable<float> _lifeTime = new();

            public AtomicEvent OnDestroy = new();

            public DestroyService DestroyService;

            private UpdateMechanics _update = new();
            private Timer _timer = new();


            [Construct]
            public void Construct(BulletModel model)
            {
                _lifeTime.Value = model.BulletConfig.LifeTime;

                _update.OnUpdate(_ =>
                {
                    if (_timer.Time > _lifeTime.Value)
                    {
                        OnDestroy?.Invoke();
                    }
                });

                OnDestroy.Subscribe(() => DestroyService.DestroyGameObject(model.Bullet.gameObject));
            }
        }

        public sealed class Factory : PlaceholderFactory<Vector3, Quaternion, Teams, int, BulletModel> { }

        public sealed class Pool : MonoMemoryPool<Vector3, BulletModel>
        {
            protected override void Reinitialize(Vector3 position, BulletModel bullet)
            {
                bullet.transform.position = position;
            }
        }
    }
}
