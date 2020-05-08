using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGlobal : MonoBehaviour
{
    public CheckInBall Check;
    public string key;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 16)
        {
            Check.SetKey(key);

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 16)
        {
            Check.RestoreKey(key);

        }
    }


}
