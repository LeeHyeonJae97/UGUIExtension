using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace UI
{
    public sealed class Panel : UIElement
    {
        public event UnityAction OnBeforeOpened;
        public event UnityAction OnOpened;
        public event UnityAction OnBeforeClosed;
        public event UnityAction OnClosed;

        protected override IEnumerator CoOpen(bool directly)
        {
            OnBeforeOpened?.Invoke();

            yield return StartCoroutine(CoSetActive(true, directly));

            OnOpened?.Invoke();
        }

        protected override IEnumerator CoClose(bool directly)
        {
            OnBeforeClosed?.Invoke();

            yield return StartCoroutine(CoSetActive(false, directly));

            OnClosed?.Invoke();
        }
    }
}