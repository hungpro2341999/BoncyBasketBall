using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWithPlayer : Check
{
    private void Start()
    {
        character = CtrlGamePlay.Ins.AI;
    }
    // Start is called before the first frame update
    public override void CheckKey(Collider2D collision)
    {
     if(collision.gameObject.tag == "Player")
        {
            var a = (AI)character;
            a.SetKeyTrigger(key);
        }
    }

    public override void UnCheckKey(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            var a = (AI)character;
            a.SetRestoreTrigger(key);
        }
    }
}
