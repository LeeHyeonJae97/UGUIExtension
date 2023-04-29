using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace MobileUI
{
    public class PopupTextView : Popup
    {
        private PopupTextViewData Data => _data as PopupTextViewData;

        [SerializeField] private TextMeshProUGUI _text;

#if UNITY_EDITOR
        private void Reset()
        {
            _data = new PopupTextViewData("Content", 1f);
        }
#endif

        public override void UpdateView(IViewData data)
        {
            _data = data;

            _text.text = Data.Content;
        }

        protected override void OnBeforeOpened()
        {
            base.OnBeforeOpened();

            StartCoroutine(CoTimer());

            IEnumerator CoTimer()
            {
                yield return new WaitForSeconds(Data.Duration);

                Close();
            }
        }
    }

    [System.Serializable]
    public class PopupTextViewData : IViewData
    {
        public string Content => _content;
        public float Duration => _duration;

        [SerializeField] private string _content;
        [SerializeField] private float _duration;

        public PopupTextViewData(string content, float duration)
        {
            _content = content;
            _duration = duration;
        }
    }
}
