using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Graphic))]
    public class UITweenGraphicAlpha : UITween
    {
        private Graphic Graphic
        {
            get
            {
                if (_graphic == null)
                {
                    _graphic = GetComponent<Graphic>();
                }
                return _graphic;
            }
        }

        [SerializeField] private float _inactive;
        [SerializeField] private float _active;
        private Graphic _graphic;

        internal override IEnumerator CoSetActive(bool value, bool directly, bool kill, bool complete)
        {
            if (directly)
            {
                if (!_activeOnTweenInactivated)
                {
                    gameObject.SetActive(value);
                }

                var color = Graphic.color;
                color.a = value ? _active : _inactive;
                Graphic.color = color;
            }
            else
            {
                if (value)
                {
                    if (!_activeOnTweenInactivated)
                    {
                        gameObject.SetActive(true);
                    }

                    _tweener = Graphic.DOFade(_active, _duration);

                    yield return _tweener.WaitForCompletion();
                }
                else
                {
                    _tweener = Graphic.DOFade(_inactive, _duration);

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
