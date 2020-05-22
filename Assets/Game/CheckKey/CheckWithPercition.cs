using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWithPercition : Check
{
    // Start is called before the first frame update
    void Start()
    {
        character = CtrlGamePlay.Ins.AI;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void CheckKey(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Percition")
        {
           Debug.Log("Coll : " + collision.gameObject.name + " " + collision.gameObject.tag);
            if (character is AI)
            {
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
                var AI = (AI)character;
                AI.SetRestoreTrigger(key);


            }


        }
    }
}
