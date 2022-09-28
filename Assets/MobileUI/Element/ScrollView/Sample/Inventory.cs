using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject _slotPrefab;
    [SerializeField] private ExtendedScrollRect _scrollRect;
    private ItemData[] _data;

    private void OnEnable()
    {
        _scrollRect.onSlotRefreshed.AddListener(OnSlotRefreshed);
    }

    private void OnDisable()
    {
        _scrollRect.onSlotRefreshed.RemoveListener(OnSlotRefreshed);
    }

    private void Start()
    {
        Initialize();
    }

    protected void Initialize()
    {
        _data = new ItemData[20];

        for (int i = 0; i < _data.Length; i++)
        {
            _data[i] = new ItemData(i);
        }

        _scrollRect.Initialize(_data.Length, _slotPrefab.GetComponent<RectTransform>().GetSize());

        for (int i = 0; i < _scrollRect.LengthVisible; i++)
        {
            Instantiate(_slotPrefab, _scrollRect.content).GetComponent<ItemSlot>().Init(_data[i]);
        }
    }

    private void OnSlotRefreshed(int index, GameObject slot)
    {
        slot.GetComponent<ItemSlot>().Init(_data[index]);
    }
}
