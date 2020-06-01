using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_05 : AI_01
{

    [Header("AI_05")]
    public string Key_Action_Catch_And_Throw = "Key_Catch_And_Throw";
    public Transform BoxCatchAndThrow;

    public override void OnTriggerMoveProtectHoop()
    {

                   
    }
    


    public override void OnMoveProtectHoop()
    {
        if (Mathf.Abs(CurrPos - CtrlGamePlay.Ins.AI.CurrPos) <= 2)
        {
            if (isBall)
            {
                isJump = true;
                isPullBall = true;
            }
        }
        else
        {
            var pos = CtrlGamePlay.Ins.GetBall().posPercition;
            MoveToPos(pos);
        }

        if(Mathf.Abs(CurrPos - CtrlGamePlay.Ins.Ball.CurrPos) <= 2)
        {
            OnEndProtectToHoop();
        }
           
       
     
    }
    public override void OnEndProtectToHoop()
    {

        changeStatus = true;
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

    private void MoveToPos(int posTarger)
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
