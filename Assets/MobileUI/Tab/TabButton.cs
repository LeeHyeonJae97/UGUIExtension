using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MobileUI
{
    public class TabButton : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
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
        internal TabGroup TabGroup => _tabGroup;

        [SerializeField] private bool _interactable = true;
        [SerializeField] private SelectableImage _image;
        [SerializeField] private TabGroup _tabGroup;

        public UnityEvent<bool> onStateChanged;
        public UnityEvent onSelected;
        public UnityEvent onDeselected;

        private void OnValidate()
        {
            if (_image != null)
            {
                _image.interactable = _interactable;
            }
        }

        public void Select()
        {
            if (!_interactable) return;

            _tabGroup.Selected?.OnStateChanged(false, true);
            _tabGroup.Selected = this;
            _tabGroup.Selected.OnStateChanged(true, true);
        }

        internal void OnStateChanged(bool value, bool invokeEvent)
        {
            if (!_interactable) return;

            if (value)
            {
                OnSelected();

                if (invokeEvent)
                {
                    onSelected.Invoke();
                    onStateChanged.Invoke(true);
                }
            }
            else
            {
                OnDeselected();

                if (invokeEvent)
                {
                    onDeselected.Invoke();
                    onStateChanged.Invoke(false);
                }
            }
        }

        protected virtual void OnSelected()
        {
            if (!_interactable) return;

            if (_image != null)
            {
                _image.selected = true;
            }
        }

        protected virtual void OnDeselected()
        {
            if (!_interactable) return;

            if (_image != null)
            {
                _image.selected = false;
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!_interactable) return;

            Select();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!_interactable) return;

            if (_image != null)
            {
                _image.color = _image.PressedColor;
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!_interactable) return;

            if (_image != null)
            {
                _image.color = _image.NormalColor;
            }
        }
    }
}