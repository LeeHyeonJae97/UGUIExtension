using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MobileUI;

public class ConfirmWindowed : Windowed
{
    [SerializeField] private Button _openSettingsFullScreenButton;
    [SerializeField] private Button _closeButton;

    protected override void Awake()
    {
        base.Awake();

        _openSettingsFullScreenButton.onClick.AddListener(OnClickOpenSettingsFullScreenButton);
        _closeButton.onClick.AddListener(OnClickCloseButton);
    }

    private void OnClickOpenSettingsFullScreenButton()
    {
        Get<SettingsFullScreen>().Open(true);
    }

    private void OnClickCloseButton()
    {
        Open(false);
    }
}
