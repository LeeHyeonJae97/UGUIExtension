using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UGUIExtension
{
    public abstract class UIEffect : MonoBehaviour
    {
        [SerializeField] protected bool _playOnEnabled;
        [SerializeField] protected float _duration;
        protected Tweener _tweener;

        public abstract void Play();

        protected virtual void Reset()
        {
            _playOnEnabled = true;
            _duration = 1;
        }

        private void OnEnable()
        {
            if (_playOnEnabled)
            {
                Play();
            }
        }
    }
}
