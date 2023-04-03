using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MobileUI
{
    public class FullScreen : Window
    {
        protected override sealed IEnumerator CoOpen(bool directly, bool kill, bool complete)
        {
            if (_activatedStack.Contains(this)) yield break;

            _activatedStack.Push(this);

            OnBeforeOpened();

            yield return StartCoroutine(CoSetActive(true, directly, kill, complete));

            OnOpened();

            CloseAllBelowWindows();

            // LOCAL FUNCTION
            void CloseAllBelowWindows()
            {
                foreach (Window window in _activatedStack)
                {
                    if (window.SortingOrder < SortingOrder)
                    {
                        StartCoroutine(window.CoSetActive(false, true, kill, complete));
                    }

                    if (window is FullScreen && window != this) break;
                }
            }
        }

        protected override sealed IEnumerator CoClose(bool directly, bool kill, bool complete)
        {
            if (!_activatedStack.Contains(this)) yield break;

            ReopenAllClosedWindows();

            OnBeforeClosed();

            yield return StartCoroutine(CoCloseAllOverWindows());

            OnClosed();

            // LOCAL FUNCTION
            void ReopenAllClosedWindows()
            {
                foreach (Window window in _activatedStack)
                {
                    if (window.SortingOrder < SortingOrder)
                    {
                        StartCoroutine(window.CoSetActive(true, true, kill, complete));
                    }

                    if (window is FullScreen && window != this) break;
                }
            }

            // LOCAL FUNCTION            
            IEnumerator CoCloseAllOverWindows()
            {
                while (true)
                {
                    var window = _activatedStack.Pop();

                    yield return StartCoroutine(window.CoSetActive(false, directly, kill, complete));

                    if (window == this) break;
                }
            }
        }
    }
}
