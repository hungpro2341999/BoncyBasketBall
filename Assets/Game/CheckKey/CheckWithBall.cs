using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWithBall : Check
{
    public override void CheckKey(Collider2D collision)
    {
        if (collision.gameObject.layer==16)
        {
          
             character.CatchBall();

        }
    }

   
}
