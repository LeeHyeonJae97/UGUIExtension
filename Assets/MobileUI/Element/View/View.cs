using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MobileUI
{
    public abstract class View : UIBehaviour
    {
        public bool IsActive { get; private set; }

        [SerializeField] private bool _activeOnViewClosed;
        private UITween[] _tweens;

        protected virtual void Awake()
        {
            // get tweens
            _tweens = GetComponents<UITween>();

            if (_tweens.Length == 0)
            {
                _tweens = null;
            }
        }

        protected virtual void Start()
        {

        }

        protected virtual void OnDestroy()
        {

        }

        public void Open(bool value, bool directly = false, bool kill = true, bool complete = false)
        {
            StartCoroutine(value ? CoOpen(directly, kill, complete) : CoClose(directly, kill, complete));
        }

        protected abstract IEnumerator CoOpen(bool directly, bool kill, bool complete);

        protected abstract IEnumerator CoClose(bool directly, bool kill, bool complete);

        internal IEnumerator CoSetActive(bool value, bool directly, bool kill, bool complete)
        {
            IsActive = value;

            if (_tweens == null)
            {
                gameObject.SetActive(value || _activeOnViewClosed);
            }
            else
            {
                if (kill)
                {
                    StopAllCoroutines();

                    foreach (var tween in _tweens)
                    {
                        tween.Kill(complete);
                    }
                }

                if (value)
                {
                    gameObject.SetActive(true);

                    if (directly)
                    {
                        for (int i = 0; i < _tweens.Length; i++)
                        {
                            StartCoroutine(_tweens[i].CoSetActive(true, directly, kill, complete, _activeOnViewClosed));
                        }
                    }
                    else
                    {
                        Coroutine[] cors = new Coroutine[_tweens.Length];

                        for (int i = 0; i < _tweens.Length; i++)
                        {
                            cors[i] = StartCoroutine(_tweens[i].CoSetActive(true, directly, kill, complete, _activeOnViewClosed));
                        }

                        for (int i = 0; i < cors.Length; i++)
                        {
                            yield return cors[i];
                        }
                    }
                }
                else
                {
                    if (directly)
                    {
                        for (int i = 0; i < _tweens.Length; i++)
                        {
                            StartCoroutine(_tweens[i].CoSetActive(false, directly, kill, complete, _activeOnViewClosed));
                        }

                        gameObject.SetActive(_activeOnViewClosed);
                    }
                    else
                    {
                        Coroutine[] cors = new Coroutine[_tweens.Length];

                        for (int i = 0; i < _tweens.Length; i++)
                        {
                            cors[i] = StartCoroutine(_tweens[i].CoSetActive(false, directly, kill, complete, _activeOnViewClosed));
                        }

                        for (int i = 0; i < cors.Length; i++)
                        {
                            yield return cors[i];
                        }

                        gameObject.SetActive(_activeOnViewClosed);
                    }
                }
            }
        }
    }
}
