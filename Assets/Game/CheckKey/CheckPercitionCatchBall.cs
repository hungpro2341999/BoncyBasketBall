using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPercitionCatchBall : Check
{
    public override void CheckKey(Collider2D collision)
    {
       if(collision.gameObject.layer ==16)
        {
         //   Debug.Log("Attack");
            var a = (AI)CtrlGamePlay.Ins.AI;
            a.RunActionTrigger(key);
           
        }
    }

    public override void UnCheckKey(Collider2D collision)
    {
         if(collision.gameObject.layer ==16)
        {
            var a = (AI)CtrlGamePlay.Ins.AI;
            a.RemoveActionTriggerWithKey(key);
        }
    }

}
