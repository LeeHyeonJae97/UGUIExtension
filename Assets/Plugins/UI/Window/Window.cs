using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(Canvas))]
    public abstract class Window : MonoBehaviour
    {
        #region static
        protected static Dictionary<string, Window> _windowDic = new Dictionary<string, Window>();
        protected static Stack<Window> _activatedStack = new Stack<Window>();

        public static T Get<T>() where T : Window
        {
            return _windowDic.TryGetValue(typeof(T).ToString(), out var window) ? (T)window : null;
        }

        // TODO :
        // need to rename
        public static void Pop()
        {
            _activatedStack.Peek().CoOpen(false);
        }

        public static void Clear()
        {
            // inactivate all of the windows
            while (_activatedStack.Count > 0)
            {
                _activatedStack.Pop().CoSetActive(false, true);
            }
        }
        #endregion

        public bool IsActive => _canvas.enabled;

        private Canvas _canvas;
        private WindowTween[] _tweens;

        protected virtual void Awake()
        {
            // get canvas
            _canvas = GetComponent<Canvas>();

            // get tweens
            _tweens = GetComponentsInChildren<WindowTween>();

            if (_tweens.Length == 0)
            {
                _tweens = null;
            }

            // keep reference statically
            _windowDic.Add(GetType().ToString(), this);

            StartCoroutine(CoSetActive(false, true));
        }

        protected virtual void Start()
        {

        }

        protected virtual void OnDestroy()
        {
            // remove reference
            _windowDic.Remove(GetType().ToString());
        }

        public void Open(bool value, bool directly = false)
        {
            StartCoroutine(CoOpen(value, directly));
        }

        public IEnumerator CoOpen(bool value, bool directly = false)
        {
            if (value)
            {
                yield return StartCoroutine(CoOpen(directly));
            }
            else
            {
                if (_activatedStack.Peek() == this)
                {
                    yield return StartCoroutine(CoClose(directly));
                }
            }
        }

        protected virtual IEnumerator CoOpen(bool directly)
        {
            yield return StartCoroutine(OpenInternal(directly));
        }

        protected virtual IEnumerator CoClose(bool directly)
        {
            yield return StartCoroutine(CloseInternal(directly));
        }

        internal void SetActive(bool value, bool directly)
        {
            StartCoroutine(CoSetActive(value, directly));
        }

        internal IEnumerator CoSetActive(bool value, bool directly)
        {
            if (_tweens == null)
            {
                _canvas.enabled = value;
            }
            else
            {
                if (value)
                {
                    _canvas.enabled = true;

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

                        _canvas.enabled = false;
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

                        _canvas.enabled = false;
                    }
                }
            }
        }

        protected abstract IEnumerator OpenInternal(bool directly);
        protected abstract IEnumerator CloseInternal(bool directly);
    }
}
