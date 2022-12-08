using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MobileUI
{
    [RequireComponent(typeof(Canvas))]
    public abstract class Window : View
    {
        protected static Dictionary<string, Window> _windowDic = new Dictionary<string, Window>();
        protected static Stack<Window> _activatedStack = new Stack<Window>();

        public static T Get<T>() where T : Window
        {
            return _windowDic.TryGetValue(typeof(T).ToString(), out var window) ? (T)window : null;
        }

        // TODO :
        // need to rename
        public static void Pop(bool directly = false, bool kill = true, bool complete = false)
        {
            _activatedStack.Peek().Open(false, directly, kill, complete);
        }

        public static void Clear(bool directly = false, bool kill = true, bool complete = false)
        {
            // inactivate all of the windows
            while (_activatedStack.Count > 0)
            {
                _activatedStack.Pop().Open(false, directly, kill, complete);
            }
        }

        public int SortingOrder => _canvas.sortingOrder;

        private Canvas _canvas;

        protected override void Awake()
        {
            base.Awake();

            _canvas = GetComponent<Canvas>();

            // keep reference statically
            _windowDic.Add(GetType().ToString(), this);

            // inactivate window at first
            StartCoroutine(CoSetActive(false, true, true, false));
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
