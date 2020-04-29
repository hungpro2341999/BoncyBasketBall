using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
public enum Ground { Ground_1, Ground_2 };
public enum CharacterState
{
    None,
    idle,
    jumb1,
    jumb2,
    jumb3,
    jumb2_slamdunk,
    move1,
    move2,
    stun,
    test

}
public abstract class Character : MonoBehaviour
{
    [Header("Animation")]
    public CharacterState StatusCurr;

    public CharacterState PerviousStatus;

    public Spine.AnimationState AnimStatus;
    public SkeletonAnimation AnimationHandle;

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
    public bool isStartJump = false;
    public bool isBall;
    public float timeStartJump;
   
    protected float m_timeStartJump;
    protected float timejump;
    public bool isPullBall = false;
    // Start is called before the first frame update
    public virtual void  Start()
    {
        
        StatusCurr = CharacterState.idle;
        Body = GetComponent<Rigidbody2D>();
        HighWithGround = transform.position.y - Physics2D.RaycastAll(transform.position, Vector2.down, 20, LayerGround)[0].point.y;
        if (AnimationHandle != null)
        {
            AnimStatus = AnimationHandle.AnimationState;
        }
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        GetStatus();
        CaculateStatus();
        Move();
        SetUpAnimation();
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
    public virtual void SetUpAnimation()
    {
        if (PerviousStatus != StatusCurr)
        {
            PlayAnimation();
            PerviousStatus = StatusCurr;
        }
    }

    public void PlayAnimation()
    {
        if (AnimStatus != null)
        {
            AnimStatus.ClearTracks();
            if (StatusCurr == CharacterState.idle)
            {
                AnimStatus.SetAnimation(0, StatusCurr.ToString(), true);
            }
            else
            {
                AnimStatus.SetAnimation(0, StatusCurr.ToString(), false);
            }
          
        }
       

    }
}
