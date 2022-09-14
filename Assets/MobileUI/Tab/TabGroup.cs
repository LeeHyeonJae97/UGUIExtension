using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MobileUI
{
    public class TabGroup : MonoBehaviour
    {
        public TabButton Selected { get; internal set; }

        [SerializeField] private TabButton _initial;

        private void Start()
        {
            // if initial tab button is saved in inspector, initialize with it
            if (_initial != null)
            {
                Initialize(_initial);
            }
        }

        public void Initialize(TabButton initial)
        {
            if (initial == null) return;

            // get tab buttons that is in this group
            var buttons = GetComponentsInChildren<TabButton>()
                     .Where((TabButton button) => button.TabGroup == this).ToArray();

            // select initial tab button and deselect others
            foreach (var button in buttons)
            {
                if (button == initial)
                {
                    button.OnStateChanged(true, true);
                }
                else
                {
                    button.OnStateChanged(false, false);
                }
            }

            // save initial tab button
            Selected = initial;
        }
    }
}
