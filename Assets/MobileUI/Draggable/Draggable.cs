using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MobileUI
{
    [RequireComponent(typeof(RectTransform))]
    public class Draggable : MonoBehaviour
    {
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

#if UNITY_EDITOR
        private void Reset()
        {
            enabled = false;
        }
#endif

        private void LateUpdate()
        {
            transform.position = Canvas.renderMode == RenderMode.ScreenSpaceCamera ? Canvas.worldCamera.ScreenToWorldPoint(Input.mousePosition) : Input.mousePosition;
        }
    }
}