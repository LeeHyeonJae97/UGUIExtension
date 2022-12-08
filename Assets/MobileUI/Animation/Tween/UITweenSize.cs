using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MobileUI
{
    [RequireComponent(typeof(RectTransform))]
    public class UITweenSize : UITween
    {
        [SerializeField] private Vector2 _inactive;
        [SerializeField] private Vector2 _active;
        [SerializeField] private RectTransform _rectTr;

        protected override void Reset()
        {
            base.Reset();

            _rectTr = GetComponent<RectTransform>();
        }

        internal override IEnumerator CoSetActive(bool value, bool directly, bool kill, bool complete, bool _activeOnViewClosed)
        {
            if (directly)
            {
                gameObject.SetActive(value || _activeOnViewClosed);

                _rectTr.sizeDelta = value ? _active : _inactive;
            }
            else
            {
                if (value)
                {
                    gameObject.SetActive(true);

                    _tweener = _rectTr.DOSizeDelta(_active, _duration).SetEase(_ease);

                    yield return _tweener.WaitForCompletion();
                }
                else
                {
                    _tweener = _rectTr.DOSizeDelta(_inactive, _duration).SetEase(_ease);

                    yield return _tweener.WaitForCompletion();

                    gameObject.SetActive(_activeOnViewClosed);
                }
            }
        }
    }
}
