using System.Collections.Generic;
using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    public class TimeScaleManager
    {
        private Stack<string> _stack = new();


        public void TryPlayTime(string key)
        {
            if (_stack.Peek() == key)
            {
                _stack.Pop();
            }

            if (_stack.Count <= 0)
            {
                PlayTime();
            }
        }

        public void StopTime(string key)
        {
            _stack.Push(key);
            Time.timeScale = 0;
        }

        public void PlayTime() => Time.timeScale = 1;
    }
}