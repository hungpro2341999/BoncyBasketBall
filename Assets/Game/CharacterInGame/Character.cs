using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;

public enum Ground { Ground_1, Ground_2 };
public enum CharacterState
{
    None,
    throw1,
    idle,
    jumb1,
    jumb2,
    jumb3,
    jumb2_slamdunk,
    move1,
    move2,
    stun,
    test,
    swing,
    
  

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
  
    public Vector2 Velocity;
    public float Force;
    public float ForceJumpOnGround;
    public float Bounch;
    public float high;
    public float HighWithGround;
    public bool isStartJump = false;
    public bool isBall;
    public bool isGround;
    public bool isJumpGround = false;
    public bool isActiveHand = false;
    public bool isMoveRight;
    public bool isMoveLeft;
  
    public bool isPullBall = false;
  
    public bool canHurt = false;
    public bool isStun = false;
    public float timeStartJump;
    public Transform BoxHurt;
    protected float m_timeStartJump;
    protected float timejump;
    public float speed;

    public float High;
    public string KeyInput = "";

    [Header("ThrowBall")]

    public float PercentageThrowBall;
    public float PercentageDistance;
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
    public virtual void FixedUpdate()
    {
        GetStatus();
        InputPlayer();
        CaculateStatus();
        Move();
        SetUpAnimation();
    }
    public virtual void InputPlayer()
    {
        string isFlyForward = "";
        if (Mathf.Sign(Body.velocity.y) != 0)
        {
            isFlyForward = (((Mathf.Sign(Body.velocity.y)) == 1) ? 1 : 0).ToString();
        }
        else
        {
            isFlyForward = 0.ToString();
        }
       
        KeyInput = isFlyForward + (isMoveRight ? 1 : 0).ToString() + (isMoveLeft ? 1 : 0).ToString() + (isBall ? 1 : 0).ToString();

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
        if (Physics2D.RaycastAll(transform.position, Vector2.down, High, LayerGround).Length >0)
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
              
                // AnimStatus.ClearTracks();
                if (StatusCurr == CharacterState.idle || StatusCurr == CharacterState.jumb2_slamdunk || StatusCurr == CharacterState.stun)
                {
                    //  AnimStatus.ClearTracks();
                    AnimStatus.SetAnimation(0, StatusCurr.ToString(), true);
                }
                else
                {
                    //  AnimStatus.ClearTracks();
                    AnimStatus.SetAnimation(0, StatusCurr.ToString(), false);
                }
          
           

           


        }


     


    }
    public virtual void CatchBall()
    {

    }
   public virtual void OnComplete(Spine.TrackEntry trackEntry)
    {
        
        var animName = trackEntry.Animation.Name;
        if(animName == "swing" )
        {
            canHurt = false;
            isActiveHand = false;
          
        }

        if (animName == "throw1")
        {
            isJumpGround = false;
            

        }








    }

    public void OnStartAnim(Spine.TrackEntry trackEntry)
    {
    }

    public void OnInterrupt(Spine.TrackEntry trackEntry)
    {
        
        var animName = trackEntry.Animation.Name;
        if (animName == "swing")
        {
            isActiveHand = false;
            BoxHurt.gameObject.SetActive(false);
            canHurt = false;
        }

        if (animName == "throw1")
        {
            isJumpGround = false;
        }
    }

    public void OnDispose(Spine.TrackEntry trackEntry)
    {
       
        var animName = trackEntry.Animation.Name;
        if (animName == "swing")
        {
            isActiveHand = false;
            BoxHurt.gameObject.SetActive(false);
            canHurt = false;
        }
        if (animName == "throw1")
        {
            isJumpGround = false;
        }
    }
    public void OnEndAnimation(Spine.TrackEntry trackEntry)
    {
     
    }

    public virtual void Force_Back()
    {
        Body.AddForce(Vector2.right * 10, ForceMode2D.Force);
    }
    public virtual void OnEvent(TrackEntry trackEntry, Spine.Event e)
    {

       
    }
    public virtual void Reset()
    {

    }
}
