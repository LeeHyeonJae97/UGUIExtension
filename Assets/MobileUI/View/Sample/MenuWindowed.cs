using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UGUIExtension
{
    public class MenuWindowed : Windowed
    {
        [SerializeField] private UnityEngine.UI.Button _confirmButton;
        [SerializeField] private UnityEngine.UI.Button _closeButton;

        protected override void Awake()
        {
            base.Awake();

            _confirmButton.onClick.AddListener(OnClickConfirmButton);
            _closeButton.onClick.AddListener(OnClickCloseButton);
        }

        public override void UpdateView(IViewData data)
        {

        }

        private void OnClickConfirmButton()
        {
            Get<ConfirmWindowed>().Open();
        }

        private void OnClickCloseButton()
        {
            Close();
        }
    }
}
