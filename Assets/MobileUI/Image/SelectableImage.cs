using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectableImage : MonoBehaviour
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
    public bool selected
    {
        get { return _selected; }

        set
        {
            _selected = value;

            color = NormalColor;
        }
    }
    public Color color
    {
        set
        {
            if (_image == null)
            {
                _image = GetComponent<Image>();
            }
            _image.color = value;
        }
    }

    public Color SelectedColor => _selectedColor;
    public Color DeselectedColor => _deselectedColor;
    public Color NormalColor => _normalColor * (_selected ? _selectedColor : _deselectedColor);
    public Color PressedColor => _pressedColor * (_selected ? _selectedColor : _deselectedColor);
    public Color DisabledColor => _disabledColor * _deselectedColor;

    [SerializeField] private Color _selectedColor = Color.white;
    [SerializeField] private Color _deselectedColor = Color.white;
    [SerializeField] private Color _normalColor = Color.white;
    [SerializeField] private Color _pressedColor = Color.white;
    [SerializeField] private Color _disabledColor = Color.white;
    private Image _image;
    private bool _interactable;
    private bool _selected;
}
