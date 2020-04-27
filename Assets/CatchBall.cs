using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchBall : MonoBehaviour
{
    public Character Handle;
    public Transform PosHand;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == 16)
        {
            var Obj = collision.GetComponent<Ball>();
            collision.GetComponent<Ball>().Velocity = Vector3.zero;
            collision.GetComponent<Ball>().Body.isKinematic = true;
            collision.GetComponent<SphereCollider>().isTrigger = true;
            collision.GetComponent<Ball>().Body.simulated = false;
            Obj.transform.parent = PosHand;
            Obj.transform.localPosition = Vector3.zero;
            if (Handle is Player)
            {
                var a = (Player)Handle;
                a.isBall = true;

            }

            if (Handle is AI)
            {
                var a = (AI)Handle;
                a.isBall = true;

            }

        }
      
    }
    
}
