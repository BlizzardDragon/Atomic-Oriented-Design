using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    public abstract class LoadingTask : ScriptableObject
    {
        public abstract UniTask<Result> Do();
        
        public struct Result
        {
            public bool Success;
            public string Error;
        }
    }
}