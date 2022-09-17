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

        [SerializeField] private bool _interactable;
        [SerializeField] private bool _isOn;
        [SerializeField] private InteractableImage _backgroundImage;
        [SerializeField] private InteractableImage _handleImage;
        [SerializeField] private RectTransform _handle;
        [SerializeField] private float _onHandlePosition;
        [SerializeField] private float _offHandlePosition;

        public UnityEvent<bool> onStateChanged;

        private void Reset()
        {
            _interactable = true;
            _isOn = true;
        }

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
                _handle.anchoredPosition = new Vector2(value ? _onHandlePosition : _offHandlePosition, _handle.anchoredPosition.y);
            }
            else
            {
                _interactable = false;
                _handle.DOAnchorPosX(value ? _onHandlePosition : _offHandlePosition, .5f).onComplete += () => _interactable = true;
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
                _backgroundImage.pressed = true;
            }
            if (_handleImage != null)
            {
                _handleImage.pressed = true;
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!_interactable) return;

            if (_backgroundImage != null)
            {
                _backgroundImage.pressed = false;
            }
            if (_handleImage != null)
            {
                _handleImage.pressed = false;
            }
        }
    }
}