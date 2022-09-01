using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class WindowTweenCanvasGroupAlpha : WindowTween
    {
        private CanvasGroup Group
        {
            get
            {
                if (_group == null)
                {
                    _group = GetComponent<CanvasGroup>();
                }
                return _group;
            }
        }

        [SerializeField] private float _inactive;
        [SerializeField] private float _active;
        private CanvasGroup _group;

        public override IEnumerator CoSetActive(bool value, bool directly)
        {
            if (directly)
            {
                if (!_activeOnTweenInactivated)
                {
                    gameObject.SetActive(value);
                }

                Group.alpha = value ? _active : _inactive;
            }
            else
            {
                if (_tweener != null && _tweener.IsActive()) _tweener.Kill();

                if (value)
                {
                    if (!_activeOnTweenInactivated)
                    {
                        gameObject.SetActive(true);
                    }

                    _tweener = Group.DOFade(_active, _duration);

                    yield return _tweener.WaitForCompletion();
                }
                else
                {
                    _tweener = Group.DOFade(_inactive, _duration);

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