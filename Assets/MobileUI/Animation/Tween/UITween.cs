using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MobileUI
{
    public abstract class UITween : MonoBehaviour
    {
        public bool ActiveOnCanvasOpened => _activeOnCanvasOpened;
        public float Duration => _duration;

        [SerializeField] protected bool _activeOnCanvasOpened;
        [SerializeField] protected bool _activeOnTweenInactivated;
        [SerializeField] protected Ease _ease;
        [SerializeField] protected float _duration;
        protected Tweener _tweener;

        protected virtual void Reset()
        {
            _activeOnCanvasOpened = true;
            _activeOnTweenInactivated = false;
            _ease = Ease.Linear;
            _duration = 1f;
        }

        internal abstract IEnumerator CoSetActive(bool value, bool directly, bool kill, bool complete);

        internal void Kill(bool complete)
        {
            if (_tweener != null && _tweener.IsActive())
            {
                _tweener.Kill(complete);
            }
        }
    }
}
