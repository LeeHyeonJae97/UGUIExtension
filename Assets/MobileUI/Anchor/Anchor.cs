using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Anchor : MonoBehaviour
{
    private enum Type
    {
        LeftTop,
        Top,
        RightTop,
        Left,
        Center,
        Right,
        LeftBottom,
        Bottom,
        RightBottom,

        Length,
    }

    [SerializeField]
    private Type _type;

    private void Awake()
    {
        UpdateAnchor();
    }

    private void UpdateAnchor()
    {
        var rectTr = GetComponent<RectTransform>();
        var value = GetValue();

        rectTr.anchorMax = value;
        rectTr.anchorMin = value;
        rectTr.pivot = value;
        rectTr.anchoredPosition = GetPosition();
        rectTr.sizeDelta = Vector2.zero;

        Vector2 GetValue()
        {
            switch (_type)
            {
                case Type.LeftTop:
                    return new Vector2(0, 1);

                case Type.Top:
                    return new Vector2(0.5f, 1);

                case Type.RightTop:
                    return new Vector2(1, 1);

                case Type.Left:
                    return new Vector2(0, 0.5f);

                case Type.Center:
                    return new Vector2(0.5f, 0.5f);

                case Type.Right:
                    return new Vector2(1, 0.5f);

                case Type.LeftBottom:
                    return new Vector2(0, 0);

                case Type.Bottom:
                    return new Vector2(0.5f, 0);

                case Type.RightBottom:
                    return new Vector2(1, 0);

                default:
                    return Vector2.zero;
            }
        }

        Vector2 GetPosition()
        {
            var width = GetComponentInParent<Canvas>().GetComponent<RectTransform>().rect.width;
            var height = GetComponentInParent<Canvas>().GetComponent<RectTransform>().rect.height;

            var targetWidth = 1920f;
            var targetHeight = 1080f;

            var ratio = (float)width / height;
            var targetRatio = (float)targetWidth / targetHeight;

            if (ratio > targetRatio)
            {
                var fix = width * (float)(1 - (targetRatio / ratio)) / 2;

                return new Vector2(value.x == 0 ? fix : value.x == 1 ? -fix : 0, 0);
            }
            else
            {
                return Vector2.zero;
            }
        }
    }

#if UNITY_EDITOR
    [MenuItem("GameObject/UI/Anchor")]
    static private void Create()
    {
        if (Selection.activeGameObject == null || !Selection.activeGameObject.TryGetComponent<Canvas>(out var canvas))
        {
            Debug.LogWarning("You should click canvas");
            return;
        }

        for (int i = 0; i < (int)Type.Length; i++)
        {
            var go = new GameObject($"{(Type)i}");

            var rectTr = go.AddComponent<RectTransform>();
            rectTr.SetParent(canvas.transform);

            var anchor = go.AddComponent<Anchor>();
            anchor._type = (Type)i;
            anchor.UpdateAnchor();
        }
    }

    [ContextMenu("Refresh")]
    private void Refresh()
    {
        UpdateAnchor();
    }
#endif
}
