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

   


    
    public bool isJump = false;

   
    
   
    // For Player:
    public bool isRayToPlayer_1;
    public bool isRayToPlayer_2;
    [Header("CatchBall")]
    public Transform PosHand;
    public Transform TriggerBallForward;
    public Vector3 TargetHoop;
    [Header("Key Collison And Action ")]

    public const string Key_Slamp_Dunk = "SlampDunk";
    public const string Key_Stun = "Stun";
    public Transform TranSlampDunk;

    public Action[] ArrayAction;
    public bool isComplete = false;
    private int i = 0;


    Dictionary<string, int> Dictinory_Collison = new Dictionary<string, int>();
    public event System.Action<float> OnAction;
    public event System.Action OnMove;

    Dictionary<string, ActionGame> Directory_OnActionGame = new Dictionary<string, ActionGame>();
    Dictionary<string, ActionGame[]> Directory_Key_Status = new Dictionary<string, ActionGame[]>();

    private void Awake()
    {
        InitKey();
    }
    public override void Start()
    {
        
        base.Start();
        AnimStatus.Complete += OnComplete;
        TargetHoop = GameObject.FindGameObjectWithTag("TargetPlayer").transform.position;



        Init();


    }

    // Update is called once per frame
    public override void GetStatus()
    {
       
        base.GetStatus();
      
      

    }
    public override void Move()
    {

      


        if (OnAction != null)
        {
           // Debug.Log("Action");
            OnAction(Time.deltaTime);
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.J))
            {

                if (isGround)
                {

                    m_timeStartJump = timeStartJump;

                    Body.velocity = new Vector2(Body.velocity.x, 0);
                    Body.AddForce(Force * Vector2.up, ForceMode2D.Impulse);
                    isStartJump = true;
                    isGround = false;

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

           // Debug.Log("Ctrl");
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

           

            if (Input.GetKeyDown(KeyCode.K))
            {
                if (isBall)
                {
                    isBall = false;
                    CtrlGamePlay.Ins.Launch(Random.Range(5, 8), TargetHoop);

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
            if (Input.GetKeyDown(KeyCode.K))
            {
                isAttack = true;

            }





            if (isStartJump)
            {
                if (!isGround)
                {
                    if (m_timeStartJump >= 0)
                    {
                        StatusCurr = CharacterState.jumb1;
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
                                    CtrlGamePlay.Ins.Launch(Random.Range(1, 2), TargetHoop);
                                    isPullBall = false;

                                }



                            }
                            StatusCurr = CharacterState.jumb3;

                        }

                    }
                }
                else
                {
                    m_timeStartJump = timeStartJump;
                    TriggerBallForward.gameObject.SetActive(true);
                    isPullBall = false;
                    isStartJump = false;
                    StatusCurr = CharacterState.idle;
                }



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

    public override void CatchBall()
    {
       
        var collision = (Ball)CtrlGamePlay.Ins.Ball;
        collision.GetComponent<Ball>().Velocity = Vector3.zero;
        collision.GetComponent<Ball>().Body.isKinematic = true;
        collision.GetComponent<CircleCollider2D>().isTrigger = true;

        collision.transform.parent = PosHand;
        collision.transform.localPosition = Vector3.zero;
        isBall = true;
        CtrlGamePlay.Ins.AI.isBall = false;
    }

    #region Action

    public void OnActionSlampDunk()
    {
        if (i < ArrayAction.Length)
        {
            if (Vector3.Distance(ArrayAction[i].transform.position, transform.position) ==0)
            {

                ArrayAction[i].SetAction();
                i++;
            }
            else
            {
                StatusCurr = CharacterState.jumb2_slamdunk;
                transform.position = Vector3.MoveTowards(transform.position, ArrayAction[i].transform.position, Time.deltaTime * ArrayAction[i].time);
            }

        }
       
       
   

        

     
       
    }

    public void OnTriggerSlampDunk()
    {
       
        i = 0;
        StatusCurr = CharacterState.jumb2_slamdunk;
        Body.simulated = false;
        Body.constraints = RigidbodyConstraints2D.FreezePositionY;
    }

    public void UnActionSlampDunk()
    {
       
        // StatusCurr = CharacterState.jumb3;
    }

    public void Init()
    {
        ActionGame lc_SlampDunk = new ActionGame(OnTriggerSlampDunk, OnActionSlampDunk,UnActionSlampDunk, 100);
        Directory_OnActionGame.Add(Key_Slamp_Dunk, lc_SlampDunk);
        

       
    }
    public void ActiveActionWithKey(string key)
    {
        Debug.Log("Active ");
        OnActionWithKey(key);
    }

    public void OnActionWithKey(string key)
    {
        Debug.Log(key);

        ActionGame a = Directory_OnActionGame[key];
        switch (a.typeAction)
        {
            case ActionGame.TypeAction.Action:

                StartAcionWithTime(a.ActionStart, a.ActionUpdate, a.ActionRemove, a.time);

                break;
            case ActionGame.TypeAction.Update:

                StartActionUpdate(a.ActionStart, a.ActionUpdate);
                break;
             


        }
    }


    public void StartAcionWithTime(System.Action ActionTrigger, System.Action ActionUpdate, System.Action ActionRemove, float time)
    {
        if (ActionTrigger != null)
        {
            ActionTrigger();
        }

        System.Action expireAction = () =>
        {
            if (ActionRemove != null)
            {
                ActionRemove();
            }

        };

        System.Action<float> UpdateAction = (t) =>
        {
            ActionUpdate();

        };


        SetupTimer(time, UpdateAction, expireAction);

    }

    System.Action SetupTimer(float seconds, System.Action<float> updateAction, System.Action expireAction)
    {

        float timer = 0;

        System.Action expireWrapper = null;

        System.Action<float> updateWrapper = null;
        updateWrapper = (dt) =>
        {
            timer += dt;

            if (updateAction != null)
            {
                updateAction(dt);
            }

            if (timer >= seconds)
            {
                expireWrapper();
            }
        };

        OnAction += updateWrapper;



        expireWrapper = () =>
        {
            OnAction -= updateWrapper;



            if (expireAction != null)
            {
                expireAction();
            }
        };

        return expireWrapper;
    }

    public void StartActionUpdate(System.Action ActionTrigger, System.Action ActionUpdate)
    {
        if (ActionTrigger != null)
        {
            ActionTrigger();
        }
        OnMove = null;
        OnMove += ActionUpdate;

    }



    #endregion 



}
