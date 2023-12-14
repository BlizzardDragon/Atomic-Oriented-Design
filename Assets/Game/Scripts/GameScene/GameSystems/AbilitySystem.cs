using System.Collections.Generic;
using Declarative;
using FrameworkUnity.OOP.Interfaces.Listeners;
using UnityEngine;
using Zenject;

namespace AtomicOrientedDesign.Shooter
{
    public enum AbilityType
    {
        FireGrenade,
        TargetedShooting,
        PowerOfNature,
        PhotonCore,
        IceProtection,
    }

    public class AbilitySystem : IDeInitGameListener
    {
        private PlayerEntity _playerEntity;
        private DestroyService _destroyService;
        private DiContainer _diContainer;

        private readonly Dictionary<AbilityType, IAbility> _abilities = new();

        private readonly Dictionary<AbilityType, string> _abilityLinks = new()
        {
            {AbilityType.FireGrenade, DEFAULT_PATH + "FireGrenadeAbility"},
            {AbilityType.TargetedShooting, DEFAULT_PATH + "TargetedShootingAbility"},
            {AbilityType.PowerOfNature, DEFAULT_PATH + "PowerOfNatureAbility"},
            {AbilityType.PhotonCore, DEFAULT_PATH + "PhotonCoreAbility"},
            {AbilityType.IceProtection, DEFAULT_PATH + "IceProtectionAbility"},
        };

        private const string DEFAULT_PATH = "Prefabs/Abilities/";


        [Inject]
        public void Construct(
            PlayerEntity playerEntity,
            DestroyService destroyService,
            DiContainer diContainer)
        {
            _playerEntity = playerEntity;
            _destroyService = destroyService;
            _diContainer = diContainer;
        }

        public IAbility GetAblility(AbilityType type)
        {
            if (!_abilities.ContainsKey(type))
            {
                var ability = AddAbility(type);
                return ability;
            }
            else
            {
                return _abilities[type];
            }
        }

        public void RemoveAllAbilities()
        {
            foreach (var ability in _abilities)
            {
                _destroyService.DestroyGameObject(ability.Value.GetGameObject());
            }
            _abilities.Clear();
        }

        public bool TryAddAbility(AbilityType type)
        {
            if (!_abilities.ContainsKey(type)) return false;

            AddAbility(type);
            return true;
        }

        public bool HasAbility(AbilityType type) => _abilities.ContainsKey(type);

        private IAbility AddAbility(AbilityType type)
        {
            var prefab = Resources.Load<GameObject>(_abilityLinks[type]);
            var parent = _playerEntity.Get<AbilityUserComponent>().ParentTransform;
            var newAblility = GameObject.Instantiate(prefab, parent);

            _diContainer.Inject(newAblility.GetComponent<DeclarativeModel>());

            var ability = newAblility.GetComponent<IAbility>();
            _abilities.Add(type, ability);

            return ability;
        }

        public void OnDeInitGame()
        {
            foreach (var ability in _abilities)
            {
                ability.Value.GetGameObject().SetActive(false);
            }
        }
    }
}
