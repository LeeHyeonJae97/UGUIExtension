using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera
{
    public static UnityEngine.Camera main
    {
        get
        {
            return UnityEngine.Camera.main;
        }
    }
    public static UnityEngine.Camera ui
    {
        get
        {
            if (_ui == null)
            {
                _ui = GameObject.FindGameObjectWithTag("UICamera").GetComponent<UnityEngine.Camera>();
            }
            return _ui;
        }
    }

    private static UnityEngine.Camera _ui;
}
