using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MobileUI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UITweenCanvasGroupAlpha : UITween
    {
        [SerializeField] private float _inactive;
        [SerializeField] private float _active;
        [SerializeField] private CanvasGroup _group;

        protected override void Reset()
        {
            base.Reset();

            _group = GetComponent<CanvasGroup>();
        }

        internal override IEnumerator CoSetActive(bool value, bool directly, bool kill, bool complete)
        {
            if (directly)
            {
                if (!_activeOnTweenInactivated)
                {
                    gameObject.SetActive(value);
                }

                _group.alpha = value ? _active : _inactive;
            }
            else
            {
                if (value)
                {
                    if (!_activeOnTweenInactivated)
                    {
                        gameObject.SetActive(true);
                    }

                    _tweener = _group.DOFade(_active, _duration).SetEase(_ease);

                    yield return _tweener.WaitForCompletion();
                }
                else
                {
                    _tweener = _group.DOFade(_inactive, _duration).SetEase(_ease);

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
