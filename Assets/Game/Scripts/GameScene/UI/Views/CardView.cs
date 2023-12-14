using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AtomicOrientedDesign.Shooter
{
    public class CardView : MonoBehaviour, IDisposable
    {
        [SerializeField] private TMP_Text _name;
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _level;
        [SerializeField] private TMP_Text _description;
        [SerializeField] private Button _button;

        private float _currentSize = 1;

        private Coroutine _coroutine;

        private readonly Vector3 _defaultScale = Vector3.one;

        private const float UPDATE_FREQUENCY = 1f / 75f;
        private const float MAX_SIZE = 1.1f;
        private const float BLINK_SPEED = 3;
        private const float START_SIZE = 1;
        private const string LEVEL = "Level ";

        public event Action<CardView> OnClicked;


        private void Start()
        {
            transform.localScale = _defaultScale;
            _button.onClick.AddListener(OnButtonClicked);
        }

        public void Dispose() => _button.onClick.RemoveListener(OnButtonClicked);

        public void SetName(string name) => _name.text = name;
        public void SetIcon(Sprite sprite) => _icon.sprite = sprite;
        public void SetLevel(string level) => _level.text = LEVEL + level;
        public void SetDescription(string description) => _description.text = description;

        private void OnButtonClicked()
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _coroutine = null;
            }

            transform.localScale = _defaultScale;
            OnClicked?.Invoke(this);
        }

        public void IncreaseSize()
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }

            _coroutine = StartCoroutine(IncreaseSizeProcess());
        }

        public void ReduceSize()
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }

            _coroutine = StartCoroutine(ReduceSizeProcess());
        }

        private IEnumerator IncreaseSizeProcess()
        {
            var pauseTime = new WaitForSecondsRealtime(UPDATE_FREQUENCY);

            while (_currentSize < MAX_SIZE)
            {
                _currentSize += BLINK_SPEED * UPDATE_FREQUENCY;
                transform.localScale = new Vector3(_currentSize, _currentSize, _currentSize);

                if (_currentSize > MAX_SIZE)
                {
                    transform.localScale = new Vector3(MAX_SIZE, MAX_SIZE, MAX_SIZE);
                    _coroutine = null;
                    yield break;
                }

                yield return pauseTime;
            }
        }

        private IEnumerator ReduceSizeProcess()
        {
            var pauseTime = new WaitForSecondsRealtime(UPDATE_FREQUENCY);

            while (_currentSize > START_SIZE)
            {
                _currentSize -= BLINK_SPEED * UPDATE_FREQUENCY;
                transform.localScale = new Vector3(_currentSize, _currentSize, _currentSize);

                if (_currentSize < START_SIZE)
                {
                    transform.localScale = new Vector3(START_SIZE, START_SIZE, START_SIZE);
                    _coroutine = null;
                    yield break;
                }

                yield return pauseTime;
            }
        }
    }
}