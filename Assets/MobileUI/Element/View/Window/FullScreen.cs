using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MobileUI
{
    public class FullScreen : Window
    {
        private Panel[] _panels;

        protected override void Awake()
        {
            base.Awake();

            _panels = GetComponentsInChildren<Panel>(true);

            if (_panels.Length == 0)
            {
                _panels = null;
            }
        }

        protected override sealed IEnumerator CoOpen(bool directly, bool kill, bool complete)
        {
            // can't open duplicately
            if (_activatedStack.Contains(this)) yield break;

            // can't open over popup window
            if (_activatedStack.Count > 0 && _activatedStack.Peek() is Popup) yield break;

            // push new window to stack
            _activatedStack.Push(this);

            OnBeforeOpened();

            // open or close all panels directly
            if (_panels != null)
            {
                foreach (var panel in _panels)
                {
                    panel.Open(value: panel.ActiveOnWindowOpened, true);
                }
            }

            // open new window
            yield return StartCoroutine(CoSetActive(true, directly, kill, complete));

            OnOpened();

            // close all the windows until fullscreen
            foreach (Window window in _activatedStack)
            {
                if (window.SortingOrder < SortingOrder)
                {
                    StartCoroutine(window.CoSetActive(false, true, kill, complete));
                }

                if (window is FullScreen && window != this) break;
            }
        }

        protected override sealed IEnumerator CoClose(bool directly, bool kill, bool complete)
        {
            // can't close not opened window
            if (!_activatedStack.Contains(this)) yield break;

            // recover closed windows when this window is opened
            foreach (Window window in _activatedStack)
            {
                if (window.SortingOrder < SortingOrder)
                {
                    StartCoroutine(window.CoSetActive(true, true, kill, complete));
                }

                if (window is FullScreen && window != this) break;
            }

            OnBeforeClosed();

            if (_panels != null)
            {
                foreach (var panel in _panels)
                {
                    panel.Close(true);
                }
            }

            // close this fullscreen window with windowed window over this            
            while (true)
            {
                var window = _activatedStack.Pop();

                yield return StartCoroutine(window.CoSetActive(false, directly, kill, complete));

                if (window == this) break;
            }

            OnClosed();
        }
    }
}
