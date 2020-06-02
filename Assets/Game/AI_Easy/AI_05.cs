﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_05 : AI_01
{

    [Header("AI_05")]
    public const string Key_Action_Catch_And_Throw = "Key_Catch_And_Throw";
    public Transform BoxCatchAndThrow;
    public Transform BoxProtectBall;

    public override void Start()
    {
      
        base.Start();
        CtrlGamePlay.Ins.GetBall().AddKeyBall_2();
    }
    public override void OnTriggerStatusMoveProtectBall()
    {
        BoxProtectBall.gameObject.SetActive(true);
        base.OnTriggerStatusMoveProtectBall();
    }


    public override void OnTriggerCpuHaveBall()
    {
        BoxProtectBall.gameObject.SetActive(false);
        base.OnTriggerCpuHaveBall();
    }

    public override void OnTriggerPlayerHaveBall()
    {
        BoxProtectBall.gameObject.SetActive(false);
        base.OnTriggerPlayerHaveBall();
    }

    public override void OnTriggerStatusMoveCatchBall()
    {
        BoxProtectBall.gameObject.SetActive(false);
        base.OnTriggerStatusMoveCatchBall();
    }

    public override void OnMoveProtectHoop()
    {
        var ball = CtrlGamePlay.Ins.GetBall();

        MoveToPos(ball.CurrPos);


        if(Mathf.Abs(CurrPos - CtrlGamePlay.Ins.Ball.CurrPos) == 0)
        {
            OnEndProtectToHoop();
        }
           
       
     
    }
    public override void OnEndProtectToHoop()
    {
        NullAction();
        changeStatus = true;
        isPullBall = false;
    }
   

    public virtual void OnTriggerCatchAndThrow()
    {
        isJump = true;
        
    }

    public virtual void OnStartCatchAndThrow()
    {
        if (isBall)
        {
            isPullBall = true;
        }
        else
        {
            isPullBall = true;
        }
    }
    

    public override void Init()
    {
        ActionGame actionGame = new ActionGame(OnTriggerCatchAndThrow, OnStartCatchAndThrow, Endjump,0.6f);

        Directory_OnActionGame.Add(Key_Action_Catch_And_Throw, actionGame);
        base.Init();
    }

    protected void MoveToPos(int posTarger)
    {
        posTarger = Mathf.Clamp(posTarger, 0, CountSperateDistance);
        //  Debug.Log(CurrPos + "  " + posTarger);
        if ((Mathf.Abs(CurrPos - posTarger) != 0))
        {



            if (Mathf.Sign(CurrPos - posTarger) == 1)
            {
                Debug.Log("Right");
                isMoveRight = true;
                isMoveLeft = false;
            }
            else
            {
                Debug.Log("left");
                isMoveRight = false;
                isMoveLeft = true;
            }

        }
        else
        {
            Debug.Log("None");
            isMoveLeft = false;
            isMoveRight = false;
        }


    }

}
