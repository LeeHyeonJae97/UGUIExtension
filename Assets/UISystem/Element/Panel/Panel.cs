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

        protected override void Awake()
        {
            base.Awake();

            // inactivate window at first
            StartCoroutine(CoSetActive(false, true, true, false));
        }

        protected override IEnumerator CoOpen(bool directly, bool kill, bool complete)
        {
            OnBeforeOpened?.Invoke();

            yield return StartCoroutine(CoSetActive(true, directly, kill, complete));

            OnOpened?.Invoke();
        }

        protected override IEnumerator CoClose(bool directly, bool kill, bool complete)
        {
            OnBeforeClosed?.Invoke();

            yield return StartCoroutine(CoSetActive(false, directly, kill, complete));

            OnClosed?.Invoke();
        }
    }
}