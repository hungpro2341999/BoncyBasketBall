using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWithBoard : Check
{
    private void Start()
    {
        character = CtrlGamePlay.Ins.AI;
    }
    public override void CheckKey(Collider2D collision)
    {
       if(collision.gameObject.tag == "AI")
        {
            Debug.Log(key+""+gameObject.name);
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
