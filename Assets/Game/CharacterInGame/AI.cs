using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using UnityEngine.UI;
using System.IO;
using Spine;



public enum DirectWithPlayer { Right,Left,None};

public class AI : Character
{






    [Header("InforCPU")]
    #region InforAI
    public LayerMask Player;
    public TextAsset textCPU;

    public float ForceHead;
    public float SpeedShoe;
    public  DirectWithPlayer DirectCpu;

    #endregion





    public bool isJump;
    public bool isJump_x2;
    public bool isThrowBall;
    public bool hasPullBall;



    public float Left;
    public float Right;
    public float Up;
    public float ThrowBall;

    public float delayAI;

    public float xTargetHoop;
    public float yTargetHoop;
    [Header("STATUS")]
    // Status

   
    public float DistanceToPlayer;

    public float PrecJump;
    public float DistanceToJump;

    public int[] MatrixPositonAi;



    public int CountSperateDistance;

    public float ForceBack;
    public float timeStun;

    public bool isLimit = false;


    public float Amount;
    public float PosInit;



    public int PreviousPos;
    public bool ChangeStatus;


    public Transform PosHand;
    public Transform BoxAttack;
    public Transform BoxCatchBall;


    public Transform TriggerBallForward;
    [Header("CPU")]
    // Key Action

    public const string Key_Thown_On_Ground = "OnThrownOnGround";
    public const string Key_Jump_Right = "OnJumpRight";
    public const string Key_Jump_Left = "OnJumpleft";
    public const string Key_Jump_Straight = "OnActionJump";

    public const string Key_Move_To_Ball = "MoveToBall";
    public const string Key_Move_To_Hoop = "MoveToHoop";

    public const string Key_Catch_Ball = "On_Catch_Ball";
    public const string Key_Move_Random = "MoveRandom";
    public const string Key_Move_Ide = "Ide";
    public const string Key_Move_To_Player = "OnMoveToPlayer";

    public const string Key_Slamp_Dunk = "SlampDunk";
    public const string Key_Stun = "Key_Stun";

    public const string Key_Action_Move_To_Ball = "OnActionMoveToBall";
    public const string Key_Action_Move_Back_Have_Ball = "OnActionMoveBackHaveBall";
    public const string Key_Action_Jump_Throw_Ball = "JumpThrowBall";

    public const string Key_Action_Protect_Ball = "OnActionMoveProtectBall";

    // Action Trigger
    public const string Key_Action_Trigger_Attack = "Action_Attack";

    public const string Key_Action_Trigger_CatchBall = "Action_Catch_Ball";





    // Key

    public const string Key_Trigger_Have_Ball = "Trigger_Ball";
    public const string Key_Trigger_Jump = "Trigger_Jump";
    public const string Key_Trigger_ThrowBall = "Trigger_Throw_Ball";
    public const string Key_Trigger_Front = "Trigger_Front";
    public const string Key_Trigger_Back = "Trigger_Back";

    public const string Key_Trigger_Slamp_Dunk = "Trigger_Slamp_Dunk";

    public const string Key_Trigger_With_Ball = "Trigger_Ball";
    public const string Key_Trigger_Board_CPU = "Trigger_Board_CPU";












    protected Dictionary<string, ActionGame> Directory_OnActionGame = new Dictionary<string, ActionGame>();

    protected Dictionary<string, int> Directory_StatusCpu = new Dictionary<string, int>();

    protected Dictionary<string, ActionGame[]> Directory_Key_Status = new Dictionary<string, ActionGame[]>();

    protected Dictionary<string, ActionGame[]> Directory_Key_Status_Have_Ball = new Dictionary<string, ActionGame[]>();

    protected Dictionary<string, ActionGame[]> Directory_Key_Status_Player_Have_Ball = new Dictionary<string, ActionGame[]>();

    protected Dictionary<string, ProcessKey> Directory_Action_Process_Key = new Dictionary<string, ProcessKey>();

    public List<ProcessKey> ListProcessKey = new List<ProcessKey>();


    public event System.Action<float> OnAction;
   
    public event System.Action OnMove;
    public event System.Action<float> OnActionEFF;
    public event System.Action OnActionTrigger;
    public event System.Action<string> OnProcessKeyAction;



    public Stack<System.Action> Stack_Active_Action = new Stack<System.Action>();

    public string KeyActionCurr;
    public string KeyActionPrevious;

    public string KeyCurrBall;
    public string KeyCurrBallPervious;

    //

    public string KeyActionTriggerCurr;
    public string KeyActionTriggerPrevious;





    [Header("Debug")]



    public Text TextStatus;
    public bool test = false;



    public enum TypeRandomMove { RandomDistance, RandomInGround1, RandomInGround2 }
    [Header("Move")]

    public Vector3 TargetHoop;



    // Move Random
    public float TargetX;

    public float DistanceRandom;

    public float TargetRandom;

    [Header("ActionEFF")]

    public Action[] ArrayAction;
    public bool isComplete = false;
    private int i = 0;
    public bool isAction = false;

    protected bool changeStatus = false;



    public void PushAction(System.Action Action)
    {
        Stack_Active_Action.Push(Action);
    }
    public System.Action PoPAction()
    {

        return Stack_Active_Action.Pop();
    }



    public Stack<System.Action> Stack_Action_Move = new Stack<System.Action>();




    #region GetSetAction
    public bool isOnAction()
    {
        return OnAction != null;
    }

    public bool isOnMove()
    {
        return OnMove != null;
    }

    public bool isActionEff()
    {
        return OnActionEFF != null;
    }

    public bool isActionTrigger()
    {
        return OnActionTrigger != null;
    }




    #endregion


    public override void Start()
    {
        base.Start();
        AnimStatus.Complete += OnComplete;
        AnimStatus.Start += OnStartAnim;
        AnimStatus.Interrupt += OnInterrupt;
        AnimStatus.Dispose += OnDispose;
        AnimStatus.Event += OnEvent;


        CtrlGamePlay.Ins.eventRestGamePlay += Reset;
        // bla bla
        // AnimStatus.Complete += OnComplete;

        Amount = CtrlGamePlay.Ins.WidthScreen / CountSperateDistance;

        MatrixPositonAi = new int[CountSperateDistance];

        PosInit = (CtrlGamePlay.Ins.WidthScreen / 2)-0.3f;
        TargetX = transform.position.x;

        TargetHoop = GameObject.FindGameObjectWithTag("TargetCPU").transform.position;
        xTargetHoop = GameObject.FindGameObjectWithTag("TargetHoop").transform.position.x;
        yTargetHoop = GameObject.FindGameObjectWithTag("TargetHoop").transform.position.x;
        base.Start();


        Init();

        CtrlGamePlay.Ins.eventRestGamePlay += Event_Reset;
        CtrlGamePlay.Ins.eventResetGame += Event_Reset;
    }



