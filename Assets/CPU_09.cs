using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPU_09 : CPU_08
{
    public const string OnACtionJumpToHoop = "JumpToHoop";
    public const string OnActionJumpThrowBack = "JumpThrowBack";
    public const string OnActionStealBall = "StealBall";


    public override void Start()
    {
        base.Start();
        delay = 0.2f;
    }
    public void OnJumpToHoop()
    {

    }

    public override void OnMoveToHoop()
    {
       
        base.OnMoveToHoop();
        if (CtrlGamePlay.Ins.Player.isBall)
        {
            changeStatus = true;
            ChangeStatus = true;
        }
    }

    public override void OnStartTriggerStealBall()
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
            OnAction_01 = null;
            if (GetDistancePlayerAndCPU() <= 4)
            {
                if (DirectCpu == DirectWithPlayer.Right)
                {
                    if (!player.isGround ||  player.StatusCurr == CharacterState.jumb1 || player.StatusCurr == CharacterState.throw1)
                    {

                        if (player.isMoveLeft)
                        {
                            MoveToPos(player.CurrPos + 1);
                          
                          
                        }
                        else
                        {
                            OnAction_01 += OnMoveBackWhenPlayerIsJump;
                            timeOnAction_01 = 0.5f;

                        }


                    }
                    else
                    {
                        MoveToPos(player.CurrPos);


                     
                    }


            }
            else
            {


                    MoveToPos(player.CurrPos - 1);

            }


            }
            else
            {
                MoveToPos(player.CurrPos - 1);
            }
        }


       
    }
    public override void ProcessStatusPlayerHaveBall()
    {
        string key = KeyCurr_Player_Have_Ball();
        KeyActionCurr = key;
        if (changeStatus)
        {
            Directory_Action_Process_Key[KeyCurrBall].TriggerAction();
            changeStatus = false;
        }

       
        if (KeyActionCurr != KeyActionPrevious || KeyActionCurr!="")
        {
            OnAtionWithKey_Status_Player_Have_Ball(key);
        }
        KeyActionPrevious = KeyActionCurr;
    }


    public override string KeyCurr_Player_Have_Ball()
    {
        return 1.ToString();
    }
    public override string KeyCurrProtectBall()
    {
      
        

        if (CtrlGamePlay.Ins.GetBall())
        {
            return 1.ToString();
        }
        else
        {
            return 0.ToString();
        }
       
        
      
    }
    public override void Init()
    {
        ActionGame AcitonJumpThrowBack = new ActionGame(OnTriggerJumpThrowBack, OnJumpThrowBack, null, 1);

        ActionGame ActionBlocKPlayer = new ActionGame(OnTriggerBlockPlayerThrow, OnStartBlockPlayerThrow);

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

        ActionGame lc_OnActionMoveProtectBall = new ActionGame(OnTriggerMoveProtectHoop, OnMoveProtectHoop);

        ActionGame lc_OnCatchAndThrowBall = new ActionGame(OnTriggerCatchAndThrow, OnStartCatchAndThrow, null, 1);

        ActionGame lc_OnJumpDirect = new ActionGame(OnActionJumpFollowDirect, OnTriggerActionJumpFollowDirect, OnEndActionJumpFollowDirect, 0.9f);

        ActionGame lc_OnStealBall = new ActionGame(OnTriggerStealBall, OnStartTriggerStealBall);
        // Update To Directory



        //  Update
        Directory_OnActionGame.Add(Key_Move_To_Ball, lc_OnActionMove);
        Directory_OnActionGame.Add(Key_Move_Ide, lc_OnIdle);
        Directory_OnActionGame.Add(Key_Move_Random, lc_OnMoveRandom);
        Directory_OnActionGame.Add(Key_Move_To_Hoop, lc_onMoveToHoop);
        Directory_OnActionGame.Add(Key_Stun, lc_OnStun);
        Directory_OnActionGame.Add(Key_Move_To_Player, lc_OnActionMoveToPlayer);
        Directory_OnActionGame.Add(Key_Action_Jump_Direct, lc_OnJumpDirect);



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
        Directory_OnActionGame.Add(OnActionStealBall, lc_OnStealBall);
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

    

    public override void OnStartBlockPlayerThrow()
    {
        Debug.Log("RUnnnnnnnnnnnnnn");
        var player = CtrlGamePlay.Ins.GetPlayer();
        if (timeOnAction_01 > 0)
        {

            timeOnAction_01 -= Time.deltaTime;
            if (OnAction_01 != null)
                OnAction_01();
        }
        else
        {
            OnAction_01 = null;


            if (GetDistancePlayerAndCPU() <= 5)
            {
                if (DirectCpu == DirectWithPlayer.Right)
                {

                    if (player.isMoveRight )
                    {
                        MoveToPos(player.CurrPos - 1);

                        if (!player.isGround ||  player.StatusCurr == CharacterState.jumb1 || player.StatusCurr == CharacterState.throw1)
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
                    else if (player.isMoveLeft)
                    {

                        MoveToPos(player.CurrPos - 1);
                    }
                    else
                    {
                        MoveToPos(player.CurrPos - 1);
                        if (!player.isGround || player.StatusCurr == CharacterState.jumb1 || player.StatusCurr == CharacterState.throw1)
                        {
                         
                            timeOnAction_01 = 0.3f;
                           
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

                    MoveToPos(player.CurrPos - 1);

                }
            }
            else
            {
                MoveToPos(player.CurrPos - 1);
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
}
