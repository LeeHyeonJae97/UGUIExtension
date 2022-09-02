using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class FullScreen : Window
    {
        protected override sealed IEnumerator CoOpen(bool directly)
        {
            // can't open duplicately
            if (_activatedStack.Contains(this)) yield break;

            // can't open over popup window
            if (_activatedStack.Peek() is Popup) yield break;

            OnBeforeOpened();

            // open new window
            yield return StartCoroutine(CoSetActive(true, directly));

            // close all the windows until fullscreen
            foreach (Window window in _activatedStack)
            {
                if (window.SortingOrder < SortingOrder)
                {
                    StartCoroutine(window.CoSetActive(false, true));
                }

                if (window is FullScreen) break;
            }

            // push new window to stack
            _activatedStack.Push(this);

            OnOpened();
        }

        protected override sealed IEnumerator CoClose(bool directly)
        {
            // can't close not opened window
            if (!_activatedStack.Contains(this)) yield break;

            // recover closed windows when this window is opened
            foreach (Window window in _activatedStack)
            {
                if (window.SortingOrder < SortingOrder)
                {
                    StartCoroutine(window.CoSetActive(true, true));
                }

                if (window is FullScreen) break;
            }

            OnBeforeClosed();

            // close this fullscreen window with windowed window over this            
            while(true)
            {
                var window = _activatedStack.Pop();

                yield return StartCoroutine(window.CoSetActive(false, directly));
                
                if (window == this) break;
            }

            OnClosed();
        }
    }
}
