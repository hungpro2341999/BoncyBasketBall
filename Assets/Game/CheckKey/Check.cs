using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check : MonoBehaviour
{
    public string key;
    public Character character;

    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D collision)
    {

        CheckKey(collision);
       

    }

   
   
    private void OnTriggerExit2D(Collider2D collision)
    {

        UnCheckKey(collision);
       
        
    }

    public virtual void CheckKey(Collider2D collision)
    {

    }

    public virtual void UnCheckKey(Collider2D collision)
    {

    }
}
