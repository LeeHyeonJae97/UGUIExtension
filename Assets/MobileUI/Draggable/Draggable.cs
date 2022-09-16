using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MobileUI
{
    [RequireComponent(typeof(RectTransform))]
    public class Draggable : MonoBehaviour
    {
        public bool enabled { get; set; }

        private Canvas Canvas
        {
            get
            {
                if (_canvas == null)
                {
                    _canvas = GetComponentInParent<Canvas>();
                }
                return _canvas;
            }
        }

        private Canvas _canvas;

        private void LateUpdate()
        {
            if (enabled)
            {
                transform.position = Canvas.renderMode == RenderMode.ScreenSpaceCamera ? Canvas.worldCamera.ScreenToWorldPoint(Input.mousePosition) : Input.mousePosition;
            }
        }
    }
}