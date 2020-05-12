using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAttackPlayer : Check
{

    public override void CheckKey(Collider2D collision)
    {
      if(collision.gameObject.tag =="Player")
        {
          
            var a = (AI)CtrlGamePlay.Ins.AI;
            a.RunActionTrigger(key);
        
        }
    }

    public override void UnCheckKey(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            var a = (AI)CtrlGamePlay.Ins.AI;
            a.RemoveActionTriggerWithKey(key);
        }
    }

}
