using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    public void Init(ItemData data)
    {
        _text.text = $"{data.index}";
    }
}
