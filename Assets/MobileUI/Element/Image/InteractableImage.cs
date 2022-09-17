using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MobileUI
{
    public class InteractableImage : Image, IInteractable
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
        public bool pressed
        {
            get { return _pressed; }

            set
            {
                _pressed = value;

                color = _pressed ? PressedColor : NormalColor;
            }
        }
        private Color NormalColor => _baseColor * _normalColor;
        private Color PressedColor => _baseColor * _pressedColor;
        private Color DisabledColor => _baseColor * _disabledColor;

        [SerializeField] private Color _baseColor;
        [SerializeField] private Color _normalColor;
        [SerializeField] private Color _pressedColor;
        [SerializeField] private Color _disabledColor;
        private bool _interactable;
        private bool _pressed;

        protected override void Reset()
        {
            _baseColor = Color.white;
            _normalColor = Color.white;
            _pressedColor = Color.white;
            _disabledColor = Color.white;
        }
    }
}
