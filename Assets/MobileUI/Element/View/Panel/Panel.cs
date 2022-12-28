using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MobileUI
{
    public sealed class Panel : UIBehaviour
    {
        [SerializeField] UITweenerKey _key;
        private UITween _tween;

        private void Awake()
        {
            _tween = GetComponent<UITween>();
        }

        public void Open(UITweenerKey key, bool directly = false)
        {
            StartCoroutine(_tween.CoPlay(key, directly));
        }

        public void Open(bool directly = false)
        {
            StartCoroutine(_tween.CoPlay(_key, directly));
        }

        public void Close(bool directly = false)
        {
            StartCoroutine(_tween.CoPlay(UITweenerKey.Close, directly));
        }
    }
}
