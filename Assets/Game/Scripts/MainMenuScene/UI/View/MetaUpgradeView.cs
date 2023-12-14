using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AtomicOrientedDesign.Shooter
{
    public class MetaUpgradeView : MonoBehaviour
    {
        public Button Button => _button;

        [SerializeField] private TMP_Text _name;
        [SerializeField] private Image _icon;
        [SerializeField] private Button _button;
        [SerializeField] private Transform _toggleParant;
        [SerializeField] private ImageToggleView _prefab;

        private List<ImageToggleView> _toggleViews = new();

        public event Action<MetaUpgradeView> OnPointerEnter;
        public event Action<MetaUpgradeView> OnPointerExit;


        public void Setup(string name, int maxLevel, Sprite sprite)
        {
            _name.text = name;
            _icon.sprite = sprite;

            if (_toggleViews.Count > 0)
            {
                foreach (var toggleView in _toggleViews)
                {
                    Destroy(toggleView.gameObject);
                }

                _toggleViews.Clear();
            }

            for (int i = 0; i < maxLevel; i++)
            {
                _toggleViews.Add(Instantiate(_prefab, _toggleParant));
            }
        }

        public void SetLevel(int level)
        {
            if (level > _toggleViews.Count)
            {
                throw new ArgumentOutOfRangeException($"The level {level} is wrong!");
            }

            for (int i = 0; i < _toggleViews.Count; i++)
            {
                _toggleViews[i].IsActive = i < level;
            }
        }

        public void PointerEnterEvent() => OnPointerEnter?.Invoke(this);
        public void PointerExitEvent() => OnPointerExit?.Invoke(this);
    }
}
