using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject _slotPrefab;
    [SerializeField] private ExtendedScrollRect _scrollRect;
    private ItemData[][] _data;
    private int _tabIndex;

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            _tabIndex = 0;

            Initialize();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _tabIndex = 1;

            Initialize();
        }

        void Initialize()
        {
            _scrollRect.Initialize(_data[_tabIndex].Length);

            for (int i = 0; i < _data[_tabIndex].Length && i < _scrollRect.LengthVisible; i++)
            {
                _scrollRect.content.GetChild(i).GetComponent<ItemSlot>().Init(_data[_tabIndex][i]);
            }
        }
    }

    protected void Initialize()
    {
        _data = new ItemData[2][];

        _data[0] = new ItemData[1];
        _data[1] = new ItemData[5];

        for (int i = 0; i < _data.Length; i++)
        {
            for (int j = 0; j < _data[i].Length; j++)
            {
                _data[i][j] = new ItemData(j);
            }
        }

        _scrollRect.Initialize(_data[0].Length, _slotPrefab);

        for (int i = 0; i < _data[0].Length && i < _scrollRect.LengthVisible; i++)
        {
            _scrollRect.content.GetChild(i).GetComponent<ItemSlot>().Init(_data[0][i]);
        }
    }

    private void OnSlotRefreshed(int index, GameObject slot)
    {
        slot.GetComponent<ItemSlot>().Init(_data[_tabIndex][index]);
    }
}
