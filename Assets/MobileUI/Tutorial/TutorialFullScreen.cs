using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MobileUI
{
    public class TutorialFullScreen : FullScreen
    {
        [SerializeField] private GameObject _highlighter;
        private UnityEngine.UI.Button _targetButton;

        public void Set(UnityEngine.UI.Button targetButton)
        {
            _targetButton = Instantiate(targetButton, transform);

            _highlighter.transform.position = _targetButton.transform.position;
            _highlighter.transform.SetAsLastSibling();
        }

        public void Set(UnityEngine.UI.Button targetButton, UnityAction onClick, bool overwrite = true)
        {
            _targetButton = Instantiate(targetButton, transform);
           
            if (overwrite)
            {
                _targetButton.onClick.RemoveAllListeners();
            }
            _targetButton.onClick.AddListener(onClick);

            _highlighter.transform.position = _targetButton.transform.position;
            _highlighter.transform.SetAsLastSibling();
        }

        protected override void OnBeforeOpened()
        {
            if (_targetButton == null)
            {
                Debug.LogWarning("There's no button set for tutorial");
                Close();
            }
        }

        protected override void OnClosed()
        {
            if (_targetButton != null)
            {
                Destroy(_targetButton);
            }
        }
    }
}
