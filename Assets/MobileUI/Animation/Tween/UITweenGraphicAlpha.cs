using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MobileUI
{
    [RequireComponent(typeof(Graphic))]
    public class UITweenGraphicAlpha : UITween
    {
        [SerializeField] private float _inactive;
        [SerializeField] private float _active;
        [SerializeField] private Graphic _graphic;

        protected override void Reset()
        {
            base.Reset();

            _graphic = GetComponent<Graphic>();
        }

        internal override IEnumerator CoSetActive(bool value, bool directly, bool kill, bool complete)
        {
            if (directly)
            {
                if (!_activeOnTweenInactivated)
                {
                    gameObject.SetActive(value);
                }

                var color = _graphic.color;
                color.a = value ? _active : _inactive;
                _graphic.color = color;
            }
            else
            {
                if (value)
                {
                    if (!_activeOnTweenInactivated)
                    {
                        gameObject.SetActive(true);
                    }

                    _tweener = _graphic.DOFade(_active, _duration).SetEase(_ease);

                    yield return _tweener.WaitForCompletion();
                }
                else
                {
                    _tweener = _graphic.DOFade(_inactive, _duration).SetEase(_ease);

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