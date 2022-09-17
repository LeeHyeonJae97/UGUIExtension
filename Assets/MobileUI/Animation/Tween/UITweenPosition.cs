using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MobileUI
{
    [RequireComponent(typeof(RectTransform))]
    public class UITweenPosition : UITween
    {
        [SerializeField] private Vector2 _inactive;
        [SerializeField] private Vector2 _active;
        [SerializeField] private RectTransform _rectTr;

        protected override void Reset()
        {
            base.Reset();

            _rectTr = GetComponent<RectTransform>();
        }

        internal override IEnumerator CoSetActive(bool value, bool directly, bool kill, bool complete)
        {
            if (directly)
            {
                if (!_activeOnTweenInactivated)
                {
                    gameObject.SetActive(value);
                }

                _rectTr.anchoredPosition = value ? _active : _inactive;
            }
            else
            {
                if (value)
                {
                    if (!_activeOnTweenInactivated)
                    {
                        gameObject.SetActive(true);
                    }

                    _tweener = _rectTr.DOAnchorPos(_active, _duration).SetEase(_ease);

                    yield return _tweener.WaitForCompletion();
                }
                else
                {
                    _tweener = _rectTr.DOAnchorPos(_inactive, _duration).SetEase(_ease);

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