    #region Core
    public override void CaculateStatus()
    {
   //     Debug.Log("Distance Start : " + Distance_Player_CPU());
        var a = CtrlGamePlay.Ins.AI;
        var b = CtrlGamePlay.Ins.Player;
        if((a.CurrPos - b.CurrPos )== 0)
        {
            DirectCpu = DirectWithPlayer.None;
        }
        else
        {
            if(Mathf.Sign((a.CurrPos - b.CurrPos))==1)
            {
                
                DirectCpu = DirectWithPlayer.Left;

            }
            else
            {
                DirectCpu = DirectWithPlayer.Right;
            }
        }
        if (Input.GetKeyDown(KeyCode.L))
        {

            OnAttack();
        }

        RaycastHit2D hits = Physics2D.Raycast(Vector3.left * 0.5f + transform.position - Vector3.up * 0.5f, Vector2.left, 1.2f);
        // 
        if (hits.collider != null)
        {
            if (hits.collider.gameObject.layer == 13)
            {
               // Debug.Log("Coll");
                Directory_StatusCpu[Key_Trigger_Front] = 1;
            }
            else
            {
                Directory_StatusCpu[Key_Trigger_Front] = 0;
            }

        }
        else
        {
            Directory_StatusCpu[Key_Trigger_Front] = 0;
        }








        if (OnActionEFF != null)
        {
            //     Debug.Log("EFF_Action");
            OnActionEFF(Time.deltaTime);
        }

        else
        {



            if (OnAction == null)
            {
                ActiveActionKey();

            }

            if (!test)
            {
                if (OnAction != null)
                {
                    OnAction(Time.deltaTime);



                }
                else if (OnMove != null)
                {

                    OnMove();


                }
                else
                {
                    isMoveLeft = false;
                    isMoveRight = false;

                }

            }
            else
            {
               
            }


            if (OnActionTrigger != null)
            {
                Debug.Log("HaveTrigger");
                OnActionTrigger();
            }
            else
            {
                Debug.Log("NotHaveTrigger");
            }


        }




    }

    public bool isInAction()
    {
        return OnAction != null;
    }



    public virtual void ActiveActionKey()
    {
        var a = (Ball)CtrlGamePlay.Ins.Ball;
        KeyCurrBall = a.ControllerBy();
        if (KeyCurrBallPervious != KeyCurrBall)
        {

            changeStatus = true;

        }
        KeyCurrBallPervious = KeyCurrBall;

        if (KeyCurrBall == "")
        {
            return;
        }
      //  Debug.Log("Active");

        var action = Directory_Action_Process_Key[KeyCurrBall].StartAction;
        if (action != null)
        {
      //      Debug.Log("Deo Null");
            action.Invoke();
        }
        else
        {
        //    Debug.Log("Null");
        }

    }



    public void LoadCurrPosionPlayer()
    {

        int point = (Mathf.FloorToInt(((PosInit - transform.position.x) / Amount)));

        CurrPos = point;

    }

    public virtual int PostionToPos(float x)
    {
        int point = (int)((PosInit - x) / Amount);

        return point;
    }
    public float PosToPositon(int x)
    {
        return x * Amount;
    }




    public override void Move()
    {
        if (isAction)
            return;
        if (isMoveRight)
        {
            Velocity = speed * Vector2.right;
        }

        if (isMoveLeft)
        {
            Velocity = speed * Vector2.left;
        }


        if (isGround)
        {
            if (!isJumpGround)
            {
                if (isJump)
                {

                    m_timeStartJump = timeStartJump;
                    if (isBall)
                    {
                        if (hasPullBall)
                        {
                            isPullBall = true;
                        }
                        else
                        {
                            isPullBall = false;
                        }

                    }
                    isJump = false;

                    isGround = false;
                    isStartJump = true;



                    Body.velocity = new Vector2(Body.velocity.x, Force);

                }
            }

        }



        //if (isThrowBall)
        //{
        //    isThrowBall = false;
        //    CtrlGamePlay.Ins.ThrowBall();
        //}

        if (isGround)
        {
            if (!isJumpGround && !isActiveHand)
            {
                //    Debug.Log(Body.velocity.x);
                if (!isMoveLeft && !isMoveRight)
                {
                    StatusCurr = CharacterState.idle;
                }
                else if (isMoveLeft && !isMoveRight)
                {
                    StatusCurr = CharacterState.move2;
                }
                else if (!isMoveLeft && isMoveRight)
                {
                    StatusCurr = CharacterState.move1;

                }
            }



        }
        else
        {
            isActiveHand = false;
            if (!isJumpGround)
            {
                // BoxCatchBall.gameObject.SetActive(false);
                if (m_timeStartJump >= 0)
                {
                    StatusCurr = CharacterState.jumb1;
                    m_timeStartJump -= Time.deltaTime;

                }
                else
                {
                    // TriggerBallForward.gameObject.SetActive(false);
                    if (Mathf.Sign(Body.velocity.y) == 1 || Mathf.Sign(Body.velocity.y) == 0)
                    {
                        StatusCurr = CharacterState.jumb2;

                    }
                    else if (Mathf.Sign(Body.velocity.y) == -1)
                    {
                        if (isPullBall)
                        {
                            if (isBall)
                            {

                                isBall = false;
                                CtrlGamePlay.Ins.CpuThrowBall();
                                isPullBall = false;
                                hasPullBall = false;



                            }


                        }
                        else
                        {
                            hasPullBall = false;
                        }
                        StatusCurr = CharacterState.jumb3;

                    }

                }
            }

        }

       


    }




    public override void CatchBall()
    {
        //   Debug.Log("CatchBall");
        var collision = (Ball)CtrlGamePlay.Ins.Ball;
        collision.GetComponent<Ball>().Velocity = Vector3.zero;
        collision.GetComponent<Ball>().Body.isKinematic = true;
        collision.GetComponent<CircleCollider2D>().isTrigger = true;

        collision.transform.parent = PosHand;
        collision.transform.localPosition = Vector3.zero;
        isBall = true;
        CtrlGamePlay.Ins.Player.isBall = false;

    }


    public override void GetStatus()
    {
        base.GetStatus();

        LoadCurrPosionPlayer();



        //1.

        if (isBall)
        {
            Directory_StatusCpu[Key_Trigger_Have_Ball] = 1;

        }
        else
        {
            Directory_StatusCpu[Key_Trigger_Have_Ball] = 0;


        }

        //2.


    }


    private void LateUpdate()
    {

        Body.velocity = new Vector2(((isMoveRight ? 1 : 0) + (isMoveLeft ? -1 : 0)) * speed, Body.velocity.y);

    }



