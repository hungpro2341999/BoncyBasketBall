using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AI_02 : AI
{
    public const string Key_Action_Move_Back_To = "OnActionMoveBackToDefend";
    protected List<float> Paramter = new List<float>();
    protected bool CompleteAction = false;
    public Text textDebug;
    protected float space = 3;

    public override void Start()
    {
       
        base.Start();
        CtrlGamePlay.Ins.GetBall().AddKeyBall_1();
    }
    public override void OnTriggerMoveToPlayer()
    {
        Paramter = new List<float>();
        Paramter.Add(2); // delaytime;
        Paramter.Add(0); // timeRun;
    }


    public override void OnMoveToPlayer()
    {
        var a = CtrlGamePlay.Ins.GetPlayer();
        var b = CtrlGamePlay.Ins.GetCPU();


        if (Mathf.Abs(a.CurrPos - b.CurrPos) == 0)
        {
            if (Mathf.Abs(Mathf.Abs(a.CurrPos - b.CurrPos)) == 1)
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


    public virtual void OnTriggerMoveBackTo()
    {
        var player = CtrlGamePlay.Ins.GetPlayer();
        var cpu = CtrlGamePlay.Ins.GetCPU();

        PushParmater(new float[] {0.5f, 0});
        // 1:PosRandomWhenPlayerCome
        // 2:PosRandomWhenPlayerForward

        Debug.Log("Pos Random : " + Paramter[0]);



    }
    public override void OnTriggerMoveToHoop()
    {
        isComplete = false;
        Paramter = new List<float>();
        Paramter.Add(Random.Range(7,8));  // RandomTargetMove

    }

    public override void OnMoveToHoop()
    {

        MoveToPos((int)Paramter[0]);  

        if (!isComplete)
        {
            if (CurrPos >= Paramter[0])
            {
                StatusCurr = CharacterState.throw1;
                isJumpGround = true;
            }
            isComplete = true;
        }
     
    }

    

    public virtual void OnStartMoveBackTo()
    {
        var player = CtrlGamePlay.Ins.Player;
        var cpu = CtrlGamePlay.Ins.AI;
        int distance = Mathf.Abs(player.CurrPos - cpu.CurrPos);
        if (!isGround)
            return;
        if (player.CurrPos >= 5)
        {
            if (DirectCpu == DirectWithPlayer.Right)
            {
              
                    MoveToPos(player.CurrPos - 4);
           
            


            }
            else
            {


                MoveToPos(player.CurrPos - 4);

            }

        }
        else
        {
            if (DirectCpu == DirectWithPlayer.Right)
            {


                if (distance <= 2)
                {
                    MoveToPos(player.CurrPos + 1);
                }
                else
                {
                    if (Paramter[1]<=0)
                    {
                        if (!player.isGround)
                        {
                            isJump = true;
                            Paramter[1]  = Paramter[0];
                            
                        }
                    }
                    else
                    {
                        Paramter[1] -= Time.deltaTime;
                    }
                    MoveToPos(player.CurrPos - 4);
                }
              




            }
            else
            {


                MoveToPos(player.CurrPos - 4);

            }
        }


      



    }

    private void MoveToPos(int posTarger)
    {
          posTarger = Mathf.Clamp(posTarger, 0, CountSperateDistance);
      //  Debug.Log(CurrPos + "  " + posTarger);
      if((Mathf.Abs(CurrPos - posTarger) != 0))
        {
           


            if (Mathf.Sign(CurrPos - posTarger) == 1 )
            {
           //     Debug.Log("Right");
                isMoveRight = true;
                isMoveLeft =false ;
            }
            else
            {
           //     Debug.Log("left");
                isMoveRight = false;
                isMoveLeft = true;
            }
           
        }
        else
        {
         //   Debug.Log("None");
            isMoveLeft = false;
            isMoveRight = false;
        }
    }

  

    //public override void OnMoveToBall()
    //{

    //}

    protected void PushParmater(float[] paramter)
    {
        Paramter = new List<float>();
        for (int i = 0; i < paramter.Length; i++)
        {
            Paramter.Add(paramter[i]);
        }
    }
    public override void Init()
    {
        ActionGame lc_Move_Back = new ActionGame(OnTriggerMoveBackTo, OnStartMoveBackTo);

        Directory_OnActionGame.Add(Key_Action_Move_Back_To, lc_Move_Back);

        base.Init();
    }



    public bool isStop()
    {
        if (CurrPos == 0 || CurrPos == 11)
        {
            return true;
        }
        return false;
    }
   


}
