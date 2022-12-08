using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MobileUI
{
    [RequireComponent(typeof(View))]
    public abstract class UITween : MonoBehaviour
    {
        public float Duration => _duration;

        [SerializeField] protected Ease _ease;
        [SerializeField] protected float _duration;
        protected Tweener _tweener;

        protected virtual void Reset()
        {
            _ease = Ease.Unset;
            _duration = 1f;
        }

        internal abstract IEnumerator CoSetActive(bool value, bool directly, bool kill, bool complete, bool _activeOnViewClosed);

        internal void Kill(bool complete)
        {
            if (_tweener != null && _tweener.IsActive())
            {
                _tweener.Kill(complete);
            }
        }
    }
}
