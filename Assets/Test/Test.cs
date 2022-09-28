using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    private void Start()
    {
        var corners = new Vector3[4];

        GetComponent<RectTransform>().GetWorldCorners(corners);

        foreach (var corner in corners)
        {
            Debug.Log(corner);
        }
    }
}
