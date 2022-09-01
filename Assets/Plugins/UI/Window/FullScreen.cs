using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class FullScreen : Window
    {
        protected override sealed IEnumerator OpenInternal(bool directly)
        {
            // activate new window
            yield return StartCoroutine(CoSetActive(true, directly));

            foreach (Window window in _activatedStack)
            {
                if (!window.IsActive) break;

                window.SetActive(false, directly);
            }

            // push new window to stack
            _activatedStack.Push(this);
        }

        protected override sealed IEnumerator CloseInternal(bool directly)
        {
            // pop(remove) from stack
            _activatedStack.Pop();

            foreach (Window window in _activatedStack)
            {
                window.SetActive(true, directly);

                if (window is FullScreen) break;
            }

            // inactivate old window
            yield return StartCoroutine(CoSetActive(false, directly));
        }
    }
}
