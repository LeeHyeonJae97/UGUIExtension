using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class Windowed : Window
    {
        protected override sealed IEnumerator OpenInternal(bool directly)
        {
            // activate new window
            yield return StartCoroutine(CoSetActive(true, directly));

            // push new window to stack
            _activatedStack.Push(this);
        }

        protected override sealed IEnumerator CloseInternal(bool directly)
        {
            // pop(remove) from stack
            _activatedStack.Pop();

            // inactivate old window
            yield return StartCoroutine(CoSetActive(false, directly));
        }
    }
}
