using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ExtendedScrollRect : ScrollRect
{
    public enum Type { Horizontal, Vertical, GridHorizontal, GridVertical }

    public int Length { get; private set; }
    public int LengthVisible { get; private set; }
    public bool Full => Length >= LengthVisible;
    public UnityEvent<int, GameObject> onSlotRefreshed { get { return _onSlotRefreshed; } set { _onSlotRefreshed = value; } }

    [SerializeField] private RectTransform _contentWrapper;
    [SerializeField] private LayoutGroup _layoutGroup;
    [SerializeField] private Type _type;
    [SerializeField] private bool _circularLoop;
    [SerializeField] private bool _scrollbar;
    [SerializeField] private UnityEvent<int, GameObject> _onSlotRefreshed;
    private float _slotWidthOrHeight;
    private int _firstIndex;
    private int _firstIndexCircular;
    private int _rowOrColumnCount;
    private int _padding;
    private Vector2 _spacing;

    protected override void Awake()
    {
        base.Awake();

        onValueChanged.AddListener(OnValueChanged);
    }

    public void Initialize(int length)
    {
        StopMovement();

        switch (_type)
        {
            case Type.Horizontal:
                InitializeHorizontal();
                break;
            case Type.Vertical:
                InitializeVertical();
                break;

            case Type.GridHorizontal:
                InitializeGridHorizontal();
                break;

            case Type.GridVertical:
                InitializeGridVertical();
                break;
        }

        for (int i = 0; i < LengthVisible; i++)
        {
            content.GetChild(i).gameObject.SetActive(i < Length);
        }

        void InitializeHorizontal()
        {
            _firstIndex = 0;
            _firstIndexCircular = 0;

            // save length
            Length = length;

            // reset padding value
            _layoutGroup.padding.left = _padding;

            // set real content's size
            content.sizeDelta = new Vector2(Mathf.Min(length, LengthVisible) * _slotWidthOrHeight + (length - 1) * _spacing.x + _layoutGroup.padding.left + _layoutGroup.padding.right, 0);

            // reset content's anchored position
            (_scrollbar ? _contentWrapper : this.content).anchoredPosition = Vector2.zero;

            // set content's size 
            if (_scrollbar)
            {
                _contentWrapper.sizeDelta = new Vector2(length * _slotWidthOrHeight + (length - 1) * _spacing.x + _layoutGroup.padding.left + _layoutGroup.padding.right, 0);
            }
        }

        void InitializeVertical()
        {
            _firstIndex = 0;
            _firstIndexCircular = 0;

            // save length
            Length = length;

            // reset padding value
            _layoutGroup.padding.top = _padding;

            // set real content's size
            content.sizeDelta = new Vector2(0, Mathf.Min(length, LengthVisible) * _slotWidthOrHeight + (length - 1) * _spacing.y + _layoutGroup.padding.top + _layoutGroup.padding.bottom);

            // reset content's anchored position
            (_scrollbar ? _contentWrapper : this.content).anchoredPosition = Vector2.zero;

            // set content's size
            if (_scrollbar)
            {
                _contentWrapper.sizeDelta = new Vector2(0, length * _slotWidthOrHeight + (length - 1) * _spacing.y + _layoutGroup.padding.top + _layoutGroup.padding.bottom);
            }
        }

        void InitializeGridHorizontal()
        {
            _firstIndex = 0;
            _firstIndexCircular = 0;

            // save length
            Length = length;

            // reset padding value
            _layoutGroup.padding.left = _padding;

            // set real content's size
            content.sizeDelta = new Vector2(Mathf.CeilToInt(Mathf.Min(length, LengthVisible) / _rowOrColumnCount) * (_slotWidthOrHeight + _spacing.x) - _spacing.x + _layoutGroup.padding.left + _layoutGroup.padding.right, 0);

            // reset content's anchored position
            (_scrollbar ? _contentWrapper : this.content).anchoredPosition = Vector2.zero;

            // set content's size
            if (_scrollbar)
            {
                _contentWrapper.sizeDelta = new Vector2(Mathf.CeilToInt(length / _rowOrColumnCount) * (_slotWidthOrHeight + _spacing.x) - _spacing.x + _layoutGroup.padding.left + _layoutGroup.padding.right, 0);
            }
        }

        void InitializeGridVertical()
        {
            _firstIndex = 0;
            _firstIndexCircular = 0;

            // save length
            Length = length;

            // reset padding value
            _layoutGroup.padding.top = _padding;

            // set real content's size
            this.content.sizeDelta = new Vector2(0, Mathf.CeilToInt(Mathf.Min(length, LengthVisible) / _rowOrColumnCount) * (_slotWidthOrHeight + _spacing.y) - _spacing.y + _layoutGroup.padding.top + _layoutGroup.padding.bottom);

            var content = _scrollbar ? _contentWrapper : this.content;

            // reset content's anchored position
            (_scrollbar ? _contentWrapper : this.content).anchoredPosition = Vector2.zero;

            // set content's size
            if (_scrollbar)
            {
                _contentWrapper.sizeDelta = new Vector2(0, Mathf.CeilToInt(length / _rowOrColumnCount) * (_slotWidthOrHeight + _spacing.y) - _spacing.y + _layoutGroup.padding.top + _layoutGroup.padding.bottom);
            }
        }
    }

    public void Initialize(int length, GameObject slotPrefab)
    {
        StopMovement();

        switch (_type)
        {
            case Type.Horizontal:
                InitializeHorizontal();
                break;
            case Type.Vertical:
                InitializeVertical();
                break;

            case Type.GridHorizontal:
                InitializeGridHorizontal();
                break;

            case Type.GridVertical:
                InitializeGridVertical();
                break;
        }

        for (int i = 0; i < LengthVisible; i++)
        {
            var slot = Instantiate(slotPrefab, content);

            if (i >= Length)
            {
                slot.SetActive(false);
            }
        }

        void InitializeHorizontal()
        {
            _firstIndex = 0;
            _firstIndexCircular = 0;

            // get slot's width
            _slotWidthOrHeight = slotPrefab.GetComponent<RectTransform>().GetSize().x;

            // save length
            Length = length;

            // save padding value
            _padding = _layoutGroup.padding.left;

            // save spacing value
            _spacing.x = ((HorizontalLayoutGroup)_layoutGroup).spacing;

            // get max count of slots needed and instantiate slot
            LengthVisible = Mathf.CeilToInt(GetComponent<RectTransform>().GetSize().x / (_slotWidthOrHeight + _spacing.x)) + 1;

            // set real content's size
            content.sizeDelta = new Vector2(Mathf.Min(length, LengthVisible) * _slotWidthOrHeight + (length - 1) * _spacing.x + _layoutGroup.padding.left + _layoutGroup.padding.right, 0);

            // set content's size 
            if (_scrollbar)
            {
                _contentWrapper.sizeDelta = new Vector2(length * _slotWidthOrHeight + (length - 1) * _spacing.x + _layoutGroup.padding.left + _layoutGroup.padding.right, 0);
            }
        }

        void InitializeVertical()
        {
            _firstIndex = 0;
            _firstIndexCircular = 0;

            // get slot's height
            _slotWidthOrHeight = slotPrefab.GetComponent<RectTransform>().GetSize().y;

            // save length
            Length = length;

            // save padding value
            _padding = _layoutGroup.padding.top;

            // save spacing value
            _spacing.y = ((VerticalLayoutGroup)_layoutGroup).spacing;

            // get max count of slots needed and instantiate slot
            LengthVisible = Mathf.CeilToInt(GetComponent<RectTransform>().GetSize().y / (_slotWidthOrHeight + _spacing.y)) + 1;

            // set real content's size
            content.sizeDelta = new Vector2(0, Mathf.Min(length, LengthVisible) * _slotWidthOrHeight + (length - 1) * _spacing.y + _layoutGroup.padding.top + _layoutGroup.padding.bottom);

            // set content's size
            if (_scrollbar)
            {
                _contentWrapper.sizeDelta = new Vector2(0, length * _slotWidthOrHeight + (length - 1) * _spacing.y + _layoutGroup.padding.top + _layoutGroup.padding.bottom);
            }
        }

        void InitializeGridHorizontal()
        {
            _firstIndex = 0;
            _firstIndexCircular = 0;

            var size = slotPrefab.GetComponent<RectTransform>().GetSize();

            // get slot's width
            _slotWidthOrHeight = size.x;

            // save length
            Length = length;

            var layoutGroup = (GridLayoutGroup)_layoutGroup;

            // set cell size
            layoutGroup.cellSize = size;

            // save padding value
            _padding = _layoutGroup.padding.left;

            // save spacing value
            _spacing = layoutGroup.spacing;

            // get slot count that need per column
            _rowOrColumnCount = layoutGroup.constraint == GridLayoutGroup.Constraint.FixedRowCount ? layoutGroup.constraintCount : Mathf.FloorToInt(GetComponent<RectTransform>().GetSize().y / (size.y + _spacing.y));

            // get max count of slots needed and instantiate slot
            LengthVisible = _rowOrColumnCount * (Mathf.CeilToInt(GetComponent<RectTransform>().GetSize().x / (_slotWidthOrHeight + _spacing.x)) + 1);

            // set real content's size
            content.sizeDelta = new Vector2(Mathf.CeilToInt(Mathf.Min(length, LengthVisible) / _rowOrColumnCount) * (_slotWidthOrHeight + _spacing.x) - _spacing.x + _layoutGroup.padding.left + _layoutGroup.padding.right, 0);

            // set content's size
            if (_scrollbar)
            {
                _contentWrapper.sizeDelta = new Vector2(Mathf.CeilToInt(length / _rowOrColumnCount) * (_slotWidthOrHeight + _spacing.x) - _spacing.x + _layoutGroup.padding.left + _layoutGroup.padding.right, 0);
            }
        }

        void InitializeGridVertical()
        {
            _firstIndex = 0;
            _firstIndexCircular = 0;

            var size = slotPrefab.GetComponent<RectTransform>().GetSize();

            // get slot's height
            _slotWidthOrHeight = size.y;

            // save length
            Length = length;

            var layoutGroup = (GridLayoutGroup)_layoutGroup;

            // set cell size
            layoutGroup.cellSize = size;

            // save spacing value
            _spacing = layoutGroup.spacing;

            // save padding value
            _padding = _layoutGroup.padding.top;

            // get slot count that need per row
            _rowOrColumnCount = layoutGroup.constraint == GridLayoutGroup.Constraint.FixedColumnCount ? layoutGroup.constraintCount : Mathf.FloorToInt(GetComponent<RectTransform>().GetSize().x / (size.x + _spacing.x));

            // get max count of slots needed and instantiate slot
            LengthVisible = _rowOrColumnCount * (Mathf.CeilToInt(GetComponent<RectTransform>().GetSize().y / (_slotWidthOrHeight + _spacing.y)) + 1);

            // set real content's size
            content.sizeDelta = new Vector2(0, Mathf.CeilToInt(Mathf.Min(length, LengthVisible) / _rowOrColumnCount) * (_slotWidthOrHeight + _spacing.y) - _spacing.y + _layoutGroup.padding.top + _layoutGroup.padding.bottom);

            // set content's size
            if (_scrollbar)
            {
                _contentWrapper.sizeDelta = new Vector2(0, Mathf.CeilToInt(length / _rowOrColumnCount) * (_slotWidthOrHeight + _spacing.y) - _spacing.y + _layoutGroup.padding.top + _layoutGroup.padding.bottom);
            }
        }
    }

    private void OnValueChanged(Vector2 arg)
    {
        // if don't need to reuse slot, just finish
        if (!Full) return;

        switch (_type)
        {
            case Type.Horizontal:
                OnValueChangedHorizontal();
                break;

            case Type.Vertical:
                OnValueChangedVertical();
                break;

            case Type.GridHorizontal:
                OnValueChangedGridHorizontal();
                break;

            case Type.GridVertical:
                OnValueChangedGridVertical();
                break;
        }

        void OnValueChangedHorizontal()
        {
            // move first slot to last
            if (base.content.anchoredPosition.x < (_firstIndex + 1) * (_slotWidthOrHeight + _spacing.x) * -1)
            {
                if (_circularLoop)
                {
                    // increase padding
                    _layoutGroup.padding.left += (int)(_slotWidthOrHeight + _spacing.x);

                    _firstIndex++;

                    // calculate circular first index
                    _firstIndexCircular = ++_firstIndexCircular % Length;

                    // calculate circular last index
                    int lastIndexCircular = (_firstIndexCircular + LengthVisible - 1) % Length;

                    // get slot
                    var slot = content.GetChild(0);

                    // refresh slot                    
                    _onSlotRefreshed?.Invoke(lastIndexCircular, slot.gameObject);

                    // move to last
                    slot.SetAsLastSibling();
                }
                else
                {
                    // if there's nothing to show more, just finish
                    if (_firstIndex + LengthVisible == Length) return;

                    // increase padding
                    _layoutGroup.padding.left += (int)(_slotWidthOrHeight + _spacing.x);

                    // get index
                    int lastIndex = ++_firstIndex + LengthVisible - 1;

                    // get slot
                    var slot = content.GetChild(0);

                    // refresh slot
                    _onSlotRefreshed?.Invoke(lastIndex, slot.gameObject);

                    // move to last
                    slot.SetAsLastSibling();
                }
            }

            // move last slot to first
            else if (base.content.anchoredPosition.x > _firstIndex * (_slotWidthOrHeight + _spacing.x) * -1)
            {
                if (_circularLoop)
                {
                    // reduce padding
                    _layoutGroup.padding.left -= (int)(_slotWidthOrHeight + _spacing.x);

                    _firstIndex--;

                    // calculate circular index
                    _firstIndexCircular = --_firstIndexCircular < 0 ? Length - 1 + (_firstIndexCircular + 1) % (Length - 1) : _firstIndexCircular;

                    // get slot
                    var slot = content.GetChild(content.childCount - 1);

                    // refresh slot                    
                    _onSlotRefreshed?.Invoke(_firstIndexCircular, slot.gameObject);

                    // move to first
                    slot.SetAsFirstSibling();
                }
                else
                {
                    if (_firstIndex == 0) return;

                    // reduce padding
                    _layoutGroup.padding.left -= (int)(_slotWidthOrHeight + _spacing.x);

                    // get slot
                    var slot = content.GetChild(content.childCount - 1);

                    // refresh slot                    
                    _onSlotRefreshed?.Invoke(--_firstIndex, slot.gameObject);

                    // move to first
                    slot.SetAsFirstSibling();
                }
            }
        }

        void OnValueChangedVertical()
        {
            // move first slot to last
            if (base.content.anchoredPosition.y > (_firstIndex + 1) * (_slotWidthOrHeight + _spacing.y))
            {
                if (_circularLoop)
                {
                    // increase padding
                    _layoutGroup.padding.top += (int)(_slotWidthOrHeight + _spacing.y);

                    _firstIndex++;

                    // calculate circular first index
                    _firstIndexCircular = ++_firstIndexCircular % Length;

                    // calculate circular last index
                    int lastIndexCircular = (_firstIndexCircular + LengthVisible - 1) % Length;

                    // get slot
                    var slot = content.GetChild(0);

                    // refresh slot                    
                    _onSlotRefreshed?.Invoke(lastIndexCircular, slot.gameObject);

                    // move to last
                    slot.SetAsLastSibling();
                }
                else
                {
                    // if there's nothing to show more, just finish
                    if (_firstIndex + LengthVisible == Length) return;

                    // increase padding
                    _layoutGroup.padding.top += (int)(_slotWidthOrHeight + _spacing.y);

                    // get index
                    int lastIndex = ++_firstIndex + LengthVisible - 1;

                    // get slot
                    var slot = content.GetChild(0);

                    // refresh slot
                    _onSlotRefreshed?.Invoke(lastIndex, slot.gameObject);

                    // move to last
                    slot.SetAsLastSibling();
                }
            }

            // move last slot to first
            else if (base.content.anchoredPosition.y < _firstIndex * (_slotWidthOrHeight + _spacing.y))
            {
                if (_circularLoop)
                {
                    // reduce padding
                    _layoutGroup.padding.top -= (int)(_slotWidthOrHeight + _spacing.y);

                    _firstIndex--;

                    // calculate circular index
                    _firstIndexCircular = --_firstIndexCircular < 0 ? Length - 1 + (_firstIndexCircular + 1) % (Length - 1) : _firstIndexCircular;

                    // get slot
                    var slot = content.GetChild(content.childCount - 1);

                    // refresh slot                    
                    _onSlotRefreshed?.Invoke(_firstIndexCircular, slot.gameObject);

                    // move to first
                    slot.SetAsFirstSibling();

                }
                else
                {
                    if (_firstIndex == 0) return;

                    // reduce padding
                    _layoutGroup.padding.top -= (int)(_slotWidthOrHeight + _spacing.y);

                    // get slot
                    var slot = content.GetChild(content.childCount - 1);

                    // refresh slot                    
                    _onSlotRefreshed?.Invoke(--_firstIndex, slot.gameObject);

                    // move to first
                    slot.SetAsFirstSibling();
                }
            }
        }

        void OnValueChangedGridHorizontal()
        {
            // move all of the slots in first column to last column
            if (base.content.anchoredPosition.x < ((_firstIndex / _rowOrColumnCount) + 1) * (_slotWidthOrHeight + _spacing.x) * -1)
            {
                if (_circularLoop)
                {
                    // increase padding
                    _layoutGroup.padding.left += (int)(_slotWidthOrHeight + _spacing.x);

                    for (int i = 0; i < _rowOrColumnCount; i++)
                    {
                        _firstIndex++;

                        // calculate circular first index
                        _firstIndexCircular = ++_firstIndexCircular % Length;

                        // calculate circular last index
                        int lastIndexCircular = (_firstIndexCircular + LengthVisible - 1) % Length;

                        // get slot
                        var slot = content.GetChild(0);

                        // refresh slot                    
                        _onSlotRefreshed?.Invoke(lastIndexCircular, slot.gameObject);

                        // move to last
                        slot.SetAsLastSibling();
                    }
                }
                else
                {
                    // if there's nothing to show more, just finish
                    if (_firstIndex + LengthVisible >= Length) return;

                    // increase padding
                    _layoutGroup.padding.left += (int)(_slotWidthOrHeight + _spacing.x);

                    for (int i = 0; i < _rowOrColumnCount; i++)
                    {
                        // get index
                        int lastIndex = ++_firstIndex + LengthVisible - 1;

                        // get slot
                        var slot = content.GetChild(0);

                        // refresh first slot with last data to show
                        if (lastIndex < Length)
                        {
                            _onSlotRefreshed?.Invoke(lastIndex, slot.gameObject);
                        }
                        // if there's nothing to show more, just inactivate the slot
                        else
                        {
                            slot.gameObject.SetActive(false);
                        }

                        // move to last
                        slot.SetAsLastSibling();
                    }
                }
            }

            // move all of the slots in last column to first column
            else if (base.content.anchoredPosition.x > (_firstIndex / _rowOrColumnCount) * (_slotWidthOrHeight + _spacing.x) * -1)
            {
                if (_circularLoop)
                {
                    // reduce padding
                    _layoutGroup.padding.left -= (int)(_slotWidthOrHeight + _spacing.x);

                    for (int i = 0; i < _rowOrColumnCount; i++)
                    {
                        _firstIndex--;

                        // calculate circular index
                        _firstIndexCircular = --_firstIndexCircular < 0 ? Length - 1 + (_firstIndexCircular + 1) % (Length - 1) : _firstIndexCircular;

                        // get slot
                        var slot = content.GetChild(content.childCount - 1);

                        // refresh last slot with first data to show
                        _onSlotRefreshed?.Invoke(_firstIndexCircular, slot.gameObject);

                        // move to first
                        slot.SetAsFirstSibling();
                    }
                }
                else
                {
                    if (_firstIndex == 0) return;

                    // reduce padding
                    _layoutGroup.padding.left -= (int)(_slotWidthOrHeight + _spacing.x);

                    for (int i = 0; i < _rowOrColumnCount; i++)
                    {
                        // get slot
                        var slot = content.GetChild(content.childCount - 1);

                        // refresh last slot with first data to show
                        _onSlotRefreshed?.Invoke(--_firstIndex, slot.gameObject);

                        // if inactivated, activate the slot
                        if (!slot.gameObject.activeInHierarchy) slot.gameObject.SetActive(true);

                        // move to first
                        slot.SetAsFirstSibling();
                    }
                }
            }
        }

        void OnValueChangedGridVertical()
        {
            // move all of the slotss in first row to last row
            if (base.content.anchoredPosition.y > ((_firstIndex / _rowOrColumnCount) + 1) * (_slotWidthOrHeight + _spacing.y))
            {
                if (_circularLoop)
                {
                    // increase padding
                    _layoutGroup.padding.top += (int)(_slotWidthOrHeight + _spacing.y);

                    for (int i = 0; i < _rowOrColumnCount; i++)
                    {
                        _firstIndex++;

                        // calculate circular first index
                        _firstIndexCircular = ++_firstIndexCircular % Length;

                        // calculate circular last index
                        int lastIndexCircular = (_firstIndexCircular + LengthVisible - 1) % Length;

                        // get slot
                        var slot = content.GetChild(0);

                        _onSlotRefreshed?.Invoke(lastIndexCircular, slot.gameObject);

                        // move to last
                        slot.SetAsLastSibling();
                    }
                }
                else
                {
                    // if there's nothing to show more, just finish
                    if (_firstIndex + LengthVisible >= Length) return;

                    // increase padding
                    _layoutGroup.padding.top += (int)(_slotWidthOrHeight + _spacing.y);

                    for (int i = 0; i < _rowOrColumnCount; i++)
                    {
                        // get index
                        int lastIndex = ++_firstIndex + LengthVisible - 1;

                        // get slot
                        var slot = content.GetChild(0);

                        // refresh first slot with last data to show
                        if (lastIndex < Length)
                        {
                            _onSlotRefreshed?.Invoke(lastIndex, slot.gameObject);
                        }
                        // if there's nothing to show more, just inactivate the slot
                        else
                        {
                            slot.gameObject.SetActive(false);
                        }

                        // move to last
                        slot.SetAsLastSibling();
                    }
                }
            }

            // move all of the slots in last row to first row
            else if (base.content.anchoredPosition.y < (_firstIndex / _rowOrColumnCount) * (_slotWidthOrHeight + _spacing.y))
            {
                if (_circularLoop)
                {
                    // reduce padding
                    _layoutGroup.padding.top -= (int)(_slotWidthOrHeight + _spacing.y);

                    for (int i = 0; i < _rowOrColumnCount; i++)
                    {
                        _firstIndex--;

                        // calculate circular index
                        _firstIndexCircular = --_firstIndexCircular < 0 ? Length - 1 + (_firstIndexCircular + 1) % (Length - 1) : _firstIndexCircular;

                        // get slot
                        var slot = content.GetChild(content.childCount - 1);

                        // refresh last slot with first data to show
                        _onSlotRefreshed?.Invoke(_firstIndexCircular, slot.gameObject);

                        // move to first
                        slot.SetAsFirstSibling();
                    }
                }
                else
                {
                    if (_firstIndex == 0) return;

                    // reduce padding
                    _layoutGroup.padding.top -= (int)(_slotWidthOrHeight + _spacing.y);

                    for (int i = 0; i < _rowOrColumnCount; i++)
                    {
                        // get slot
                        var slot = content.GetChild(content.childCount - 1);

                        // refresh last slot with first data to show
                        _onSlotRefreshed?.Invoke(--_firstIndex, slot.gameObject);

                        // if inactivated, activate the slot
                        if (!slot.gameObject.activeInHierarchy) slot.gameObject.SetActive(true);

                        // move to first
                        slot.SetAsFirstSibling();
                    }
                }
            }
        }
    }
}
