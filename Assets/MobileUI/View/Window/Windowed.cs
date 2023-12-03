using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UGUIExtension
{
    public abstract class Windowed : Window
    {
        protected override sealed IEnumerator CoOpen(bool directly, bool kill, bool complete)
        {
            if (_actives.Contains(this)) yield break;

            _actives.Push(this);

            OnBeforeOpened();

            yield return StartCoroutine(CoSetActive(true, directly, kill, complete));

            OnOpened();
        }

        protected override sealed IEnumerator CoClose(bool directly, bool kill, bool complete)
        {
            if (!_actives.Contains(this)) yield break;

            OnBeforeClosed();

            _actives.Pop();

            yield return StartCoroutine(CoSetActive(false, directly, kill, complete));

            OnClosed();
        }
    }
}
