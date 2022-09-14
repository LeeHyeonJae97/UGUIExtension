using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MobileUI
{
    [RequireComponent(typeof(RectTransform))]
    public class UITweenSize : UITween
    {
        private RectTransform RectTr
        {
            get
            {
                if (_rectTr == null)
                {
                    _rectTr = GetComponent<RectTransform>();
                }
                return _rectTr;
            }
        }

        [SerializeField] private Vector2 _inactive;
        [SerializeField] private Vector2 _active;
        private RectTransform _rectTr;

        internal override IEnumerator CoSetActive(bool value, bool directly, bool kill, bool complete)
        {
            if (directly)
            {
                if (!_activeOnTweenInactivated)
                {
                    gameObject.SetActive(value);
                }

                RectTr.sizeDelta = value ? _active : _inactive;
            }
            else
            {
                if (value)
                {
                    if (!_activeOnTweenInactivated)
                    {
                        gameObject.SetActive(true);
                    }

                    _tweener = RectTr.DOSizeDelta(_active, _duration);

                    yield return _tweener.WaitForCompletion();
                }
                else
                {
                    _tweener = RectTr.DOSizeDelta(_inactive, _duration);

                    yield return _tweener.WaitForCompletion();

                    if (!_activeOnTweenInactivated)
                    {
                        gameObject.SetActive(false);
                    }
                }
            }
        }
    }
}
