using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_07 :AI_06
{
    // Start is called before the first frame update
    public override void Start()
    {
        StatusCurr = CharacterState.idle;
        Body = GetComponent<Rigidbody2D>();

        if (AnimationHandle != null)
        {
            AnimStatus = AnimationHandle.AnimationState;
        }
        AnimStatus.Complete += OnComplete;
        AnimStatus.Start += OnStartAnim;
        AnimStatus.Interrupt += OnInterrupt;
        AnimStatus.Dispose += OnDispose;
        AnimStatus.Event += OnEvent;


        CtrlGamePlay.Ins.eventRestGamePlay += Reset;

        Amount = CtrlGamePlay.Ins.WidthScreen / CountSperateDistance;

        MatrixPositonAi = new int[CountSperateDistance];

        PosInit = (CtrlGamePlay.Ins.WidthScreen / 2) - 0.3f;
        TargetX = transform.position.x;

        TargetHoop = GameObject.FindGameObjectWithTag("TargetCPU").transform.position;
        xTargetHoop = GameObject.FindGameObjectWithTag("TargetHoop").transform.position.x;
        yTargetHoop = GameObject.FindGameObjectWithTag("TargetHoop").transform.position.x;

        CtrlGamePlay.Ins.eventRestGamePlay += Event_Reset;
        CtrlGamePlay.Ins.eventResetGame += Event_Reset;
        Init();
        this.speed = 5.5f;
        CtrlGamePlay.Ins.GetBall().AddKeyBall_2();

    }

    // Update is called once per frame
  
    //  Block Ball Player Have Ball

    private System.Action OnAction_01;
    private float timeOnAction_01;
    private float timeDelay;
    public void OnTriggerBlockPlayerThrow()
    {
        OnAction_01 = null;

        timeOnAction_01 = 0;



    }
    

    public void OnTriggerStealBall()
    {
        OnAction_01 = null;
        timeDelay = 0;
    }

    public void OnStartTriggerStealBall()
    {
        var player = CtrlGamePlay.Ins.GetPlayer();
        if (timeOnAction_01 > 0)
        {

            timeOnAction_01 -= Time.deltaTime;
            if (OnAction_01 != null)
                OnAction_01();
        }
        else
        {
            if (GetDistancePlayerAndCPU() <= 3)
            {
                if (DirectCpu == DirectWithPlayer.Right)
                {
                    if (!player.isGround)
                    {

                        if (isMoveLeft) 
                        {
                            MoveToPos(1);
                        }
                        else
                        {
                            if (timeDelay < 0)
                            {
                                timeDelay = 0.4f;
                                isJump = true;
                            }
                            else
                            {
                                timeDelay -= Time.deltaTime;
                            }
                        }
                       

                    }
                    else
                    {
                        MoveToPos(player.CurrPos);

                    }


                }
                else
                {

                    MoveToPos(player.CurrPos);

                }


            }
            else
            {
                MoveToPos(player.CurrPos);
            }
        }
    }

    

    public void OnEndTriggerStealBall()
    {

    }


    public void OnStartBlockPlayerThrow()
    {

        var player = CtrlGamePlay.Ins.GetPlayer();
        if (timeOnAction_01>0)
        {

            timeOnAction_01 -= Time.deltaTime;
            if(OnAction_01!=null)
            OnAction_01();
        }
        else
        {
            OnAction_01 = null;
           

            if (GetDistancePlayerAndCPU() <= 5)
            {
                if(DirectCpu == DirectWithPlayer.Right){

                    if (player.isMoveRight)
                    {
                        MoveToPos(player.CurrPos);

                        if (!player.isGround)
                        {
                            OnAction_01 += JumpLeft;
                            timeOnAction_01 = 0.3f;
                            isMoveRight = true;
                            isMoveLeft = false;
                        }
                    }
                    else if (player.isMoveLeft)
                    {

                        MoveToPos(1);
                    }
                    else
                    {
                        MoveToPos(player.CurrPos);
                        if (!player.isGround)
                        {
                            OnAction_01 += Jump;
                            timeOnAction_01 = 0.3f;
                            isMoveRight = false;
                            isMoveLeft = false;
                        }
                    }
                }
                else
                {
                    int i = 0;
                    if (i == Random.Range(0, 1))
                    {
                        OnAction_01 += MoveAndJump;
                    }
                    else
                    {
                        OnAction_01 += MoveAndJumpAndThrow;
                    }
                    timeOnAction_01 = 1;
                    isMoveRight = true;
                    isMoveLeft = false;

                }
            }
            else
            {
                MoveToPos(player.CurrPos);
                if (timeDelay < 0)
                {
                    timeDelay = 0.4f;
                    isJump = true;
                }
                else
                {
                    timeDelay -= Time.deltaTime;
                }
               
            }

        }
    }
    private void JumpLeft()
    {
       

    }
    private void Jump()
    {


    }


    public void MoveAndJump()
    {
        if (!CtrlGamePlay.Ins.GetPlayer().isGround || CtrlGamePlay.Ins.GetPlayer().StatusCurr == CharacterState.throw1)
        {
            isJump = true;
           
        }
    }

    public void MoveAndJumpAndThrow()
    {
        if (!CtrlGamePlay.Ins.GetPlayer().isGround || CtrlGamePlay.Ins.GetPlayer().StatusCurr == CharacterState.throw1)
        {
            isJump = true;
            isPullBall = true;
        }
    }

    public void OnEndBlockPlayerThrow()
    {
        ChangeStatus = true;
        changeStatus = true;
        KeyActionCurr = "";
    }



    // JumpDirect

    private Vector3 direct;

    public void JumpDirect()
    {
        var ball = CtrlGamePlay.Ins.Ball;
        Vector3 direct = Vector3.Normalize(ball.transform.position - transform.position);


    }

    public int GetDistanceBallAndPlayer()
    {
        return Mathf.Abs((CtrlGamePlay.Ins.Ball.CurrPos - CtrlGamePlay.Ins.Player.CurrPos));
    }
    public int GetDistanceBallAndCPU()
    {
        return Mathf.Abs((CtrlGamePlay.Ins.Ball.CurrPos - CtrlGamePlay.Ins.AI.CurrPos));
    }

    public int GetDistancePlayerAndCPU()
    {
        return Mathf.Abs((CtrlGamePlay.Ins.Player.CurrPos - CtrlGamePlay.Ins.AI.CurrPos));
    }

    public void OnTriggerJumpThrowBack()
    {

    }

    public void OnJumpThrowBack()
    {
        var player = CtrlGamePlay.Ins.GetPlayer();

        if (timeOnAction_01 > 0)
        {

            timeOnAction_01 -= Time.deltaTime;
            if (OnAction_01 != null)
                OnAction_01();
        }
        else
        {

            if(player.StatusCurr == CharacterState.swing)
            {
                isMoveRight = true;
                isMoveLeft = false;
                isJump = true;
                isThrowBall = true;
                timeOnAction_01 = 0.3f;
                OnAction_01 += Jump;
            }
            else
            {
                
                MoveToPos(12);
            }

           


        }

        if (!isBall)
        {
            NullAction();
           changeStatus = true;
           ChangeStatus = true;
        }
    }


    public const string Key_Action_Block_Ball = "Action_Block_Ball";
    public const string Key_Action_Jump_Throw_Back = "Action_Jump_Throw_Back";


    public override void Init()
    {
        Debug.Log("INIT PPSPSPS");
        ActionGame AcitonJumpThrowBack = new ActionGame(OnTriggerJumpThrowBack, OnJumpThrowBack, null, 1);
      
        ActionGame ActionBlocKPlayer = new ActionGame(OnTriggerBlockPlayerThrow, OnStartBlockPlayerThrow, OnEndBlockPlayerThrow, 1);

        ActionGame lc_OnActionMoveToPlayer = new ActionGame(OnTriggerMoveToPlayer, OnMoveToPlayer);
        ActionGame lc_OnActionMove = new ActionGame(null, OnMoveToBall);
        ActionGame lc_OnJump = new ActionGame(OnJump, OnJumpStraight, Endjump, 0.75f);
        ActionGame lc_OnIdle = new ActionGame(null, OnMoveIde, EndAction, 0.5f);
        ActionGame lc_OnMoveRandom = new ActionGame(null, OnMoveRandom);
        ActionGame lc_onMoveToHoop = new ActionGame(null, OnMoveToHoop);
        ActionGame lc_OnJump_left = new ActionGame(OnTriggerJumpLeft, OnJumpLeft, Endjump, 1f);
        ActionGame lc_OnJump_right = new ActionGame(OnJump, OnJumpRight, Endjump, 1f);
        ActionGame lc_OnSlampDunk = new ActionGame(OnTriggerSlampDunk, OnActionSlampDunk, OnEndSlampDunk, 2, true);
        ActionGame lc_OnStun = new ActionGame(OnTriggerStun, OnStun, OnEndTriggerStun, 0);

        ActionGame lc_OnAttack = new ActionGame(OnStartActionAttack, OnActtionTriggerAttack, null);
        ActionGame lc_OnCatchBall = new ActionGame(OnStartActionCatchBall, OnActtionTriggerCatchBall, null);

        ActionGame lc_OnThrowOnGround = new ActionGame(OnStartThrowOnEarth, OnUpdateThrowOnEarth, OnEndThrowOnEarth, 0.5f);

        ActionGame lc_OnActionMoveToBall = new ActionGame(null, OnMoveToBall, EndAction, 1);

        ActionGame lc_OnMoveBackHaveBall = new ActionGame(OnStartMoveBackHaveBall, OnMoveBackHaveBall, EndActionMoveBackHaveBall, 2.5f);

        ActionGame lc_OnActionJumpThrowBall = new ActionGame(OnJumpThrowBall, OnStartThrowBall, EndAction, 1);

        ActionGame lc_OnActionMoveProtectBall = new ActionGame(OnTriggerMoveProtectHoop, OnMoveProtectHoop, OnEndProtectToHoop, 2);

        ActionGame lc_OnCatchAndThrowBall = new ActionGame(OnTriggerCatchAndThrow, OnStartCatchAndThrow, null, 1);
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
        Directory_OnActionGame.Add(Key_Action_Jump_Throw_Back, AcitonJumpThrowBack);
        Directory_OnActionGame.Add(Key_Action_Block_Ball, ActionBlocKPlayer);
        Directory_OnActionGame.Add(Key_Action_Catch_And_Throw, lc_OnCatchAndThrowBall);

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
        Directory_StatusCpu.Add(Key_Trigger_Jump_Protect_Ball, 0);

        // Load Text Cpu

        ProcessKey ProcessProtectBoard = new ProcessKey(OnTriggerStatusMoveProtectBall, ProcessKey_Protect_Board);

        ProcessKey KeyMoveToBall = new ProcessKey(OnTriggerStatusMoveCatchBall, ProcessStatus);
        ProcessKey KeyProcessStatusHaveBall = new ProcessKey(OnTriggerCpuHaveBall, ProcessStatusCPUHaveBall);
        ProcessKey KeyProcessStatusPlayerHave = new ProcessKey(OnTriggerPlayerHaveBall, ProcessStatusPlayerHaveBall);
        ListProcessKey.Add(ProcessProtectBoard);
        ListProcessKey.Add(KeyMoveToBall);
        ListProcessKey.Add(KeyProcessStatusHaveBall);
        ListProcessKey.Add(KeyProcessStatusPlayerHave);

        ReadFileCPU();
    }




    //
}
