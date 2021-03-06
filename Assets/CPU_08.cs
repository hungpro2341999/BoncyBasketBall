﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPU_08 : AI_07
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        this.speed = 5f;
      
        delay = 0.2f;
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
                    if (!player.isGround  || player.StatusCurr == CharacterState.jumb1 || player.StatusCurr == CharacterState.throw1)
                    {

                        if (player.isMoveLeft)
                        {
                            MoveToPos(player.CurrPos+1);
                        }
                        else
                        {

                            OnAction_01 += OnMoveBackWhenPlayerIsJump;
                            timeOnAction_01 = 0.5f;
                        }


                    }
                    else
                    {
                        MoveToPos(player.CurrPos-1);
                        if (!player.isGround || player.StatusCurr == CharacterState.jumb1 || player.StatusCurr == CharacterState.throw1)
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


                }
                else
                {

                    MoveToPos(player.CurrPos-1);

                    if (!player.isGround || player.StatusCurr == CharacterState.jumb1 || player.StatusCurr == CharacterState.throw1)
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


            }
            else
            {
                MoveToPos(player.CurrPos-1);
            }
        }


        if (!player.isBall)
        {
            NullAction();
            changeStatus = true;
            ChangeStatus = true;
        }

    }

    protected void OnMoveBackWhenPlayerIsJump()
    {
        MoveToPos(CtrlGamePlay.Ins.GetPlayer().CurrPos-1);
        if (!CtrlGamePlay.Ins.GetPlayer().isGround)
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

  
    
}
