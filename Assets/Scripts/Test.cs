using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Test : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log($"Click {Time.frameCount}");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log($"Down {Time.frameCount}");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log($"Up {Time.frameCount}");
    }

    private void Update()
    {
        Debug.Log(Time.frameCount);
    }
}
