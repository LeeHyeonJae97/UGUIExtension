using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(Canvas))]
    public abstract class Window : UIElement
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

        public int SortingOrder => _canvas.sortingOrder;

        private Canvas _canvas;

        protected override void Awake()
        {
            base.Awake();

            _canvas = GetComponent<Canvas>();

            // keep reference statically
            _windowDic.Add(GetType().ToString(), this);

            // inactivate window at first
            StartCoroutine(CoSetActive(false, true));
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            // remove reference
            _windowDic.Remove(GetType().ToString());
        }

        protected virtual void OnBeforeOpened()
        {

        }

        protected virtual void OnOpened()
        {

        }

        protected virtual void OnBeforeClosed()
        {

        }

        protected virtual void OnClosed()
        {

        }
    }
}
