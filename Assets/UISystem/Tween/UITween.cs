using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public abstract class UITween : MonoBehaviour
    {
        public bool ActiveOnCanvasOpened => _activeOnCanvasOpened;
        public float Duration => _duration;

        [SerializeField] protected bool _activeOnCanvasOpened;
        [SerializeField] protected bool _activeOnTweenInactivated;
        [SerializeField] protected float _duration;
        protected Tweener _tweener;

        public abstract IEnumerator CoSetActive(bool value, bool directly);
    }
}
