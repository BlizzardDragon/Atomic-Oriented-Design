using System;
using Atomic;
using Declarative;
using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    [Serializable]
    public class RepeatingSpawnSection
    {
        public GameObject Prefab;
        public AtomicVariable<int> SpawnNubmer = new();
        public AtomicEvent<GameObject> OnSpawn = new();
        public AtomicEvent OnTimeIsOver = new();
        public CallbackTimer CallbackTimer;


        [Construct]
        public void Construct()
        {
            SpawnNubmer.Value = 1;
            CallbackTimer = new(OnTimeIsOver);

            OnTimeIsOver.Subscribe(() =>
            {
                for (int i = 0; i < SpawnNubmer.Value; i++)
                {
                    var newObject = GameObject.Instantiate(Prefab);
                    OnSpawn?.Invoke(newObject);
                }
            });
        }
    }
}