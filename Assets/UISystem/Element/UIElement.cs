using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public abstract class UIElement : UIBehaviour
    {
        public bool IsActive => gameObject.activeInHierarchy;

        private UITween[] _tweens;

        protected virtual void Awake()
        {
            // get tweens
            _tweens = GetComponentsInChildren<UITween>();

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
            // CONTINUE :
            // if not intend to kill, skip when coroutine is active
            //
            if (kill)
            {
                Kill(complete);
            }

            StartCoroutine(value ? CoOpen(directly, kill, complete) : CoClose(directly, kill, complete));
        }

        protected abstract IEnumerator CoOpen(bool directly, bool kill, bool complete);

        protected abstract IEnumerator CoClose(bool directly, bool kill, bool complete);

        internal IEnumerator CoSetActive(bool value, bool directly, bool kill, bool complete)
        {
            if (_tweens == null)
            {
                gameObject.SetActive(value);
            }
            else
            {
                if (value)
                {
                    gameObject.SetActive(true);

                    if (directly)
                    {
                        for (int i = 0; i < _tweens.Length; i++)
                        {
                            StartCoroutine(_tweens[i].CoSetActive(_tweens[i].ActiveOnCanvasOpened, directly, kill, complete));
                        }
                    }
                    else
                    {
                        Coroutine[] cors = new Coroutine[_tweens.Length];

                        for (int i = 0; i < _tweens.Length; i++)
                        {
                            cors[i] = StartCoroutine(_tweens[i].CoSetActive(true, directly, kill, complete));
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
                            StartCoroutine(_tweens[i].CoSetActive(false, directly, kill, complete));
                        }

                        gameObject.SetActive(false);
                    }
                    else
                    {
                        Coroutine[] cors = new Coroutine[_tweens.Length];

                        for (int i = 0; i < _tweens.Length; i++)
                        {
                            cors[i] = StartCoroutine(_tweens[i].CoSetActive(false, directly, kill, complete));
                        }

                        for (int i = 0; i < cors.Length; i++)
                        {
                            yield return cors[i];
                        }

                        gameObject.SetActive(false);
                    }
                }
            }
        }

        protected void Kill(bool complete)
        {
            StopAllCoroutines();

            if (_tweens != null)
            {
                foreach (var tween in _tweens)
                {
                    tween.Kill(complete);
                }
            }
        }
    }
}
