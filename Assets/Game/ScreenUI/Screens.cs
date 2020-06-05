using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screens : MonoBehaviour
{
    public TypeScreen typeSceen;
    // Start is called before the first frame update
    public virtual void Open()
    {
       
        gameObject.SetActive(true);
        EventOpen();
    }

    public virtual void Close()
    {
      
        gameObject.SetActive(false);
        EventClose();
    }

    public virtual void EventClose()
    {

    }

    public virtual void EventOpen()
    {

    }
    public virtual void OnEnableScreen()
    {

    }
    public virtual void OnDisableScreen()
    {

    }

    private void OnDisable()
    {
        OnDisableScreen();
    }
    private void OnEnable()
    {
        OnEnableScreen();
    }



}
