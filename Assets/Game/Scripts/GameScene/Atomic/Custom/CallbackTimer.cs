using System;
using Atomic;
using Declarative;

namespace AtomicOrientedDesign.Shooter
{
    public class CallbackTimer : IUpdate, IEnable, IDisable
    {
        public event Action OnTimeIsOver
        {
            add { _onTimeIsOver.Subscribe(value); }
            remove { _onTimeIsOver.Unsubscribe(value); }
        }

        private readonly AtomicEvent _playTimer = new();
        private readonly AtomicEvent _stopTimer = new();
        private readonly AtomicEvent _resetTimer = new();
        private readonly AtomicEvent<float> _setCallbackTime = new();
        private readonly AtomicEvent _onTimeIsOver = new();

        private float _callbackTime;
        private float _currentTime;
        private bool _isStoped;

        public CallbackTimer()
        {
        }

        public CallbackTimer(AtomicEvent onTimeIsOver)
        {
            _onTimeIsOver = onTimeIsOver;
        }

        public CallbackTimer(AtomicEvent onTimeIsOver, float callbackTime)
        {
            _onTimeIsOver = onTimeIsOver;
            _callbackTime = callbackTime;
        }

        public CallbackTimer(
            AtomicEvent playTimer,
            AtomicEvent stopTimer,
            AtomicEvent resetTimer,
            AtomicEvent<float> setCallbackTime,
            AtomicEvent onTimeIsOver)
        {
            _playTimer = playTimer;
            _stopTimer = stopTimer;
            _resetTimer = resetTimer;
            _setCallbackTime = setCallbackTime;
            _onTimeIsOver = onTimeIsOver;
        }


        public void OnDisable()
        {
            _playTimer.Subscribe(Play);
            _stopTimer.Subscribe(Stop);
            _resetTimer.Subscribe(Reset);
            _setCallbackTime.Subscribe(SetCallbackTime);
        }

        public void OnEnable()
        {
            _playTimer.Unsubscribe(Play);
            _stopTimer.Unsubscribe(Stop);
            _resetTimer.Unsubscribe(Reset);
            _setCallbackTime.Unsubscribe(SetCallbackTime);
        }

        public void Update(float deltaTime)
        {
            if (_isStoped) return;

            if (_currentTime >= _callbackTime)
            {
                Reset();
                _onTimeIsOver.Invoke();
            }
            else
            {
                _currentTime += deltaTime;
            }
        }

        public void Play() => _isStoped = false;
        public void Stop() => _isStoped = true;
        public void Reset() => _currentTime = 0;
        public void SetCallbackTime(float callbackTime) => _callbackTime = callbackTime;
    }
}