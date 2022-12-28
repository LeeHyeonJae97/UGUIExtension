using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace MobileUI
{
    public class PopupTextView : Popup
    {
        [SerializeField] private TextMeshProUGUI _text;

        protected override void OnBeforeOpened()
        {
            base.OnBeforeOpened();

            var data = _data as PopupTextViewData;

            _text.text = data.content;

            StartCoroutine(CoTimer());

            IEnumerator CoTimer()
            {
                yield return new WaitForSeconds(data.duration);

                Close();
            }
        }
    }

    [System.Serializable]
    public class PopupTextViewData : IViewData
    {
        public string content;
        public float duration;

        public PopupTextViewData(string content, float duration)
        {
            this.content = content;
            this.duration = duration;
        }
    }
}
