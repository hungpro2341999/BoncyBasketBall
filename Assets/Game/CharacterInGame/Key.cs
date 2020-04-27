using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public Character character;
    public string key;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
      
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 16)
        {
            if (character is Player)
            {
                var a = (Player)character;

                a.Active_Key(key);


            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 16)
        {
            if (character is Player)
            {
                var a = (Player)character;

                a.Reset_Key(key);


            }
        }
    }

}
