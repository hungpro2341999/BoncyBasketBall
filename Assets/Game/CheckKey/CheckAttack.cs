using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAttack : Check
{
    // Start is called before the first frame update
    public override void CheckKey(Collider2D collision)
    {
        if (collision.gameObject.tag == "AI")
        {
            if (character.canHurt)
            {
               
                var a = collision.gameObject.GetComponent<AI>();
                a.OnActionEFFWithKey(key);

            }
            
        }
       

        
    }

}
