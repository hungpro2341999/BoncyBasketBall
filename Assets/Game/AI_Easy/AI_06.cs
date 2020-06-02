using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_06 : AI_05
{
    [Header("AI_06")]
    public int[] PerThrowBall = new int[] { 100,60,40,30,20,10,5,5,5,5,5,5,5};
    private int PosThrowCurr;
    private int PosThrowPrevious;

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

        CtrlGamePlay.Ins.GetBall().AddKeyBall_2();
    }

    public override void GetStatus()

    {
        if(Mathf.Abs(CurrPos - CtrlGamePlay.Ins.GetPlayer().CurrPos) <= 2)
        {
            Directory_StatusCpu[Key_Trigger_Front] = 1;
        }
        else
        {
            Directory_StatusCpu[Key_Trigger_Front] = 0;
        }
        base.GetStatus();
    }
    private bool OnTriggerNewCurrThrowPos(int Pos)
    {
        int r = Random.Range(0, 100);
        if(r< PerThrowBall[Pos])
        {
            return true;
        }

        return false;
    }


    public override void OnMoveToHoop()
    {
        PosThrowCurr = CurrPos;
        if (PosThrowCurr != PosThrowPrevious)
        {
            if (OnTriggerNewCurrThrowPos(PosThrowCurr))
            {
                isJump = true;
                isThrowBall = true;
                if (CurrPos >= 11)
                {
                    isMoveLeft = false;
                    isMoveRight = false;
                }
                else if (CurrPos < 11 && CurrPos>=6)
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
        else
        {
            MoveToPos(12);
        }
        PosThrowPrevious = PosThrowCurr;


    }
    public override void OnTriggerJumpLeft()
    {
        var ball = CtrlGamePlay.Ins.Ball;
        var player = CtrlGamePlay.Ins.Player;
        if(Mathf.Abs(ball.CurrPos - player.CurrPos) <= 2)
        {
            isJump = false;
        }
        else
        {
            isJump = true;
        }
    }


    public override void OnJumpLeft()
    {

        isMoveLeft = true;
        isMoveRight = false;
    }

    public override void Endjump()
    {
        base.Endjump();
    }


    public override void Init()
    {
        ActionGame lc_OnActionMoveToPlayer = new ActionGame(OnTriggerMoveToPlayer, OnMoveToPlayer);
        ActionGame lc_OnActionMove = new ActionGame(null, OnMoveToBall);
        ActionGame lc_OnJump = new ActionGame(OnJump, OnJumpStraight, Endjump, 0.75f);
        ActionGame lc_OnIdle = new ActionGame(null, OnMoveIde, EndAction, 0.5f);
        ActionGame lc_OnMoveRandom = new ActionGame(null, OnMoveRandom);
        ActionGame lc_onMoveToHoop = new ActionGame(null, OnMoveToHoop);
        ActionGame lc_OnJump_left = new ActionGame(OnTriggerJumpLeft, OnJumpLeft, Endjump, 0.8f);
        ActionGame lc_OnJump_right = new ActionGame(OnJump, OnJumpRight, Endjump, 0.8f);
        ActionGame lc_OnSlampDunk = new ActionGame(OnTriggerSlampDunk, OnActionSlampDunk, OnEndSlampDunk, 2, true);
        ActionGame lc_OnStun = new ActionGame(OnTriggerStun, OnStun, OnEndTriggerStun, 0);

        ActionGame lc_OnAttack = new ActionGame(OnStartActionAttack, OnActtionTriggerAttack, null);
        ActionGame lc_OnCatchBall = new ActionGame(OnStartActionCatchBall, OnActtionTriggerCatchBall, null);

        ActionGame lc_OnThrowOnGround = new ActionGame(OnStartThrowOnEarth, OnUpdateThrowOnEarth, OnEndThrowOnEarth, 0.35f);

        ActionGame lc_OnActionMoveToBall = new ActionGame(null, OnMoveToBall, EndAction, 1);

        ActionGame lc_OnMoveBackHaveBall = new ActionGame(OnStartMoveBackHaveBall, OnMoveBackHaveBall, EndActionMoveBackHaveBall, 2.5f);

        ActionGame lc_OnActionJumpThrowBall = new ActionGame(OnJumpThrowBall, OnStartThrowBall, EndAction, 0.85f);

        ActionGame lc_OnActionMoveProtectBall = new ActionGame(OnTriggerMoveProtectHoop, OnMoveProtectHoop, OnEndProtectToHoop, 2);

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


    // OnTrickPlayer
    //private float timeAction_1=0.4f;
    //private float timeAction_2=0.4f;
    //private float timeActionTrickPlayer;

    //public void OnTriggerActionTrickPlayer()
    //{


    //    isJump = true;
    //    var player = CtrlGamePlay.Ins.Player;


    //    if(DirectCpu != DirectWithPlayer.Left)
    //    {
    //        if (player.StatusCurr == CharacterState.swing)
    //        {

    //            isJump = true;
    //            isMoveRight = true;
    //            isMoveLeft = false;
    //        }
    //        else if (!player.isGround)
    //        {
    //            MoveToPos(1);

    //        }
    //    }

    //}
    //public void OnActionTrickPlayer()
    //{

    //    if (!isBall)
    //    {
    //        NullAction();
    //    }
    //}



}
