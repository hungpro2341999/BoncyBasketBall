using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum StatusPlayer { Ground, Jump };
public class Player : Character
{
   
    // Start is called before the first frame update
    public StatusPlayer Status_Player;
    public const string key_Collison_1 = "Key_1";
    public const string key_Collison_2 = "Key_2";
    public const string key_Collison_3 = "Key_3";
    public const string key_Collison_4 = "Key_4";
    
    public Dictionary<string, int> Collison_Body = new Dictionary<string, int>();

    public bool isInputMove = false;

    public bool isBall = false;


    
    public bool isJump = false;

   
    
   
    // For Player:
    public bool isRayToPlayer_1;
    public bool isRayToPlayer_2;
    [Header("CatchBall")]
    public Transform PosHand;
    public Transform TriggerBallForward;

    private void Awake()
    {
        InitKey();
    }
    public override void Start()
    {
        
        base.Start();
        

      
    }

    // Update is called once per frame
    public override void GetStatus()
    {
       
        base.GetStatus();
      
      

    }
    public override void Move()
    {

        if (Input.GetKeyDown(KeyCode.Z))
        {
            PlayAnimation();
        }

            if (Input.GetKeyDown(KeyCode.A))
        {
            isInputMove = true;
            Velocity = speed * Vector2.left;
            if (isGround)
            {
                StatusCurr = CharacterState.move1;
            }
           
           
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            
            isInputMove = true;
            Velocity = speed * Vector2.right;
            if (isGround)
            {
                StatusCurr = CharacterState.move2;
            }
           

        }
       
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (isGround)
            {

                m_timeStartJump = timeStartJump;
                StatusCurr = CharacterState.jumb1;
                Body.velocity = new Vector2(Body.velocity.x, 0);
                Body.AddForce(Force * Vector2.up,ForceMode2D.Impulse);
                isStartJump = true;
                isGround = false;
                StatusCurr = CharacterState.jumb1;
                if (isBall)
                {
                    isPullBall = true;
                }
                else
                {
                    isPullBall = false;
                }
            }
        }   

        if (Input.GetKeyDown(KeyCode.K))
        {
            if (isBall)
            {
                isBall = false;
                CtrlGamePlay.Ins.Launch(Random.Range(5,8));

               
            }
        }
       

        if (Input.GetKeyUp(KeyCode.A))
        {
            if (isGround)
            {
                StatusCurr = CharacterState.idle;
            }
       
            Velocity = Vector2.zero;
           
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            if (isGround)
            {
                StatusCurr = CharacterState.idle;
            }
          
            Velocity = Vector2.zero;


        }

        if (isStartJump)
        {
            if (!isGround)
            {
                if (m_timeStartJump > 0)
                {
                    m_timeStartJump -= Time.deltaTime;
                }
                else
                {
                    TriggerBallForward.gameObject.SetActive(false);
                    if (Mathf.Sign(Body.velocity.y) == 1)
                    {
                        StatusCurr = CharacterState.jumb2;
                      
                    }
                    else
                    {
                        if (isPullBall)
                        {
                            if (isBall)
                            {
                                isBall = false;
                                CtrlGamePlay.Ins.Launch(Random.Range(1, 2));
                                isPullBall = false;

                            }
                            


                        }
                        StatusCurr = CharacterState.jumb3;
                        
                    }

                }
            }
            else
            {
                TriggerBallForward.gameObject.SetActive(true);
                isPullBall = false;
                isStartJump = false;
                StatusCurr = CharacterState.idle;
            }

           
           
        }
       




    }
    private void LateUpdate()
    {
           
           Body.velocity = new Vector2 (Velocity.x,Body.velocity.y);
        
    }
    public override void CaculateStatus()
    {
        base.CaculateStatus();
        if(high >= 0.5f)
        {
            Status_Player = StatusPlayer.Jump;
        }
        else if(high < 0.5f)
        {
            Status_Player = StatusPlayer.Ground;
        }

       
        
        base.CaculateStatus();
    }

    public void InitKey()
    {
        Collison_Body[key_Collison_1] = 0;
        Collison_Body[key_Collison_2] = 0;
        Collison_Body[key_Collison_3] = 0;
        Collison_Body[key_Collison_4] = 0;

    }

    public void Active_Key(string key)
    {
        Collison_Body[key] = 1;
        ActiveAction();

    }
    public void Reset_Key(string key)
    {
        Collison_Body[key] = 0;
    }

    public void ActiveAction()
    {
      
    }
  
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == 16)
        {

            Vector3 bounceDir = (other.gameObject.transform.position - gameObject.transform.position);
            Vector3 shootForce;
            bounceDir.Normalize();
            shootForce = Vector3.Scale(bounceDir, new Vector3(0.3f, 0.3f, 0.3f))*Bounch * (1+high);
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(shootForce, ForceMode2D.Impulse);



        }
    }

    public void CatchBall()
    {
        Debug.Log("CatchBall");
        var collision = (Ball)CtrlGamePlay.Ins.Ball;
        collision.GetComponent<Ball>().Velocity = Vector3.zero;
        collision.GetComponent<Ball>().Body.isKinematic = true;
        collision.GetComponent<CircleCollider2D>().isTrigger = true;

        collision.transform.parent = PosHand;
        collision.transform.localPosition = Vector3.zero;
        isBall = true;
    }
  
    

}
