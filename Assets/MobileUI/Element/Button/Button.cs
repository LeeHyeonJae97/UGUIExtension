using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace MobileUI
{
    [RequireComponent(typeof(InteractableImage))]
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

        [SerializeField] private bool _interactable;
        [SerializeField] private InteractableImage _image;
        [SerializeField] private float _longClickInvokeTime;
        private bool _down;
        private float _downDuration;

        [HideInInspector] public UnityEvent onClick { get; private set; }
        [HideInInspector] public UnityEvent onLongDown { get; private set; }
        [HideInInspector] public UnityEvent onLongUp { get; private set; }

        private void Reset()
        {
            _interactable = true;
            _image = GetComponent<InteractableImage>();
            _longClickInvokeTime = 1f;
        }

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
                _image.pressed = true;
            }

            _down = true;
            _downDuration = 0;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!_interactable) return;

            if (_image != null)
            {
                _image.pressed = false;
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
