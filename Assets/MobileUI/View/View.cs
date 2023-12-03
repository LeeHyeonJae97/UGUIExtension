using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UGUIExtension
{
    public abstract class View : UIBehaviour
    {
        public bool IsActive { get; private set; }

        [SerializeField] private bool _activeOnViewClosed;
        internal Canvas canvas;
        protected IViewData _data;
        private UITween _tween;

        /// <summary>
        /// update contents with the data passed
        /// </summary>
        /// <param name="data"></param>
        public abstract void UpdateView(IViewData data);

        protected abstract IEnumerator CoOpen(bool directly, bool kill, bool complete);

        protected abstract IEnumerator CoClose(bool directly, bool kill, bool complete);

        protected virtual void Awake()
        {
            canvas = GetComponent<Canvas>();
            _tween = GetComponent<UITween>();
        }

        protected virtual void Start()
        {

        }

        protected virtual void OnDestroy()
        {

        }

        internal IEnumerator CoSetActive(bool value, bool directly, bool kill, bool complete)
        {
            IsActive = value;

            if (_tween == null)
            {
                gameObject.SetActive(value || _activeOnViewClosed);
            }
            else
            {
                if (kill)
                {
                    StopAllCoroutines();
                    _tween.Kill(complete);
                }

                if (value)
                {
                    gameObject.SetActive(true);

                    if (directly)
                    {
                        StartCoroutine(_tween.CoPlay(UITweenerKey.Open, true));
                    }
                    else
                    {
                        yield return StartCoroutine(_tween.CoPlay(UITweenerKey.Open, false));
                    }
                }
                else
                {
                    if (directly)
                    {
                        StartCoroutine(_tween.CoPlay(UITweenerKey.Close, true));
                    }
                    else
                    {
                        yield return StartCoroutine(_tween.CoPlay(UITweenerKey.Close, false));
                    }

                    gameObject.SetActive(_activeOnViewClosed);
                }
            }
        }

        /// <summary>
        /// open
        /// </summary>
        /// <param name="directly"></param>
        /// <param name="kill"></param>
        /// <param name="complete"></param>
        public void Open(bool directly = false, bool kill = true, bool complete = false)
        {
            StartCoroutine(CoOpen(directly, kill, complete));
        }

        /// <summary>
        /// update contents with the data passed and open
        /// </summary>
        /// <param name="data"></param>
        /// <param name="directly"></param>
        /// <param name="kill"></param>
        /// <param name="complete"></param>
        public void Open(IViewData data, bool directly = false, bool kill = true, bool complete = false)
        {
            UpdateView(data);

            StartCoroutine(CoOpen(directly, kill, complete));
        }

        /// <summary>
        /// close
        /// </summary>
        /// <param name="directly"></param>
        /// <param name="kill"></param>
        /// <param name="complete"></param>
        public void Close(bool directly = false, bool kill = true, bool complete = false)
        {
            StartCoroutine(CoClose(directly, kill, complete));
        }

        /// <summary>
        /// open or close
        /// </summary>
        /// <param name="value">if ture, open view or close</param>
        /// <param name="directly"></param>
        /// <param name="kill"></param>
        /// <param name="complete"></param>
        public void SetActive(bool value, bool directly = false, bool kill = true, bool complete = false)
        {
            StartCoroutine(value ? CoOpen(directly, kill, complete) : CoClose(directly, kill, complete));
        }

        /// <summary>
        /// called right before opening
        /// </summary>
        protected virtual void OnBeforeOpened()
        {

        }

        /// <summary>
        /// called right after opening
        /// </summary>
        protected virtual void OnOpened()
        {

        }

        /// <summary>
        /// called right before closing
        /// </summary>
        protected virtual void OnBeforeClosed()
        {

        }

        /// <summary>
        /// called right after closing
        /// </summary>
        protected virtual void OnClosed()
        {

        }
    }
}
