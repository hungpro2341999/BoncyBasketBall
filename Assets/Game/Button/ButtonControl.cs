using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonControl : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    public delegate void OnDown();
    public delegate void OnUp();
    public event OnDown eventDownButton;
    public event OnUp eventUpButton;

    public void OnPointerDown(PointerEventData eventData)
    {
        eventDownButton();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(eventUpButton!=null)
        eventUpButton();
    }
}
