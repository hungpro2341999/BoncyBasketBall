using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckStunWithPlayer : Check
{
    public override void CheckKey(Collider2D collision)
    {
         if(collision.gameObject.tag == "Player")
        {
            var a = (Player)CtrlGamePlay.Ins.Player;
            a.ActiveActionWithKey(key);


        }
    }
}
