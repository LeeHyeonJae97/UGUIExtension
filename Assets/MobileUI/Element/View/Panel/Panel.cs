using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MobileUI
{
    public sealed class Panel : UIBehaviour
    {
        private UITween _tween;

        private void Awake()
        {
            _tween = GetComponent<UITween>();
        }

        public void Open(UITweenerKey key, bool directly = false)
        {
            if (_tween != null)
            {
                StartCoroutine(_tween.CoPlay(key, directly));
            }
        }

        public void Open(bool directly = false)
        {
            if (_tween != null)
            {
                StartCoroutine(_tween.CoPlay(UITweenerKey.Open, directly));
            }
        }

        public void Close(bool directly = false)
        {
            if (_tween != null)
            {
                StartCoroutine(_tween.CoPlay(UITweenerKey.Close, directly));
            }
        }
    }
}
