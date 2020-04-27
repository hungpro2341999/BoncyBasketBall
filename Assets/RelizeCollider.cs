using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelizeCollider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        var a =  Physics2D.CircleCast(transform.position, 0.2f, Vector2.up);
        if (a.collider != null)
        {
         //   Debug.Log(a.collider.gameObject.name);
        }
    }
}
