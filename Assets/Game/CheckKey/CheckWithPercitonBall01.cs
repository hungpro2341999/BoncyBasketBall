using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWithPercitonBall01 : Check
{
    public override void CheckKey(Collider2D collision)
    {

        if (collision.gameObject.tag == "Percition" || collision.gameObject.layer ==16)
        {
            Debug.Log("COll : " + collision.gameObject.name + "  ");
            if (character is AI)
            {
               // collision.GetComponent<CheckPoint>().Coll = true;
                var AI = (AI)character;
                AI.SetKeyTrigger(key);

            }

        }
    }

    public override void UnCheckKey(Collider2D collision)
    {
        if (collision.gameObject.tag == "Percition")
        {
            if (character is AI)
            {
          //      collision.GetComponent<CheckPoint>().Coll = false;
                var AI = (AI)character;
                AI.SetRestoreTrigger(key);


            }


        }
    }
}
