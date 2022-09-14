using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MobileUI;

public class Initializer : MonoBehaviour
{
    private void Start()
    {
        Window.Get<MainFullScreen>().Open(true);
    }
}
