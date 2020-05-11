using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPercitionCatchBall : Check
{
    public override void CheckKey(Collider2D collision)
    {
       if(collision.gameObject.tag == "Percition"||collision.gameObject.layer ==16)
        {
            var a = (AI)CtrlGamePlay.Ins.AI;
            a.RunActionTriiger(key);
           
        }
    }

    public override void UnCheckKey(Collider2D collision)
    {
         if(collision.gameObject.tag == "Percition" || collision.gameObject.layer ==16)
        {
            var a = (AI)CtrlGamePlay.Ins.AI;
            a.RemoveActionTriggerWithKey(key);
        }
    }

}
