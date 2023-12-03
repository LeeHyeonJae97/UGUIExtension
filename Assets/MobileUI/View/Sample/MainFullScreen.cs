using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UGUIExtension
{
    public class MainFullScreen : FullScreen
    {
        [SerializeField] private UnityEngine.UI.Button _menuButton;

        protected override void Awake()
        {
            base.Awake();

            _menuButton.onClick.AddListener(OnClickMenuButton);
        }

        public override void UpdateView(IViewData data)
        {

        }

        private void OnClickMenuButton()
        {
            Get<MenuWindowed>().Open();
        }
    }
}
