using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MobileUI;

public class SettingsFullScreen : FullScreen
{
    [SerializeField] private Button _switchButton;
    [SerializeField] private Button _closeButton;
    [SerializeField] private Panel _buttonPanel;

    protected override void Awake()
    {
        base.Awake();

        _switchButton.onClick.AddListener(OnClickSwitchButton);
        _closeButton.onClick.AddListener(OnClickCloseButton);
    }

    private void OnClickSwitchButton()
    {
        _buttonPanel.Open(!_buttonPanel.IsActive, false, false, false);
    }

    private void OnClickCloseButton()
    {
        Open(false);
    }
}