using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MobileUI
{
    [RequireComponent(typeof(RectTransform))]
    public class UITweenRotation : UITween
    {
        [SerializeField] private Vector3 _inactive;
        [SerializeField] private Vector3 _active;
        [SerializeField] private RotateMode _rotateMode;
        [SerializeField] private RectTransform _rectTr;

        protected override void Reset()
        {
            base.Reset();

            _rotateMode = RotateMode.FastBeyond360;
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

                _rectTr.localEulerAngles = value ? _active : _inactive;
            }
            else
            {
                if (value)
                {
                    if (!_activeOnTweenInactivated)
                    {
                        gameObject.SetActive(true);
                    }

                    _tweener = _rectTr.DORotate(_active, _duration, _rotateMode).SetEase(_ease);

                    yield return _tweener.WaitForCompletion();
                }
                else
                {
                    _tweener = _rectTr.DORotate(_inactive, _duration, _rotateMode).SetEase(_ease);

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
