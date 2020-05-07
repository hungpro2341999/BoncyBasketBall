using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWithSlampDunk : Check
{
    // Start is called before the first frame update
    public override void CheckKey(Collider2D collision)
    {
        if(collision.gameObject.tag == "Hand")
        {
            Player a = (Player)character;
           
            a.ActiveActionWithKey(key);

            a.ArrayAction = transform.GetComponentsInChildren<Action>();

            gameObject.SetActive(true);
            
           
            Debug.Log("Active ");
           

        }
    }
}
