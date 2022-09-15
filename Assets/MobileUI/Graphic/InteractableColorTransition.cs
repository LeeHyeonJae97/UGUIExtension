using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Graphic))]
public class InteractableColorTransition : MonoBehaviour
{
    public bool interactable
    {
        get { return _interactable; }

        set
        {
            _interactable = value;

            color = _interactable ? NormalColor : DisabledColor;
        }
    }
    public Color color
    {
        set
        {
            if (_graphic == null)
            {
                _graphic = GetComponent<Graphic>();
            }
            _graphic.color = value;
        }
    }

    public Color BaseColor => _baseColor;
    public Color NormalColor => _baseColor * _normalColor;
    public Color PressedColor => _baseColor * _pressedColor;
    public Color DisabledColor => _baseColor * _disabledColor;

    [SerializeField] private Color _baseColor = Color.white;
    [SerializeField] private Color _normalColor = Color.white;
    [SerializeField] private Color _pressedColor = Color.white;
    [SerializeField] private Color _disabledColor = Color.white;
    private Graphic _graphic;
    private bool _interactable;
}
