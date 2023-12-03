using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UGUIExtension
{
    [RequireComponent(typeof(Canvas))]
    public abstract class Window : View
    {
        protected const string SortingLayerName = "Window";

        protected int SortingOrder { get { return _actives.Count; } }

        protected static Dictionary<string, Window> _windows = new Dictionary<string, Window>();
        protected static Stack<Window> _actives = new Stack<Window>();

        public static T Get<T>() where T : Window
        {
            return _windows.TryGetValue(typeof(T).ToString(), out var window) ? (T)window : null;
        }

        public static bool Pop(bool directly = false, bool kill = true, bool complete = false)
        {
            bool success = _actives.TryPop(out var window);

            if (success)
            {
                window.Close(directly, kill, complete);
            }
            return success;
        }

        public static void Clear(bool directly = false, bool kill = true, bool complete = false)
        {
            while (_actives.Count > 0)
            {
                _actives.Pop().Close(directly, kill, complete);
            }
        }

        protected override void Awake()
        {
            base.Awake();

            _windows.Add(GetType().ToString(), this);

            StartCoroutine(CoSetActive(false, true, true, false));
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            _windows.Remove(GetType().ToString());
        }
    }
}
