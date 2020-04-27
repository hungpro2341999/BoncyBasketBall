using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Ground { Ground_1, Ground_2 };
public abstract class Character : MonoBehaviour
{
    public Ground GroundCurr;
    public LayerMask LayerGround;
    public Rigidbody2D Body;
    public float speed;
    public bool isGround;
    public float High;
    public Vector2 Velocity;
    public float Force;
    public float Bounch;
    public float high;
    public float HighWithGround;
    // Start is called before the first frame update
    public virtual void  Start()
    {
        Body = GetComponent<Rigidbody2D>();
          HighWithGround = transform.position.y - Physics2D.RaycastAll(transform.position, Vector2.down, 20, LayerGround)[0].point.y;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        GetStatus();
        CaculateStatus();
        Move();
    }

    public virtual void Move()
    {
      
    }
    public virtual void CaculateStatus()
    {
        if(-CtrlGamePlay.Ins.WidthScreen/2 <= transform.position.x && transform.position.x <= 0)
        {
            GroundCurr = Ground.Ground_1;
        }
        else
        {
            GroundCurr = Ground.Ground_2;
        }
    }

    public virtual void GetStatus()
    {
        if (Physics2D.RaycastAll(transform.position, Vector2.down, High, LayerGround).Length > 0)
        {
            
            isGround = true;
        }
        else
        {
            isGround = false;
        }

        var a = Physics2D.RaycastAll(transform.position, Vector2.down, 20, LayerGround);
        if (a.Length > 0)
        {
            high = (transform.position.y - a[0].point.y) - HighWithGround;
         //   Debug.Log("Have");

        }
        else
        {
           // Debug.Log("No Have");
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.down *High);
    }

}
