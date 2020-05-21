using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWithSlampDunk : Check
{
    public string[] KeyActive;
    public bool WaithForAction = false;
    // Start is called before the first frame update
    public override void CheckKey(Collider2D collision)
    {
        if(collision.gameObject.tag == "Hand")
        {
            Player a = (Player)character;
            if (isCorrectKey(a.KeyInput))
            {
                WaithForAction = true;
            }

          

        }
    }
    public override void UnCheckKey(Collider2D collision)
    {
        if (collision.gameObject.tag == "Hand")
        {
            WaithForAction = false;
        }
    }

    public void ActiveAction()
    {
        Player a = (Player)character;
        if (a.isBall)
        {

            a.ActiveActionWithKey(key);

            a.ArrayAction = transform.GetComponentsInChildren<Action>();

            gameObject.SetActive(true);

        
            Debug.Log("Active ");
        }
    }
    public bool isCorrectKey(string key)
    {
        for(int i = 0; i < KeyActive.Length; i++)
        {
            if(KeyActive[i] == key)
            {
                return true;
            }
        }
        return false;
    }



}
