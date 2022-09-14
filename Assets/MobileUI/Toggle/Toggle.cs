using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MobileUI
{
    public class Toggle : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
    {
        public bool interactable
        {
            get { return _interactable; }

            set
            {
                _interactable = value;


                if (_backgroundImage != null)
                {
                    _backgroundImage.interactable = value;
                }
                if (_handleImage != null)
                {
                    _handleImage.interactable = value;
                }
            }
        }
        public bool IsOn => _isOn;

        [SerializeField] private bool _interactable = true;
        [SerializeField] private bool _isOn;
        [SerializeField] private InteractableImage _backgroundImage;
        [SerializeField] private InteractableImage _handleImage;
        [SerializeField] private RectTransform _handle;
        [SerializeField] private float _on;
        [SerializeField] private float _off;

        public UnityEvent<bool> onStateChanged;

        private void OnValidate()
        {
            if (_backgroundImage != null)
            {
                _backgroundImage.interactable = _interactable;
            }
            if (_handleImage != null)
            {
                _handleImage.interactable = _interactable;
            }
        }

        private void Awake()
        {
            if (!_interactable) return;

            Switch(_isOn, true, false);
        }

        public void Switch(bool directly, bool invokeEvent)
        {
            if (!_interactable) return;

            Switch(!_isOn, directly, invokeEvent);
        }

        public void Switch(bool value, bool directly, bool invokeEvent)
        {
            if (!_interactable) return;

            _isOn = value;

            if (directly)
            {
                _handle.anchoredPosition = new Vector2(value ? _on : _off, _handle.anchoredPosition.y);
            }
            else
            {
                _interactable = false;
                _handle.DOAnchorPosX(value ? _on : _off, .5f).onComplete += () => _interactable = true;
            }

            if (invokeEvent)
            {
                onStateChanged.Invoke(value);
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!_interactable) return;

            Switch(false, true);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!_interactable) return;

            if (_backgroundImage != null)
            {
                _backgroundImage.color = _backgroundImage.PressedColor;
            }
            if (_handleImage != null)
            {
                _handleImage.color = _handleImage.PressedColor;
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!_interactable) return;

            if (_backgroundImage != null)
            {
                _backgroundImage.color = _backgroundImage.NormalColor;
            }
            if (_handleImage != null)
            {
                _handleImage.color = _handleImage.NormalColor;
            }
        }
    }
}