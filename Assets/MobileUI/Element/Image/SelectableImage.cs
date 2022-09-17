using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MobileUI
{
    public class SelectableImage : Image, ISelectable
    {
        public bool interactable
        {
            get { return _interactable; }

            set
            {
                _interactable = value;

                color = _interactable ? NormalColor : DisabledColor;
            }
        }
        public bool selected
        {
            get { return _selected; }

            set
            {
                _selected = value;

                color = NormalColor;
            }
        }
        public bool pressed
        {
            get { return _pressed; }

            set
            {
                _pressed = value;

                color = _pressed ? PressedColor : NormalColor;
            }
        }
        private Color NormalColor => _normalColor * (_selected ? _selectedColor : _deselectedColor);
        private Color PressedColor => _pressedColor * (_selected ? _selectedColor : _deselectedColor);
        private Color DisabledColor => _disabledColor * _deselectedColor;

        [SerializeField] private Color _selectedColor = Color.white;
        [SerializeField] private Color _deselectedColor = Color.white;
        [SerializeField] private Color _normalColor = Color.white;
        [SerializeField] private Color _pressedColor = Color.white;
        [SerializeField] private Color _disabledColor = Color.white;
        private bool _interactable;
        private bool _selected;
        private bool _pressed;
    }
}