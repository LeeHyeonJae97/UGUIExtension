using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ExtendedScrollRect : ScrollRect
{
    public enum Type { Horizontal, Vertical, GridHorizontal, GridVertical }

    public int Length { get; private set; }
    public int MaxSlot { get; private set; }
    public bool Full => Length >= MaxSlot;
    public new RectTransform content => _layoutGroup.GetComponent<RectTransform>();
    public UnityEvent<int, GameObject> onSlotRefreshed { get { return _onSlotRefreshed; } set { _onSlotRefreshed = value; } }
    private int FirstIndex { get; set; }
    private int LastIndex { get; set; }

    [SerializeField] private LayoutGroup _layoutGroup;
    [SerializeField] private Type _type;
    [SerializeField] private bool _circularLoop;
    [SerializeField] private UnityEvent<int, GameObject> _onSlotRefreshed;
    private float _slotWidthOrHeight;
    private int _firstDataIndex;
    private int _rowOrColumnCount;
    private Vector2 _padding;
    private Vector2 _spacing;

    protected override void Awake()
    {
        base.Awake();

        onValueChanged.AddListener(OnValueChanged);
    }

    public void Initialize(int length, Vector2 size)
    {
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

        void InitializeHorizontal()
        {
            // save length
            Length = length;

            // set content's size
            base.content.sizeDelta = new Vector2(length * size.x, 0);

            // get slot's width
            _slotWidthOrHeight = size.x;

            // get max count of slots needed and instantiate slot
            MaxSlot = Mathf.CeilToInt(GetComponent<RectTransform>().rect.width / _slotWidthOrHeight) + 1;

            // save spacing value
            _spacing.x = ((HorizontalLayoutGroup)_layoutGroup).spacing;

            // save padding value
            _padding.x = _layoutGroup.padding.left;
        }

        void InitializeVertical()
        {
            // save length
            Length = length;

            // set content's size
            base.content.sizeDelta = new Vector2(0, length * size.y);

            // get slot's height
            _slotWidthOrHeight = size.y;

            // get max count of slots needed and instantiate slot
            MaxSlot = Mathf.CeilToInt(GetComponent<RectTransform>().rect.height / _slotWidthOrHeight) + 1;

            // save spacing value
            _spacing.y = ((VerticalLayoutGroup)_layoutGroup).spacing;

            // save padding value
            _padding.y = _layoutGroup.padding.top;
        }

        void InitializeGridHorizontal()
        {
            // save length
            Length = length;

            // set content's size
            base.content.sizeDelta = new Vector2(length * size.x, 0);

            // get slot's width
            _slotWidthOrHeight = size.x;

            var layoutGroup = (GridLayoutGroup)_layoutGroup;

            // set cell size
            layoutGroup.cellSize = size;

            // get slot count that need per column
            _rowOrColumnCount = layoutGroup.constraint == GridLayoutGroup.Constraint.FixedRowCount ? layoutGroup.constraintCount : Mathf.FloorToInt(GetComponent<RectTransform>().rect.height / size.y);

            // get max count of slots needed and instantiate slot
            MaxSlot = _rowOrColumnCount * (Mathf.CeilToInt(GetComponent<RectTransform>().rect.width / _slotWidthOrHeight) + 1);

            // save spacing value
            _spacing = layoutGroup.spacing;

            // save padding value
            _padding = new Vector2(_layoutGroup.padding.left, _layoutGroup.padding.top);
        }

        void InitializeGridVertical()
        {
            // save length
            Length = length;

            // set content's size
            base.content.sizeDelta = new Vector2(0, length * size.y);

            // get slot's height
            _slotWidthOrHeight = size.y;

            var layoutGroup = (GridLayoutGroup)_layoutGroup;

            // set cell size
            layoutGroup.cellSize = size;

            // get slot count that need per row
            _rowOrColumnCount = layoutGroup.constraint == GridLayoutGroup.Constraint.FixedColumnCount ? layoutGroup.constraintCount : Mathf.FloorToInt(GetComponent<RectTransform>().rect.width / size.x);

            // get max count of slots needed and instantiate slot
            MaxSlot = _rowOrColumnCount * (Mathf.CeilToInt(GetComponent<RectTransform>().rect.height / _slotWidthOrHeight) + 1);

            // save spacing value
            _spacing = layoutGroup.spacing;

            // save padding value
            _padding = new Vector2(_layoutGroup.padding.left, _layoutGroup.padding.top);
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
            if (base.content.anchoredPosition.x + _padding.x < (_layoutGroup.padding.left + _slotWidthOrHeight) * -1)
            {
                if (_circularLoop)
                {
                    // increase padding
                    _layoutGroup.padding.left += (int)_slotWidthOrHeight;

                    // calculate circular index
                    if (++_firstDataIndex >= Length)
                    {
                        _firstDataIndex %= Length;
                    }

                    int index = _firstDataIndex + MaxSlot - 1;

                    if (index >= Length)
                    {
                        index %= Length;
                    }

                    // refresh slot                    
                    _onSlotRefreshed?.Invoke(index, content.GetChild(0).gameObject);

                    // move to last
                    content.GetChild(0).SetAsLastSibling();
                }
                else
                {
                    // if there's nothing to show more, just finish
                    if (_firstDataIndex + MaxSlot == Length) return;

                    // increase padding
                    _layoutGroup.padding.left += (int)_slotWidthOrHeight;

                    // refresh slot                    
                    _onSlotRefreshed?.Invoke(++_firstDataIndex + MaxSlot - 1, content.GetChild(0).gameObject);

                    // move to last
                    content.GetChild(0).SetAsLastSibling();
                }

            }

            // move last slot to first
            else if (base.content.anchoredPosition.x + _padding.x > _layoutGroup.padding.left * -1)
            {
                if (_circularLoop)
                {
                    // reduce padding
                    _layoutGroup.padding.left -= (int)_slotWidthOrHeight;

                    // calculate circular index
                    if (--_firstDataIndex < 0)
                    {
                        _firstDataIndex = Length - 1 + (_firstDataIndex + 1) % (Length - 1);
                    }

                    // refresh slot                    
                    _onSlotRefreshed?.Invoke(_firstDataIndex, content.GetChild(content.childCount - 1).gameObject);

                    // move to first
                    content.GetChild(content.childCount - 1).SetAsFirstSibling();
                }
                else
                {
                    if (_layoutGroup.padding.left <= _padding.x) return;

                    // reduce padding
                    _layoutGroup.padding.left -= (int)_slotWidthOrHeight;

                    // refresh slot                    
                    _onSlotRefreshed?.Invoke(--_firstDataIndex, content.GetChild(content.childCount - 1).gameObject);

                    // move to first
                    content.GetChild(content.childCount - 1).SetAsFirstSibling();
                }
            }
        }

        void OnValueChangedVertical()
        {
            // move first slot to last
            if (base.content.anchoredPosition.y + _padding.y > _layoutGroup.padding.top + _slotWidthOrHeight)
            {
                if (_circularLoop)
                {
                    // increase padding
                    _layoutGroup.padding.top += (int)_slotWidthOrHeight;

                    // calculate circular index
                    if (++_firstDataIndex >= Length)
                    {
                        _firstDataIndex %= Length;
                    }

                    int index = _firstDataIndex + MaxSlot - 1;

                    if (index >= Length)
                    {
                        index %= Length;
                    }

                    // refresh slot                    
                    _onSlotRefreshed?.Invoke(index, content.GetChild(0).gameObject);

                    // move to last
                    content.GetChild(0).SetAsLastSibling();
                }
                else
                {
                    // if there's nothing to show more, just finish
                    if (_firstDataIndex + MaxSlot == Length) return;

                    // increase padding
                    _layoutGroup.padding.top += (int)_slotWidthOrHeight;

                    // refresh slot                    
                    _onSlotRefreshed?.Invoke(++_firstDataIndex + MaxSlot - 1, content.GetChild(0).gameObject);

                    // move to last
                    content.GetChild(0).SetAsLastSibling();
                }
            }

            // move last slot to first
            else if (base.content.anchoredPosition.y + _padding.y < _layoutGroup.padding.top)
            {
                if (_circularLoop)
                {
                    // reduce padding
                    _layoutGroup.padding.top -= (int)_slotWidthOrHeight;

                    // calculate circular index
                    if (--_firstDataIndex < 0)
                    {
                        _firstDataIndex = Length - 1 + (_firstDataIndex + 1) % (Length - 1);
                    }

                    // refresh slot                    
                    _onSlotRefreshed?.Invoke(_firstDataIndex, content.GetChild(content.childCount - 1).gameObject);

                    // move to first
                    content.GetChild(content.childCount - 1).SetAsFirstSibling();
                }
                else
                {
                    if (_layoutGroup.padding.top <= _padding.y) return;

                    // reduce padding
                    _layoutGroup.padding.top -= (int)_slotWidthOrHeight;

                    // refresh slot                    
                    _onSlotRefreshed?.Invoke(--_firstDataIndex, content.GetChild(content.childCount - 1).gameObject);

                    // move to first
                    content.GetChild(content.childCount - 1).SetAsFirstSibling();
                }
            }
        }

        void OnValueChangedGridHorizontal()
        {
            // move all of the slots in first column to last column
            if (base.content.anchoredPosition.x + _padding.x < (_layoutGroup.padding.left + _slotWidthOrHeight) * -1)
            {
                if (_circularLoop)
                {
                    // increase padding
                    _layoutGroup.padding.left += (int)_slotWidthOrHeight;

                    for (int i = 0; i < _rowOrColumnCount; i++)
                    {
                        // calculate circular index
                        if (++_firstDataIndex >= Length)
                        {
                            _firstDataIndex %= Length;
                        }

                        int index = _firstDataIndex + MaxSlot - 1;

                        if (index >= Length)
                        {
                            index %= Length;
                        }

                        _onSlotRefreshed?.Invoke(index, content.GetChild(0).gameObject);

                        // move to last
                        content.GetChild(0).SetAsLastSibling();
                    }
                }
                else
                {
                    // if there's nothing to show more, just finish
                    if (_firstDataIndex + MaxSlot - 1 >= Length - 1) return;

                    // increase padding
                    _layoutGroup.padding.left += (int)_slotWidthOrHeight;

                    for (int i = 0; i < _rowOrColumnCount; i++)
                    {
                        // refresh first slot with last data to show
                        if (_firstDataIndex++ + MaxSlot < Length)
                        {
                            _onSlotRefreshed?.Invoke(_firstDataIndex + MaxSlot - 1, content.GetChild(0).gameObject);
                        }
                        // if there's nothing to show more, just inactivate the slot
                        else
                        {
                            content.GetChild(0).gameObject.SetActive(false);
                        }

                        // move to last
                        content.GetChild(0).SetAsLastSibling();
                    }
                }
            }

            // move all of the slots in last column to first column
            else if (base.content.anchoredPosition.x + _padding.x > _layoutGroup.padding.left * -1)
            {
                if (_circularLoop)
                {
                    // reduce padding
                    _layoutGroup.padding.left -= (int)_slotWidthOrHeight;

                    for (int i = 0; i < _rowOrColumnCount; i++)
                    {
                        // calculate circular index
                        if (--_firstDataIndex < 0)
                        {
                            _firstDataIndex = Length - 1 + (_firstDataIndex + 1) % (Length - 1);
                        }

                        // refresh last slot with first data to show
                        _onSlotRefreshed?.Invoke(_firstDataIndex, content.GetChild(content.childCount - 1).gameObject);

                        // move to first
                        content.GetChild(content.childCount - 1).SetAsFirstSibling();
                    }
                }
                else
                {
                    if (_layoutGroup.padding.left <= _padding.x) return;

                    // reduce padding
                    _layoutGroup.padding.left -= (int)_slotWidthOrHeight;

                    for (int i = 0; i < _rowOrColumnCount; i++)
                    {
                        // refresh last slot with first data to show
                        _onSlotRefreshed?.Invoke(--_firstDataIndex, content.GetChild(content.childCount - 1).gameObject);

                        // if inactivated, activate the slot
                        if (!content.GetChild(content.childCount - 1).gameObject.activeInHierarchy) content.GetChild(content.childCount - 1).gameObject.SetActive(true);

                        // move to first
                        content.GetChild(content.childCount - 1).SetAsFirstSibling();
                    }
                }
            }
        }

        void OnValueChangedGridVertical()
        {
            // move all of the slotss in first row to last row
            if (base.content.anchoredPosition.y + _padding.y > _layoutGroup.padding.top + _slotWidthOrHeight)
            {
                if (_circularLoop)
                {
                    // increase padding
                    _layoutGroup.padding.top += (int)_slotWidthOrHeight;

                    for (int i = 0; i < _rowOrColumnCount; i++)
                    {
                        // calculate circular index
                        if (++_firstDataIndex >= Length)
                        {
                            _firstDataIndex %= Length;
                        }

                        int index = _firstDataIndex + MaxSlot - 1;

                        if (index >= Length)
                        {
                            index %= Length;
                        }

                        _onSlotRefreshed?.Invoke(index, content.GetChild(0).gameObject);

                        // move to last
                        content.GetChild(0).SetAsLastSibling();
                    }
                }
                else
                {
                    // if there's nothing to show more, just finish
                    if (_firstDataIndex + MaxSlot >= Length) return;

                    // increase padding
                    _layoutGroup.padding.top += (int)_slotWidthOrHeight;

                    for (int i = 0; i < _rowOrColumnCount; i++)
                    {
                        // refresh first slot with last data to show
                        if (_firstDataIndex++ + MaxSlot - 1 < Length - 1)
                        {
                            _onSlotRefreshed?.Invoke(_firstDataIndex + MaxSlot - 1, content.GetChild(0).gameObject);
                        }
                        // if there's nothing to show more, just inactivate the slot
                        else
                        {
                            content.GetChild(0).gameObject.SetActive(false);
                        }

                        // move to last
                        content.GetChild(0).SetAsLastSibling();
                    }
                }

            }

            // move all of the slots in last row to first row
            else if (base.content.anchoredPosition.y + _padding.y < _layoutGroup.padding.top)
            {
                if (_circularLoop)
                {
                    // reduce padding
                    _layoutGroup.padding.top -= (int)_slotWidthOrHeight;

                    for (int i = 0; i < _rowOrColumnCount; i++)
                    {
                        // calculate circular index
                        if (--_firstDataIndex < 0)
                        {
                            _firstDataIndex = Length - 1 + (_firstDataIndex + 1) % (Length - 1);
                        }

                        // refresh last slot with first data to show
                        _onSlotRefreshed?.Invoke(_firstDataIndex, content.GetChild(content.childCount - 1).gameObject);

                        // move to first
                        content.GetChild(content.childCount - 1).SetAsFirstSibling();
                    }
                }
                else
                {
                    if (_layoutGroup.padding.top <= _padding.y) return;

                    // reduce padding
                    _layoutGroup.padding.top -= (int)_slotWidthOrHeight;

                    for (int i = 0; i < _rowOrColumnCount; i++)
                    {
                        // refresh last slot with first data to show
                        _onSlotRefreshed?.Invoke(--_firstDataIndex, content.GetChild(content.childCount - 1).gameObject);

                        // if inactivated, activate the slot
                        if (!content.GetChild(content.childCount - 1).gameObject.activeInHierarchy) content.GetChild(content.childCount - 1).gameObject.SetActive(true);

                        // move to first
                        content.GetChild(content.childCount - 1).SetAsFirstSibling();
                    }
                }
            }
        }
    }
}
