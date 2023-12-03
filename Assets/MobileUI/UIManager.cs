using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UGUIExtension
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameObject("UIManager").AddComponent<UIManager>();
                }
                return _instance;
            }
        }

        private static UIManager _instance;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                if (!Popup.Pop())
                {
                    Window.Pop();
                }
            }
        }
    }
}
