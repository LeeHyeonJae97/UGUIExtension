using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Inventory : MonoBehaviour
{
    [SerializeField] private ItemSlot _slotPrefab;
    [SerializeField] private ExtendedScrollRect _scrollRect;
    private List<List<ItemData>> _data;
    private int _tabIndex;

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
            _scrollRect.SetItems(_data[_tabIndex].Cast<IScrollRectItem>().ToList());
        }
    }

    protected void Initialize()
    {
        _data = new List<List<ItemData>>(2);

        _data.Add(new List<ItemData>(8));
        _data.Add(new List<ItemData>(5));

        for (int i = 0; i < _data.Capacity; i++)
        {
            for (int j = 0; j < _data[i].Capacity; j++)
            {
                _data[i].Add(new ItemData(j));
            }
        }

        _scrollRect.Initialize(_data[0].Cast<IScrollRectItem>().ToList(), _slotPrefab);
    }
}
