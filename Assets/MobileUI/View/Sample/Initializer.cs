using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UGUIExtension;

namespace UGUIExtension
{
    public class Initializer : MonoBehaviour
    {
        private void Start()
        {
            Window.Get<MainFullScreen>().Open();
        }
    }
}