    #endregion

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 16)
        {
            if (!isBall)
            {
                Vector3 bounceDir = collision.gameObject.transform.position - gameObject.transform.position;
                Vector3 shootForce;
                bounceDir.Normalize();
                shootForce = Vector3.Scale(bounceDir, new Vector3(ForceHead, ForceHead, ForceHead)) * Bounch;
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(shootForce, ForceMode2D.Force);
            }

        }
    }


    #region SetUpKeyAction



    public void ActionWithKey(string key)
    {
        if (Directory_OnActionGame.ContainsKey(key))
        {

        }
    }



    #endregion

    #region AnimationPullBall



    #endregion



    #region Animation



    #endregion










    // Move
    #region Move

    public virtual void OnTriggerMoveToHoop()
    {

    }




    public virtual void OnMoveToHoop()
    {

        if (Mathf.Abs(transform.position.x - PostionToPos(xTargetHoop)) != 0)
        {
            if (Mathf.Sign(xTargetHoop - transform.position.x) == 1)
            {
                isMoveLeft = false;
                isMoveRight = true;
            }
            else
            {
                isMoveLeft = true;
                isMoveRight = false;
            }
        }
        else
        {
            isMoveLeft = false;
            isMoveRight = false;
        }

    }


    public virtual void OnMoveIde()
    {
        //  OnAction = null;
        // OnMove = null;
        isMoveLeft = false;
        isMoveRight = false;

    }



    //private void  OnStun          

    public virtual void OnMoveRandom()
    {



        if (isChange())
        {
            while (Mathf.Abs(TargetX - transform.position.x) <= 0.2f)
            {
                int r = Random.Range(CurrPos - 2, CurrPos + 2);
                r = Mathf.Clamp(r, 1, MatrixPositonAi.Length - 1);
                TargetX = r * Amount;
            }
            Debug.Log("Target : " + TargetX);
            Debug.Log("Not_Move");
        }
        else
        {
            Debug.Log("Move");
            if (Mathf.Sign(TargetX - transform.position.x) == -1)
            {
                isMoveLeft = true;
                isMoveRight = false;
            }
            else
            {
                isMoveLeft = false;
                isMoveRight = true;
            }
        }


    }

    #endregion
    public bool isChange()
    {
        if (Mathf.Abs(TargetX - transform.position.x) <= 0.2f)
        {

            return true;


        }
        else
        {
            return false;

        }
        return false;
    }

    public virtual void OnMoveToBall()
    {
        var a = (Ball)CtrlGamePlay.Ins.Ball;

        if ((CurrPos - PostionToPos(a.transform.position.x) != 0))
        {
            if (Mathf.Sign(CurrPos - PostionToPos(a.transform.position.x)) == 1)
            {
                isMoveLeft = false;
                isMoveRight = true;
            }
            else
            {
                isMoveLeft = true;
                isMoveRight = false;
            }
        }
        else
        {
            isMoveLeft = false;
            isMoveRight = false;
        }

    }

    public virtual void OnMoveHoop()
    {
        isMoveLeft = false;
        isMoveRight = true;
    }

    // Protect Hoop
    public virtual void OnTriggerMoveProtectHoop()
    {
        isMoveRight = true;
        isMoveLeft = false;
    }



    public virtual void OnMoveProtectHoop()
    {
        int i = Random.Range(0, 1000);
        if (5 < i && i < 30)
        {
            OnJumpRight();
        }

    }

    public virtual void OnEndProtectToHoop()
    {

    }

    public virtual void OnIdle()
    {
        isMoveLeft = false;
        isMoveRight = false;

    }

    // MoveBack Throw Ball

    protected float delay_move_Back_throw_ball = 0.5f;
    protected bool endActionMoveBackThrowBall = false;
    protected bool isComplte_move_Back_throw_ball = false;
    public virtual void OnMoveBackHaveBall()
    {

        if (isBall)
        {
            if (isComplte_move_Back_throw_ball)
                return;
            if (delay_move_Back_throw_ball > 0)
            {
                delay_move_Back_throw_ball -= Time.deltaTime;
            }
            else
            {
                if (Random.Range(0, 2) == 0)
                {
                    if (isComplte_move_Back_throw_ball)
                        return;
                    isComplte_move_Back_throw_ball = true;
                    isJump = true;
                    hasPullBall = true;
                }
                else
                {
                    if (isComplte_move_Back_throw_ball)
                        return;
                    StatusCurr = CharacterState.throw1;
                    isJumpGround = true;
                    isComplte_move_Back_throw_ball = true;
                }
            }

        }
        else
        {
            OnAction = null;
            EndActionMoveBackHaveBall();
        }
    }

    public virtual void OnStartMoveBackHaveBall()
    {
        if (CurrPos > 10)
        {
            isMoveLeft = true;
            isMoveRight = false;
        }
        else
        {
            isComplte_move_Back_throw_ball = false;
            delay_move_Back_throw_ball = 0.5f;
            isMoveLeft = false;
            isMoveRight = true;
        }

    }
    public virtual void EndActionMoveBackHaveBall()
    {

        changeStatus = true;
    }






 
    public virtual void OnMoveToPlayer()
    {
        var a = CtrlGamePlay.Ins.Player;
        if (Mathf.Abs(transform.position.x - a.transform.position.x) >= 0.5f)
        {
            if (Mathf.Sign(a.transform.position.x - transform.position.x) == 1)
            {
                isMoveLeft = false;
                isMoveRight = true;
            }
            else
            {
                isMoveLeft = true;
                isMoveRight = false;
            }
        }
        else
        {
            isMoveLeft = false;
            isMoveRight = false;
        }

    }
    public virtual void OnTriggerMoveToPlayer() 
    {
        
    }

    public virtual void OnJumpLeft()
    {

      

    }
    public virtual void OnTriggerJumpLeft()
    {
        var a = CtrlGamePlay.Ins.GetPlayer();
        var b = CtrlGamePlay.Ins.GetCPU();
        var c = CtrlGamePlay.Ins.GetBall();
        if ((a.CurrPos - c.CurrPos) >=0 && (a.CurrPos - c.CurrPos) <= 1)
        {
            isJump = false;
            isMoveRight = false;
            isMoveLeft = true;
        }
        else if((a.CurrPos - c.CurrPos) <=0 && (a.CurrPos - c.CurrPos) >= -1)
        {
            isJump = false;
            isMoveRight = false;
            isMoveLeft = true;
        }
        else
        {
            isJump = true;
            isMoveRight = false;
            isMoveLeft = true;
        }
        


     
    }
    public virtual void OnJumpRight()
    {

        isMoveRight = true;
        isMoveLeft = false;

    }
    public virtual void OnJumpStraight()
    {

        isMoveRight = false;
        isMoveLeft = false;

    }


    public virtual void OnJump()
    {
        // Debug.Log("Jump");
        hasPullBall = false;
      if(isGround)
        isJump = true;
    }

    public virtual void OnJumpThrowBall()
    {
        hasPullBall = true;
        
        isJump = true;
        int r = Random.Range(0, 3);
        if (r == 0)
        {
            isMoveLeft = false;
            isMoveRight = false;
        }
        else if (r == 1)
        {
            isMoveLeft = true;
            isMoveRight = false;
        }
        else
        {
            isMoveLeft = false;
            isMoveRight = true;
        }
       

    }
    public virtual void OnStartThrowBall()
    {
       
    }
    
    public virtual void ResetJump()
    {
        isMoveRight = false;
        isMoveLeft = false;
        isJump = false;
    }




    public virtual void OnStun()
    {
        Velocity = new Vector2(0,Body.velocity.y);
        isAction = true;
      //  StatusCurr = CharacterState.stun;
        isStun = true;
        isMoveLeft = false;
        isMoveRight = false;
        isJump = false;


    }

   
    public virtual void OnTriggerStun()
    {
        Velocity = Vector3.zero;
        Force_Back();
       
    }

    public virtual void OnEndTriggerStun()

    {
        isStun = false;
        isAction = false;

    }
    
    public virtual void OnAttack()
    {
      
        if (isGround)
        {
            if (!isJumpGround)
            {
                if (delay <= 0)
                {
                    delay = 0.4f;
                    Debug.Log("Attack");
                    StatusCurr = CharacterState.swing;
                    isActiveHand = true;
                }
                else
                {
                    delay -= Time.deltaTime;

                }

            }

        }

    }
    public virtual void OnStartThrowOnEarth()
    {
        isJumpGround = true;
        StatusCurr = CharacterState.throw1;
    }

    public virtual void OnUpdateThrowOnEarth()
    {
        isMoveLeft = false;
        isMoveRight = false;
    }

    public virtual void OnEndThrowOnEarth()
    {
        isMoveLeft = false;
        isMoveRight = true;
    }



    public  override void Force_Back()
    {
      //  Body.velocity = Vector3.zero;
     
        if (isBall)
        {
            var Ball = CtrlGamePlay.Ins.Ball;

            isBall = false;

            Ball.transform.transform.parent = null;
            Ball.GetComponent<CircleCollider2D>().isTrigger = false;
            Ball.Body.isKinematic = false;
            Ball.Body.simulated = true;
            Ball.Body.AddForce(Vector2.up, ForceMode2D.Force);
        }

    }

   
    private float delay = 0.5f;
    private bool change = false;
    public virtual void OnActtionTriggerCatchBall()
    {
        if (isGround)
        {
            if (delay <= 0)
            {

                delay = 0.5f;
                isActiveHand = true;
                StatusCurr = CharacterState.swing;
                   
                
            }
            else
            {

                delay -= Time.deltaTime;
               
            }
           

        }
    }
    

    public virtual void OnActtionTriggerAttack()
    {

        if (delay <= 0)
        {
            delay = 0.6f;
            if (isGround)
            {
                if (!isJumpGround)
                {
                    StatusCurr = CharacterState.swing;
                    isActiveHand = true;
                    Debug.Log("attackPlayer");
                }
               
            }
        }
        else
        {
            delay -= Time.deltaTime;
        }
       
    }

    public virtual void OnStartActionAttack()
    {
        delay = 0;
       
       
       
      
    }
    public virtual void OnStartActionCatchBall()
    {
          
        
    }
    public virtual void EndAction()
    {

        changeStatus = true;
        KeyActionCurr = "";
       
    }



    public virtual void Endjump()
    {
        isMoveRight = true;
        changeStatus = true;
        isMoveLeft = false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// 
    #region AnimtionJump





    public virtual void OnActionSlampDunk()
    {
        if (i < ArrayAction.Length)
        {
            if (Vector3.Distance(ArrayAction[i].transform.position, transform.position) == 0)
            {

                ArrayAction[i].SetAction();
                i++;
            }
            else
            {

                transform.position = Vector3.MoveTowards(transform.position, ArrayAction[i].transform.position, Time.deltaTime * ArrayAction[i].time);
            }

        }

    }

    public virtual void OnTriggerSlampDunk()
    {
        isAction = true;
        StatusCurr = CharacterState.jumb2_slamdunk;
        isComplete = false;
        i = 0;
        Body.simulated = false;
        isMoveLeft = false;
        isMoveRight = false;

        Body.constraints = RigidbodyConstraints2D.FreezePositionY;
    }





    #endregion




    #region ActionKey

    public virtual void Init()
    {

       
        ActionGame lc_OnActionMoveToPlayer = new ActionGame(OnTriggerMoveToPlayer, OnMoveToPlayer);
        ActionGame lc_OnActionMove = new ActionGame(null, OnMoveToBall);
        ActionGame lc_OnJump = new ActionGame(OnJump, OnJumpStraight, Endjump, 0.75f);
        ActionGame lc_OnIdle = new ActionGame(null, OnMoveIde, EndAction, 0.5f);
        ActionGame lc_OnMoveRandom = new ActionGame(null, OnMoveRandom);
        ActionGame lc_onMoveToHoop = new ActionGame(OnTriggerMoveToHoop, OnMoveToHoop);
        ActionGame lc_OnJump_left = new ActionGame(OnTriggerJumpLeft, OnJumpLeft, Endjump, 1f);
        ActionGame lc_OnJump_right = new ActionGame(OnJump, OnJumpRight, Endjump, 1f);
        ActionGame lc_OnSlampDunk = new ActionGame(OnTriggerSlampDunk, OnActionSlampDunk, null, 2, true);
        ActionGame lc_OnStun = new ActionGame(OnTriggerStun, OnStun, OnEndTriggerStun, 0);

        ActionGame lc_OnAttack = new ActionGame(OnStartActionAttack, OnActtionTriggerAttack, null);
        ActionGame lc_OnCatchBall = new ActionGame(OnStartActionCatchBall, OnActtionTriggerCatchBall, null);

        ActionGame lc_OnThrowOnGround = new ActionGame(OnStartThrowOnEarth, OnUpdateThrowOnEarth, OnEndThrowOnEarth, 0.5f);

        ActionGame lc_OnActionMoveToBall = new ActionGame(null, OnMoveToBall, EndAction, 1);

        ActionGame lc_OnMoveBackHaveBall = new ActionGame(OnStartMoveBackHaveBall, OnMoveBackHaveBall, EndActionMoveBackHaveBall, 2.5f);

        ActionGame lc_OnActionJumpThrowBall = new ActionGame(OnJumpThrowBall, OnStartThrowBall, EndAction, 1);

        ActionGame lc_OnActionMoveProtectBall = new ActionGame(OnTriggerMoveProtectHoop, OnMoveProtectHoop, OnEndProtectToHoop, 4);

        // Update To Directory



        //  Update
        Directory_OnActionGame.Add(Key_Move_To_Ball, lc_OnActionMove);
        Directory_OnActionGame.Add(Key_Move_Ide, lc_OnIdle);
        Directory_OnActionGame.Add(Key_Move_Random, lc_OnMoveRandom);
        Directory_OnActionGame.Add(Key_Move_To_Hoop, lc_onMoveToHoop);
        Directory_OnActionGame.Add(Key_Stun, lc_OnStun);
        Directory_OnActionGame.Add(Key_Move_To_Player, lc_OnActionMoveToPlayer);




        //  Action

        Directory_OnActionGame.Add(Key_Action_Move_To_Ball, lc_OnActionMoveToBall);
        Directory_OnActionGame.Add(Key_Jump_Straight, lc_OnJump);
        Directory_OnActionGame.Add(Key_Jump_Left, lc_OnJump_left);
        Directory_OnActionGame.Add(Key_Jump_Right, lc_OnJump_right);
        Directory_OnActionGame.Add(Key_Thown_On_Ground, lc_OnThrowOnGround);
        Directory_OnActionGame.Add(Key_Action_Move_Back_Have_Ball, lc_OnMoveBackHaveBall);
        Directory_OnActionGame.Add(Key_Action_Jump_Throw_Ball, lc_OnActionJumpThrowBall);
        Directory_OnActionGame.Add(Key_Action_Protect_Ball, lc_OnActionMoveProtectBall);

        // Action EFF

        Directory_OnActionGame.Add(Key_Slamp_Dunk, lc_OnSlampDunk);

        // ACtion Trigger
        Directory_OnActionGame.Add(Key_Action_Trigger_Attack, lc_OnAttack);
        Directory_OnActionGame.Add(Key_Action_Trigger_CatchBall, lc_OnCatchBall);



        // Status

        Directory_StatusCpu.Add(Key_Trigger_Have_Ball, 0);
        Directory_StatusCpu.Add(Key_Trigger_Front, 0);
        Directory_StatusCpu.Add(Key_Trigger_Back, 0);
        Directory_StatusCpu.Add(Key_Trigger_Jump, 0);
        Directory_StatusCpu.Add(Key_Trigger_ThrowBall, 0);
        Directory_StatusCpu.Add(Key_Trigger_Slamp_Dunk, 0);

        // Load Text Cpu

        ProcessKey KeyMoveToBall = new ProcessKey(OnTriggerStatusMoveCatchBall, ProcessStatus);
        ProcessKey KeyProcessStatusHaveBall = new ProcessKey(OnTriggerCpuHaveBall, ProcessStatusCPUHaveBall);
        ProcessKey KeyProcessStatusPlayerHave = new ProcessKey(OnTriggerPlayerHaveBall, ProcessStatusPlayerHaveBall);
        ListProcessKey.Add(KeyMoveToBall);
        ListProcessKey.Add(KeyProcessStatusHaveBall);
        ListProcessKey.Add(KeyProcessStatusPlayerHave);

        ReadFileCPU();



    }

    //public virtual void Init()
    //{
    //    // Initialize Action With Key

    //    ActionGame lc_OnActionMoveToPlayer = new ActionGame(null, OnMoveToPlayer);
    //    ActionGame lc_OnActionMove = new ActionGame(null, OnMoveToBall);
    //    ActionGame lc_OnJump = new ActionGame(OnJump, OnJumpStraight, Endjump, 1.5f);
    //    ActionGame lc_OnIdle = new ActionGame(null, OnMoveIde,EndAction,0.5f);
    //    ActionGame lc_OnMoveRandom = new ActionGame(null, OnMoveRandom);
    //    ActionGame lc_onMoveToHoop = new ActionGame(null, OnMoveToHoop);
    //    ActionGame lc_OnJump_left = new ActionGame(OnTriggerJumpLeft, OnJumpLeft, Endjump, 1.7f);
    //    ActionGame lc_OnJump_right = new ActionGame(OnJump, OnJumpRight, Endjump, 1.7f);
    //    ActionGame lc_OnSlampDunk = new ActionGame(OnTriggerSlampDunk, OnActionSlampDunk, null, 100f, true);
    //    ActionGame lc_OnStun = new ActionGame(OnTriggerStun, OnStun, OnEndTriggerStun, 0);

    //    ActionGame lc_OnAttack = new ActionGame(OnStartActionAttack, OnActtionTriggerAttack, null);
    //    ActionGame lc_OnCatchBall = new ActionGame(OnStartActionCatchBall, OnActtionTriggerCatchBall, null);

    //    ActionGame lc_OnThrowOnGround = new ActionGame(OnStartThrowOnEarth,OnUpdateThrowOnEarth,OnEndThrowOnEarth,0.5f);

    //    ActionGame lc_OnActionMoveToBall = new ActionGame(null, OnMoveToBall, EndAction, 1);

    //    ActionGame lc_OnMoveBackHaveBall = new ActionGame(OnStartMoveBackHaveBall, OnMoveBackHaveBall, EndActionMoveBackHaveBall, 2.5f);

    //    ActionGame lc_OnActionJumpThrowBall = new ActionGame(OnJumpThrowBall, OnStartThrowBall, EndAction, 1);

    //    ActionGame lc_OnActionMoveProtectBall = new ActionGame(OnTriggerMoveProtectHoop, OnMoveProtectHoop, OnEndProtectToHoop, 4);

    //    // Update To Directory



    //    //  Update
    //    Directory_OnActionGame.Add(Key_Move_To_Ball, lc_OnActionMove);
    //    Directory_OnActionGame.Add(Key_Move_Ide, lc_OnIdle);
    //    Directory_OnActionGame.Add(Key_Move_Random, lc_OnMoveRandom);
    //    Directory_OnActionGame.Add(Key_Move_To_Hoop, lc_onMoveToHoop);
    //    Directory_OnActionGame.Add(Key_Stun, lc_OnStun);
    //    Directory_OnActionGame.Add(Key_Move_To_Player, lc_OnActionMoveToPlayer);
        
      


    //    //  Action
      
    //    Directory_OnActionGame.Add(Key_Action_Move_To_Ball, lc_OnActionMoveToBall);
    //    Directory_OnActionGame.Add(Key_Jump_Straight, lc_OnJump);
    //    Directory_OnActionGame.Add(Key_Jump_Left, lc_OnJump_left);
    //    Directory_OnActionGame.Add(Key_Jump_Right, lc_OnJump_right);
    //    Directory_OnActionGame.Add(Key_Thown_On_Ground, lc_OnThrowOnGround);
    //    Directory_OnActionGame.Add(Key_Action_Move_Back_Have_Ball, lc_OnMoveBackHaveBall);
    //    Directory_OnActionGame.Add(Key_Action_Jump_Throw_Ball, lc_OnActionJumpThrowBall);
    //    Directory_OnActionGame.Add(Key_Action_Protect_Ball, lc_OnActionMoveProtectBall);

    //    // Action EFF

    //    Directory_OnActionGame.Add(Key_Slamp_Dunk, lc_OnSlampDunk);

    //    // ACtion Trigger
    //    Directory_OnActionGame.Add(Key_Action_Trigger_Attack, lc_OnAttack);
    //    Directory_OnActionGame.Add(Key_Action_Trigger_CatchBall, lc_OnCatchBall);



    //    // Status

    //    Directory_StatusCpu.Add(Key_Trigger_Have_Ball, 0);
    //    Directory_StatusCpu.Add(Key_Trigger_Front, 0);
    //    Directory_StatusCpu.Add(Key_Trigger_Back, 0);
    //    Directory_StatusCpu.Add(Key_Trigger_Jump, 0);
    //    Directory_StatusCpu.Add(Key_Trigger_ThrowBall, 0);
    //    Directory_StatusCpu.Add(Key_Trigger_Slamp_Dunk, 0);
      

    //    // Load Text Cpu

    //    ProcessKey KeyMoveToBall = new ProcessKey(OnTriggerStatusMoveCatchBall,ProcessStatus);
    //    ProcessKey KeyProcessStatusHaveBall = new ProcessKey(OnTriggerCpuHaveBall, ProcessStatusCPUHaveBall);
    //    ProcessKey KeyProcessStatusPlayerHave = new ProcessKey(OnTriggerPlayerHaveBall, ProcessStatusPlayerHaveBall);
    //    ListProcessKey.Add(KeyMoveToBall);
    //    ListProcessKey.Add(KeyProcessStatusHaveBall);
    //    ListProcessKey.Add(KeyProcessStatusPlayerHave);

    //    ReadFileCPU();


    //}


   

    public virtual void OnTriggerStatusMoveCatchBall()
    {
        var ball = (Ball)CtrlGamePlay.Ins.Ball;
        ball.OnPercitionBall_0();
        BoxAttack.gameObject.SetActive(false);
        BoxCatchBall.gameObject.SetActive(true);
        OnActionTrigger = null;
    }
    public virtual void OnTriggerCpuHaveBall()
    {
        var ball = (Ball)CtrlGamePlay.Ins.Ball;
        ball.OffPercitionBall();
        BoxCatchBall.gameObject.SetActive(false);
        BoxAttack.gameObject.SetActive(false);
        OnActionTrigger = null;
    }
    public virtual void OnTriggerPlayerHaveBall()
    {
        var ball = (Ball)CtrlGamePlay.Ins.Ball;
        ball.OffPercitionBall();
        BoxCatchBall.gameObject.SetActive(false);
        BoxAttack.gameObject.SetActive(true);
    }
    public string KeyCurr()
    {

        string key = Directory_StatusCpu[Key_Trigger_Jump].ToString() + Directory_StatusCpu[Key_Trigger_Front].ToString()
               + Directory_StatusCpu[Key_Trigger_Back].ToString();
        TextStatus.text = key;

        return key;
    }
    public string KeyCurr_Player_Have_Ball()
    {

        string key = Directory_StatusCpu[Key_Trigger_Front].ToString()+ Directory_StatusCpu[Key_Trigger_Back].ToString();
               
        TextStatus.text = key;

        return key;
    }


    public string KeyCurr_Status_Have_Ball()
    {
       
        string key = Directory_StatusCpu[Key_Trigger_Front].ToString() + Directory_StatusCpu[Key_Trigger_Back].ToString() + Directory_StatusCpu[Key_Trigger_ThrowBall].ToString();
        TextStatus.text = key;
        return key;
    }


    public void ProcessStatus()
    {
        string key = KeyCurr();
        KeyActionCurr = key;
        if (changeStatus)
        {
            Directory_Action_Process_Key[KeyCurrBall].TriggerAction();
          //  Directory_Action_Process_Key[KeyCurrBall].StartAction();
            KeyActionCurr = "";
            changeStatus = false;
        }


        if (KeyActionCurr != KeyActionPrevious)
        {
            OnActionWithKey(key);
        }




        //if (key == "00000")
        //{
        //    StartActionUpdate(null, OnMoveToBall);
        //}
        //else if(key == "10000")
        //{
        //    StartActionUpdate(null,OnMoveIde);
        //}
        KeyActionPrevious = KeyActionCurr;


    }
    public void ProcessStatusCPUHaveBall()
    {
       string  key = KeyCurr_Status_Have_Ball();
         KeyActionCurr = key;

        if (changeStatus)
        {
            Directory_Action_Process_Key[KeyCurrBall].TriggerAction();
            OnAtionWithKey_Status_Have_Ball(key);
            KeyActionCurr = "";
          changeStatus = false;
        }
        if (KeyActionCurr != KeyActionPrevious)
        {
            OnAtionWithKey_Status_Have_Ball(key);
        }
        KeyActionPrevious = KeyActionCurr;
    }

    public void ProcessStatusPlayerHaveBall()
    {
       


       string  key = KeyCurr_Player_Have_Ball();
        KeyActionCurr = key;
        if (changeStatus)
        {
            Directory_Action_Process_Key[KeyCurrBall].TriggerAction();
            KeyActionCurr = "";
            changeStatus = false;
        }
        if (KeyActionCurr != KeyActionPrevious)
        {
            OnAtionWithKey_Status_Player_Have_Ball(key);
        }
        KeyActionPrevious = KeyActionCurr;
    }



    public virtual void ReadFileCPU()
    {
      
        string text = textCPU.text;
        string[] w = new string[1] { "--Staus--" };
        string[] sss = text.Split(w,System.StringSplitOptions.None);

         for(int i = 0; i < sss.Length; i++)
        {
            //Debug.Log("String : "+sss[i]);
        }

        string s = sss[1];
        string s1 = sss[2];
        string s2 = sss[3];
        string s3 = sss[0];


        StringReader strRead = new StringReader(s);
        StringReader strRead_1 = new StringReader(s1);
        StringReader strRead_2 = new StringReader(s2);
        StringReader strRead_3 = new StringReader(s3);
        while (true)
        {

            string line = strRead.ReadLine();
            if (line != null)
            {
                if (line.StartsWith("//") || line == "")
                {
                    continue;
                }

                FormatString_Status_Move_To_Ball(line);
                //   Debug.Log(line);

            }
            else
            {

                break;
            }
        }

        while (true)
        {

            string line = strRead_1.ReadLine();
            if (line != null)
            {
                if (line.StartsWith("//") || line == "")
                {
                    continue;
                }

                FormatString_Status_Have_Ball(line);
                //   Debug.Log(line);

            }
            else
            {

                break;
            }
        }
        while (true)
        {

            string line = strRead_2.ReadLine();
            if (line != null)
            {
                if (line.StartsWith("//") || line == "")
                {
                    continue;
                }

                FormatString_Status_Player_Have_Ball(line);
                //   Debug.Log(line);

            }
            else
            {

                break;
            }
        }

        while (true)
        {

            string line = strRead_3.ReadLine();
            if (line != null)
            {
                if (line.StartsWith("//") || line == "")
                {
                    continue;
                }

                FormatString_Process_Status_Key(line);
                //   Debug.Log(line);

            }
            else
            {

                break;
            }
        }



    }
    protected void FormatString_Process_Status_Key(string line)
    {
        string ss = "";
        // Debug.Log("Line Format : " + line);
        string[] w = new string[1] { "." };
        string[] w1 = new string[1] { "=" };
        string[] w2 = new string[1] { "||" };
        string[] word = line.Split(w, System.StringSplitOptions.None);

        for (int i = 0; i < word.Length; i++)
        {
            //    Debug.Log(word[i]);
        }
        string[] word2 = word[1].Split(w1, System.StringSplitOptions.None);
        string key = word2[0].Trim();

        string key_1 = word2[1].Trim();

     
        int index = int.Parse(key_1);

        //string[] value = word2[1].Split(w2, System.StringSplitOptions.None);
        //string[] ArrayValue = new string[value.Length];
        //for (int i = 0; i < value.Length; i++)
        //{
        //    ArrayValue[i] = value[i].Trim();
        //    ss += " " + ArrayValue[i];

        //}

        //ActionGame[] ArrayAction = new ActionGame[value.Length];

        //for (int i = 0; i < ArrayValue.Length; i++)
        //{
        //    //    Debug.Log(ArrayValue[i]);
        //    ArrayAction[i] = Directory_OnActionGame[ArrayValue[i]];
        //}


        Directory_Action_Process_Key.Add(key, ListProcessKey[index]);

        //  Debug.Log("Key : "+key + " " + ss);

    }

    protected void FormatString_Status_Move_To_Ball(string line)
    {
        string ss = "";
        // Debug.Log("Line Format : " + line);
        string[] w = new string[1] { "." };
        string[] w1 = new string[1] { "=" };
        string[] w2 = new string[1] { "||" };
        string[] word = line.Split(w, System.StringSplitOptions.None);

        for (int i = 0; i < word.Length; i++)
        {
            //    Debug.Log(word[i]);
        }
        string[] word2 = word[1].Split(w1, System.StringSplitOptions.None);
        string key = word2[0].Trim();
        string[] value = word2[1].Split(w2, System.StringSplitOptions.None);
        string[] ArrayValue = new string[value.Length];
        for (int i = 0; i < value.Length; i++)
        {
            ArrayValue[i] = value[i].Trim();
            ss += " " + ArrayValue[i];

        }

        ActionGame[] ArrayAction = new ActionGame[value.Length];

        for (int i = 0; i < ArrayValue.Length; i++)
        {
            //    Debug.Log(ArrayValue[i]);
            ArrayAction[i] = Directory_OnActionGame[ArrayValue[i]];
        }


        Directory_Key_Status.Add(key, ArrayAction);

        //  Debug.Log("Key : "+key + " " + ss);

    }

    protected void FormatString_Status_Have_Ball(string line)
    {
        string ss = "";
        // Debug.Log("Line Format : " + line);
        string[] w = new string[1] { "." };
        string[] w1 = new string[1] { "=" };
        string[] w2 = new string[1] { "||" };
        string[] word = line.Split(w, System.StringSplitOptions.None);

        for (int i = 0; i < word.Length; i++)
        {
            //    Debug.Log(word[i]);
        }
        string[] word2 = word[1].Split(w1, System.StringSplitOptions.None);
        string key = word2[0].Trim();
        string[] value = word2[1].Split(w2, System.StringSplitOptions.None);
        string[] ArrayValue = new string[value.Length];
        for (int i = 0; i < value.Length; i++)
        {
            ArrayValue[i] = value[i].Trim();
            ss += " " + ArrayValue[i];

        }

        ActionGame[] ArrayAction = new ActionGame[value.Length];

        for (int i = 0; i < ArrayValue.Length; i++)
        {
            //    Debug.Log(ArrayValue[i]);
            ArrayAction[i] = Directory_OnActionGame[ArrayValue[i]];
        }


        Directory_Key_Status_Have_Ball.Add(key, ArrayAction);

        //  Debug.Log("Key : "+key + " " + ss);

    }

    protected void FormatString_Status_Player_Have_Ball(string line)
    {
        
        string ss = "";
        // Debug.Log("Line Format : " + line);
        string[] w = new string[1] { "." };
        string[] w1 = new string[1] { "=" };
        string[] w2 = new string[1] { "||" };
        string[] word = line.Split(w, System.StringSplitOptions.None);

        for (int i = 0; i < word.Length; i++)
        {
            //    Debug.Log(word[i]);
        }
        string[] word2 = word[1].Split(w1, System.StringSplitOptions.None);
        string key = word2[0].Trim();
        string[] value = word2[1].Split(w2, System.StringSplitOptions.None);
        string[] ArrayValue = new string[value.Length];
        for (int i = 0; i < value.Length; i++)
        {
            ArrayValue[i] = value[i].Trim();
            ss += " " + ArrayValue[i];

        }

        ActionGame[] ArrayAction = new ActionGame[value.Length];

        for (int i = 0; i < ArrayValue.Length; i++)
        {
            //    Debug.Log(ArrayValue[i]);
            ArrayAction[i] = Directory_OnActionGame[ArrayValue[i]];
        }


        Directory_Key_Status_Player_Have_Ball.Add(key, ArrayAction);

        //  Debug.Log("Key : "+key + " " + ss);

    }


    public void OnActionWithKey(string key)
    {
       
        Debug.Log(key);

        int r = Random.Range(0, Directory_Key_Status[key].Length);
        ActionGame a = Directory_Key_Status[key][r];
        switch (a.typeAction)
        {
            case ActionGame.TypeAction.Action:

                StartAcionWithTime(a.ActionStart, a.ActionUpdate, a.ActionRemove, a.time);

                break;
            case ActionGame.TypeAction.Update:

                StartActionUpdate(a.ActionStart, a.ActionUpdate);
                break;

            case ActionGame.TypeAction.ActionEFF:

                StartAcionEFFWithTime(a.ActionStart, a.ActionUpdate, a.ActionRemove, a.time);

                break;
            case ActionGame.TypeAction.ActionTrigger:

                StartActionTrigger(a.ActionStart, a.ActionUpdate);

                break;


        }
    }
   
    public void OnAtionWithKey_Status_Have_Ball(string key)
    {
     //   Debug.Log(key);

        int r = Random.Range(0, Directory_Key_Status_Have_Ball[key].Length);
        ActionGame a = Directory_Key_Status_Have_Ball[key][r];
        switch (a.typeAction)
        {
            case ActionGame.TypeAction.Action:

                StartAcionWithTime(a.ActionStart, a.ActionUpdate, a.ActionRemove, a.time);

                break;
            case ActionGame.TypeAction.Update:

                StartActionUpdate(a.ActionStart, a.ActionUpdate);
                break;

            case ActionGame.TypeAction.ActionEFF:

                StartAcionEFFWithTime(a.ActionStart, a.ActionUpdate, a.ActionRemove, a.time);

                break;
            case ActionGame.TypeAction.ActionTrigger:

                StartActionTrigger(a.ActionStart, a.ActionUpdate);

                break;


        }
    }

    public void OnAtionWithKey_Status_Player_Have_Ball(string key)
    {
        Debug.Log(key);

        int r = Random.Range(0, Directory_Key_Status_Player_Have_Ball[key].Length);
        ActionGame a = Directory_Key_Status_Player_Have_Ball[key][r];
        switch (a.typeAction)
        {
            case ActionGame.TypeAction.Action:

                StartAcionWithTime(a.ActionStart, a.ActionUpdate, a.ActionRemove, a.time);

                break;
            case ActionGame.TypeAction.Update:

                StartActionUpdate(a.ActionStart, a.ActionUpdate);
                break;

            case ActionGame.TypeAction.ActionEFF:

                StartAcionEFFWithTime(a.ActionStart, a.ActionUpdate, a.ActionRemove, a.time);

                break;
            case ActionGame.TypeAction.ActionTrigger:

                StartActionTrigger(a.ActionStart, a.ActionUpdate);

                break;


        }
    }


    public void RunActionTrigger(string key)
    {
        ActionGame a = Directory_OnActionGame[key];
        StartActionTrigger(a.ActionStart, a.ActionUpdate);

    }


    public void OnActionEFFWithKey(string key)
    {
        ActionGame a = Directory_OnActionGame[key];
        StartAcionEFFWithTime(a.ActionStart, a.ActionUpdate, a.ActionRemove, a.time);

    }



    public void RemoveActionTriggerWithKey(string key)
    {
        ActionGame a = Directory_OnActionGame[key];
        switch (a.typeAction)
        {
            case ActionGame.TypeAction.Action:



                break;
            case ActionGame.TypeAction.Update:


                break;

            case ActionGame.TypeAction.ActionEFF:


                break;
            case ActionGame.TypeAction.ActionTrigger:

                RemoveActionTrigger(a.ActionUpdate);

                break;


        }

    }

    public void RemoveActionTrigger(System.Action eventUpdate)
    {
        OnActionTrigger = null;
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




    public void StartAcionEFFWithTime(System.Action ActionTrigger, System.Action ActionUpdate, System.Action ActionRemove, float time)
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


        SetupTimerActionEFF(time, UpdateAction, expireAction);

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

    public void StartActionTrigger(System.Action ActionTrigger, System.Action ActionUpdate)
    {
        if (ActionTrigger != null)
        {
            ActionTrigger();
        }
        OnActionTrigger += ActionUpdate;

    }




    public ActionGame GetAction(string key)
    {
        if (Directory_OnActionGame.ContainsKey(key))
        {
            return Directory_OnActionGame[key];
        }
        else
        {
            return null;
        }

        return null;
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





    System.Action SetupTimerActionEFF(float seconds, System.Action<float> updateAction, System.Action expireAction)
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

        OnActionEFF += updateWrapper;



        expireWrapper = () =>
        {
            OnActionEFF -= updateWrapper;



            if (expireAction != null)
            {
                KeyActionCurr = "";
                expireAction();
            }
        };

        return expireWrapper;
    }



    public void SetKeyTrigger(string key)
    {
        Directory_StatusCpu[key] = 1;
    }

    public void SetRestoreTrigger(string key)
    {
        Directory_StatusCpu[key] = 0;
    }

    public void RestoreAll()
    {
        Directory_StatusCpu[Key_Trigger_Back] = 0;
        Directory_StatusCpu[Key_Trigger_Front] = 0;
        Directory_StatusCpu[Key_Trigger_Have_Ball] = 0;
        Directory_StatusCpu[Key_Trigger_Jump] = 0;
        Directory_StatusCpu[Key_Trigger_ThrowBall] = 0;
        Directory_StatusCpu[Key_Trigger_Board_CPU] = 0;
    }



    #endregion
    //void RemoveListener(string name, System.Action action)
    //{
    //    if (registry.ContainsKey(name) == false)
    //    {
    //        return;
    //    }

    //    registry[name].listeners -= action;
    //}

    //void AddListener(string name, System.Action action)
    //{
    //    if (registry.ContainsKey(name) == false)
    //    {
    //        registry[name] = new RegistryItem();
    //    }

    //    registry[name].listeners += action;
    //}


    public override void OnEvent(TrackEntry trackEntry, Spine.Event e)
    {
        if (trackEntry.Animation.Name == "swing")
        {
            var evenName = e.Data.Name;
            if (evenName == "atk")
            {
                BoxHurt.gameObject.SetActive(true);
                canHurt = true;
            }

        }

        if (trackEntry.Animation.Name == "throw1")
        {
            var evenName1 = e.Data.Name;

            if (evenName1 == "move")
            {
                Body.velocity = new Vector2(Body.velocity.x, ForceJumpOnGround);
            }

            if (evenName1 == "atk")
            {
                if (isBall)
                {
                    CtrlGamePlay.Ins.CpuThrowBall();
                }
                Debug.Log("ThrowBall");

            }
        }


    }
    public override void OnComplete(TrackEntry trackEntry)
    {
        if (trackEntry.Animation.Name == "swing")
        {
           
            isActiveHand = false;
        }
        if (trackEntry.Animation.Name == "throw1")
        {
            isJumpGround = false; 
           
        }
    }


    protected int Distance_Player_CPU()
    {
        var player = CtrlGamePlay.Ins.Player;
        var cpu = CtrlGamePlay.Ins.AI;
        return Mathf.Abs(player.CurrPos - cpu.CurrPos);
    }





    #region EVENT
    public void Event_Reset()
    {
        changeStatus = true;
        ChangeStatus = true;
        KeyActionCurr = "";
        KeyActionPrevious = "";
        KeyActionTriggerCurr = "";
        KeyActionTriggerPrevious = "";
        Body.simulated = true;
        StatusCurr = CharacterState.idle;
        OnActionEFF = null;
        OnAction = null;
        OnMove = null;
        isMoveLeft = false;
        isMoveRight = false;
        isJump = false;
        isAction = false;
        isBall = false;
        isPullBall = false;
        isStun = false;
        ArrayAction = new Action[0];
        Body.constraints = RigidbodyConstraints2D.None;
        Body.constraints = RigidbodyConstraints2D.FreezeRotation;
       
    }
    #endregion

    private void OnDrawGizmos()
    {
        
        Gizmos.DrawLine(Vector3.left*0.5f+transform.position - Vector3.up * 0.5f, (transform.position - Vector3.up * 0.5f)+ Vector3.left*1.4f );;
    }


    public void NullOnActionTrigger()
    {
        OnActionTrigger = null;
    }
    public void NullAction()
    {
        OnAction = null;
    }
}
public class ProcessKey
{
   public  ProcessKey(System.Action TriggerAction, System.Action StartAction)
    {
        this.TriggerAction = TriggerAction;
        this.StartAction = StartAction;
    }
    public System.Action TriggerAction;
    public System.Action StartAction;

}


public class ActionGame
{
    public enum TypeAction { Update, Action ,ActionEFF,ActionTrigger};
    public TypeAction typeAction;
    public bool isAction;
    public System.Action ActionUpdate;
    public System.Action ActionRemove;
    public System.Action ActionStart;
    public float time;

    public ActionGame(System.Action ActionStart, System.Action UpdateAction)
    {
        typeAction = TypeAction.Update;
        ActionUpdate = UpdateAction;
        this.ActionStart = ActionStart;

    }
    public ActionGame(System.Action ActionStart, System.Action UpdateAction, System.Action RemoveAction, float time)
    {
        this.ActionStart = ActionStart;
        typeAction = TypeAction.Action;
        ActionUpdate = UpdateAction;
        ActionRemove = RemoveAction;
        this.time = time;
    }
    public ActionGame(System.Action ActionStart, System.Action UpdateAction, System.Action RemoveAction, float time,bool isActionEff)
    {
        this.ActionStart = ActionStart;
        typeAction = TypeAction.ActionEFF;
        ActionUpdate = UpdateAction;
        ActionRemove = RemoveAction;
        this.time = time;
    }

    public ActionGame(System.Action ActionStart, System.Action UpdateAction,System.Action RemoveAction)
    {
        typeAction = TypeAction.ActionTrigger;
        this.ActionStart = ActionStart;
        this.ActionUpdate = UpdateAction;
        this.ActionRemove = RemoveAction;
    }





   
   
}
class RegistryItem
{
    int _value;
    public int Value
    {
        get { return _value; }
        set
        {
            _value = value;
            if (listeners != null)
            {
                listeners();
            }
        }
    }

    public event System.Action listeners;
}