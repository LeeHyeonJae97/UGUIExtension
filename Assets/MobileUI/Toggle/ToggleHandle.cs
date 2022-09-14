using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleHandle : MonoBehaviour
{
    [SerializeField] private Graphic _targetGraphic;
    [SerializeField] private Color _baseColor = Color.white;
    [SerializeField] private Color _normalColor = Color.white;
    [SerializeField] private Color _pressedColor = Color.white;
    [SerializeField] private Color _disabledColor = Color.white;
}
