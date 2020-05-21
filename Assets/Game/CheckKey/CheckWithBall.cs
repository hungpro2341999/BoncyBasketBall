using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWithBall : Check
{
   
    public override void CheckKey(Collider2D collision)
    {
        if (collision.gameObject.layer==16)
        {
            var a = (Ball)CtrlGamePlay.Ins.Ball;

            if (!character.isStun)
            {
                if (!a.isHand)
                {
                    character.CatchBall();
                }
               
            }
             

        }
    }

   
}
