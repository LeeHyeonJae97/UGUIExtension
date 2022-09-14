using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MobileUI;

public class MainFullScreen : FullScreen
{
    [SerializeField] private Button _menuButton;

    protected override void Awake()
    {
        base.Awake();

        _menuButton.onClick.AddListener(OnClickMenuButton);
    }

    private void OnClickMenuButton()
    {
        Get<MenuWindowed>().Open(true);
    }
}
