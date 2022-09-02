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

        public void Open(bool value, bool directly = false)
        {
            StartCoroutine(CoOpen(value, directly));
        }

        public IEnumerator CoOpen(bool value, bool directly = false)
        {
            yield return StartCoroutine(value ? CoOpen(directly) : CoClose(directly));
        }

        protected abstract IEnumerator CoOpen(bool directly);

        protected abstract IEnumerator CoClose(bool directly);

        internal IEnumerator CoSetActive(bool value, bool directly)
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
                            StartCoroutine(_tweens[i].CoSetActive(_tweens[i].ActiveOnCanvasOpened, directly));
                        }
                    }
                    else
                    {
                        Coroutine[] cors = new Coroutine[_tweens.Length];

                        for (int i = 0; i < _tweens.Length; i++)
                        {
                            cors[i] = StartCoroutine(_tweens[i].CoSetActive(true, directly));
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
                            StartCoroutine(_tweens[i].CoSetActive(false, directly));
                        }

                        gameObject.SetActive(false);
                    }
                    else
                    {
                        Coroutine[] cors = new Coroutine[_tweens.Length];

                        for (int i = 0; i < _tweens.Length; i++)
                        {
                            cors[i] = StartCoroutine(_tweens[i].CoSetActive(false, directly));
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
    }
}
