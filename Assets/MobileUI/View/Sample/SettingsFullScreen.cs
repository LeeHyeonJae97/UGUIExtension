using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UGUIExtension
{
    public class SettingsFullScreen : FullScreen
    {
        [SerializeField] private UnityEngine.UI.Button _openPanelButton;
        [SerializeField] private UnityEngine.UI.Button _closePanelButton;
        [SerializeField] private UnityEngine.UI.Button _closeButton;
        [SerializeField] private Panel _buttonsPanel;

        protected override void Awake()
        {
            base.Awake();

            _openPanelButton.onClick.AddListener(OnClickOpenPanelButton);
            _closePanelButton.onClick.AddListener(OnClickClosePanelButton);
            _closeButton.onClick.AddListener(OnClickCloseButton);
        }

        public override void UpdateView(IViewData data)
        {
            throw new System.NotImplementedException();
        }

        private void OnClickOpenPanelButton()
        {
            _buttonsPanel.Open(false);
        }

        private void OnClickClosePanelButton()
        {
            _buttonsPanel.Close(false);
        }

        private void OnClickCloseButton()
        {
            Close();
        }
    }
}
