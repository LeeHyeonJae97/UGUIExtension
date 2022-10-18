using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MobileUI;

public class MenuWindowed : Windowed
{
    [SerializeField] private Button _confirmButton;
    [SerializeField] private Button _closeButton;

    protected override void Awake()
    {
        base.Awake();

        _confirmButton.onClick.AddListener(OnClickConfirmButton);
        _closeButton.onClick.AddListener(OnClickCloseButton);
    }

    private void OnClickConfirmButton()
    {
        Get<ConfirmWindowed>().Open(true);
    }

    private void OnClickCloseButton()
    {
        Open(false);
    }
}