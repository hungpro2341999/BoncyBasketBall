using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check : MonoBehaviour
{
    public string key;
    public AI AI;
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D collision)
    {
       

        if (collision.gameObject.tag == "Percition")
        {
            Debug.Log("Coll : " + collision.gameObject.name + " " + collision.gameObject.tag);
            AI.SetValue(key);
        }
        if(collision.gameObject.layer == 16)
        {
            AI.CatchBall();
        }
       
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
      
        if (collision.gameObject.tag == "Percition")
        {
            AI.ResetActionJump(key);
        }
        
    }
}
