using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace MobileUI
{
    public class Button : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
    {
        public bool interactable
        {
            get { return _interactable; }

            set
            {
                _interactable = value;


                if (_image != null)
                {
                    _image.interactable = value;
                }
            }
        }

        [SerializeField] private bool _interactable = true;
        [SerializeField] private InteractableImage _image;
        [SerializeField] private float _longClickInvokeTime;
        private bool _down;
        private float _downDuration;

        public UnityEvent onClick;
        public UnityEvent onLongDown;
        public UnityEvent onLongUp;

        private void OnValidate()
        {
            if (_image != null)
            {
                _image.interactable = _interactable;
            }
        }

        private void Update()
        {
            if (_down)
            {
                _downDuration += Time.deltaTime;

                if (_downDuration > _longClickInvokeTime)
                {
                    _down = false;

                    onLongDown.Invoke();
                }
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!_interactable) return;

            if (_downDuration < _longClickInvokeTime)
            {
                onClick.Invoke();
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!_interactable) return;

            if (_image != null)
            {
                _image.color = _image.PressedColor;
            }

            _down = true;
            _downDuration = 0;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!_interactable) return;

            if (_image != null)
            {
                _image.color = _image.NormalColor;
            }

            _down = false;

            if (_downDuration >= _longClickInvokeTime)
            {
                onLongUp.Invoke();
            }
        }

        public void Debug(string log)
        {
            UnityEngine.Debug.Log(log);
        }
    }
}
