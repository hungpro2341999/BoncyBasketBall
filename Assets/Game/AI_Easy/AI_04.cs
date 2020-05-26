using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_04 : AI_01
{
    public override void Start()
    {
        delayTimeMove = 0.5f;
        base.Start();
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

    public override void OnMoveToBall()
    {
        var ball = CtrlGamePlay.Ins.GetBall();


        Vector3 targert = ball.PrecitionBallGround;
        int posTarget = PostionToPos(targert.x);
        
        posTarget = Mathf.Clamp(posTarget,0, CountSperateDistance);
        Debug.Log("Ball Target : " + posTarget);
        MoveToPos(posTarget);

    }

    public override int PostionToPos(float x)
    {
        int point = (int)((PosInit - x) / Amount);

        return point;
    }

    public override void OnTriggerJumpLeft()
    {
      
    }

    public override void OnMoveProtectHoop()
    {
        var ball = CtrlGamePlay.Ins.GetBall();
        int posHoop = PostionToPos(ball.PosPercitionHoop.x);;
        Debug.Log("PosHoop : ");
        MoveToPos(posHoop);
    }
    public override void OnTriggerMoveProtectHoop()
    {
        

    }
    public override void OnEndProtectToHoop()
    {
        base.OnEndProtectToHoop();
    }

    public override void OnTriggerMoveToPlayer()
    {
        delayMove = 0.5f;
    }
    public override void OnMoveToPlayer()
    {
        var a = CtrlGamePlay.Ins.GetPlayer();
        var b = CtrlGamePlay.Ins.GetCPU();


        if (Mathf.Abs(transform.position.x - a.transform.position.x) > 0)
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

        if (Mathf.Abs(a.CurrPos - b.CurrPos) <= 4)
        {
            if (delayMove < 0)
            {
                if (a.StatusCurr == CharacterState.throw1)
                {
                        isJump = true;
                        isMoveRight = true;
                        isMoveLeft = false;
                        delayMove = 0.6f;
                     
                  

                }
            }
            else
            {
                delayMove -= Time.deltaTime;
            }
                
        }
    }


    public override void OnJumpLeft()
    {
        var a = CtrlGamePlay.Ins.Ball;
        var b = CtrlGamePlay.Ins.Player;

        if (Mathf.Abs(a.CurrPos - b.CurrPos) <= 2)
        {

            if (!b.isGround)
            {
                isJump = true;

               
                isMoveRight = false;
                isMoveLeft = true;
            }

        }
        else
        {
            isJump = true;

            isMoveRight = false;
            isMoveLeft = true;
        }
    }

    

}
