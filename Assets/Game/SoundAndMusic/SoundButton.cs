using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class SoundButton : MonoBehaviour, IPointerDownHandler
{
    public string sound;
    public void OnPointerDown(PointerEventData eventData)
    {
        AudioCtrl.Ins.Play("Press");
    }

    
   
}
