using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UI;

public class SettingsFullScreen : FullScreen
{
    [SerializeField] private Button _closeButton;

    protected override void Awake()
    {
        base.Awake();

        _closeButton.onClick.AddListener(OnClickCloseButton);
    }

    private void OnClickCloseButton()
    {
        Open(false);
    }
}