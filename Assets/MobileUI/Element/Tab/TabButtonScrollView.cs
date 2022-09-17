using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MobileUI
{
    public class TabButtonScrollView : TabButton
    {
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private RectTransform _content;

        protected override void OnSelected()
        {
            base.OnSelected();

            _scrollRect.content = _content;
            _content.SetAsLastSibling();
            _content.gameObject.SetActive(true);
        }

        protected override void OnDeselected()
        {
            base.OnDeselected();

            _content.gameObject.SetActive(false);
        }
    }
}
