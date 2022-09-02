using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class Popup : Window
    {
        protected override sealed IEnumerator CoOpen(bool directly)
        {
            // can't open duplicately
            if (_activatedStack.Contains(this)) yield break;

            // can't open multiple popup window
            if (_activatedStack.Peek() is Popup) yield break;

            OnBeforeOpened();

            // open new window
            yield return StartCoroutine(CoSetActive(true, directly));

            // push new window to stack
            _activatedStack.Push(this);

            OnOpened();
        }

        protected override sealed IEnumerator CoClose(bool directly)
        {
            // can't close not opened window
            if (!_activatedStack.Contains(this)) yield break;

            OnBeforeClosed();

            // pop(remove) from stack
            _activatedStack.Pop();

            // close old window
            yield return StartCoroutine(CoSetActive(false, directly));

            OnClosed();
        }
    }
}
