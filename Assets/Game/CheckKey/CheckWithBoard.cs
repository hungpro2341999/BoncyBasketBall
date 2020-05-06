using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWithBoard : Check
{
    public override void CheckKey(Collider2D collision)
    {
       if(collision.gameObject.tag == "AI")
        {
            var a = (AI)character;
            a.SetKeyTrigger(key);
        }
    }
    public override void UnCheckKey(Collider2D collision)
    {
        if (collision.gameObject.tag == "AI")
        {
            var a = (AI)character;
            a.SetRestoreTrigger(key);
        }
    }
}
