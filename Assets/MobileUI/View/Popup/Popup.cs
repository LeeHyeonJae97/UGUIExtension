using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UGUIExtension
{
    [RequireComponent(typeof(Canvas))]
    public abstract class Popup : View, IComparable<Popup>
    {
        static MaxHeap<Popup> _actives = new MaxHeap<Popup>(5);

        public static T Get<T>() where T : Popup
        {
            int sortingOrder = _actives.Count * 10;

            return Get<T>(sortingOrder);
        }

        public static T Get<T>(int sortingOrder) where T : Popup
        {
            var popup = Instantiate(Resources.Load<T>($"Canvas - {typeof(T).Name}"));

            popup.canvas.sortingLayerName = "Popup";
            popup.canvas.sortingOrder = sortingOrder;

            _actives.Add(popup);

            return popup;
        }

        public static bool Pop()
        {
            bool isEmpty = _actives.IsEmpty;

            if (!isEmpty)
            {
                _actives.Pop().Close(false, true, false);
            }
            return !isEmpty;
        }

        protected override void Awake()
        {
            base.Awake();

            StartCoroutine(CoSetActive(false, true, true, false));
        }

        protected override sealed IEnumerator CoOpen(bool directly, bool kill, bool complete)
        {
            OnBeforeOpened();

            yield return StartCoroutine(CoSetActive(true, directly, kill, complete));

            OnOpened();
        }

        protected override sealed IEnumerator CoClose(bool directly, bool kill, bool complete)
        {
            OnBeforeClosed();

            yield return StartCoroutine(CoSetActive(false, directly, kill, complete));

            OnClosed();
        }

        protected override void OnClosed()
        {
            Destroy(gameObject);
        }

        public int CompareTo(Popup other)
        {
            return canvas.sortingOrder.CompareTo(other.canvas.sortingOrder);
        }
    }
}
