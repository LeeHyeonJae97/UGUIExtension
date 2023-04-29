using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MobileUI
{
    public abstract class Windowed : Window
    {
        protected override sealed IEnumerator CoOpen(bool directly, bool kill, bool complete)
        {
            if (_activatedStack.Contains(this)) yield break;

            _activatedStack.Push(this);

            OnBeforeOpened();

            yield return StartCoroutine(CoSetActive(true, directly, kill, complete));

            OnOpened();
        }

        protected override sealed IEnumerator CoClose(bool directly, bool kill, bool complete)
        {
            if (!_activatedStack.Contains(this)) yield break;

            OnBeforeClosed();

            _activatedStack.Pop();

            yield return StartCoroutine(CoSetActive(false, directly, kill, complete));

            OnClosed();
        }
    }
}
