using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check : MonoBehaviour
{
    public string key;
    public Character character;

    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D collision)
    {
       

        if (collision.gameObject.tag == "Percition")
        {
            Debug.Log("Coll : " + collision.gameObject.name + " " + collision.gameObject.tag);
            if(character is AI)
            {
                var AI = (AI)character;
                 AI.SetValue(key);
            }
          
        }
        if (key == "")
        {
            if (collision.gameObject.layer == 16)
            {

                if (character is AI)
                {
                    var AI = (AI)character;
                    AI.CatchBall();

                }
                else if (character is Player)
                {
                    var Player = (Player)character;
                    Player.CatchBall();
                }

            }
        }
       
       
    }
   
    private void OnTriggerExit2D(Collider2D collision)
    {
      
        if (collision.gameObject.tag == "Percition")
        {
            if(character is AI)
            {
                var AI = (AI)character;
                 AI.ResetActionJump(key);
            }
            else if(character is Player)
            {

            }
           
        }
        
    }
}
