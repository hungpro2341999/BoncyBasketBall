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


    public override void OnTriggerMoveToPlayer()
    {
        Paramter = new List<float>();
        Paramter.Add(2); // delaytime;
        Paramter.Add(0); // timeRun;
    }


    public override void OnMoveToPlayer()
    {
        var player = CtrlGamePlay.Ins.Player;
        var cpu = CtrlGamePlay.Ins.AI;

        if (Mathf.Abs(player.CurrPos - cpu.CurrPos) == 1)
        {

            isMoveLeft = false;
            isMoveRight = false;



        }
        else
        {


        }

    }


    public virtual void OnTriggerMoveBackTo()
    {
        var player = CtrlGamePlay.Ins.GetPlayer();
        var cpu = CtrlGamePlay.Ins.GetCPU();

        PushParmater(new float[] { 2, 0.5f, 0 });
        // 1:PosRandomWhenPlayerCome
        // 2:PosRandomWhenPlayerForward

        if (player.CurrPos >= 6)
        {
            Paramter[0] = (float)Random.Range(2, (int)(player.CurrPos / 2));
            Paramter[0] = Mathf.Clamp(Paramter[0], 0, 11);


        }

        Debug.Log("Pos Random : " + Paramter[0]);



    }
    public virtual void OnStartMoveBackTo()
    {
        var player = CtrlGamePlay.Ins.Player;
        var cpu = CtrlGamePlay.Ins.AI;
        string s = "";

        Debug.Log("Run");
        if (DirectCpu == DirectWithPlayer.Right)
        {


            if (player.CurrPos >= 6)
            {
                if(Distance_Player_CPU() <= 4)
                {
                
                    MoveToPos(player.CurrPos, 0.1f);
                }
                else
                {
                    MoveToPos((int)Paramter[0], 0.3f);
                }
              

            }
            else
            {
                MoveToPos((int)Paramter[0],0.3f);
                if (Paramter[2] >= 0)
                {
                    Paramter[2] -= Time.deltaTime;

                }
                else
                {
                    if (!player.isGround)
                    {

                        isJump = true;
                        Paramter[2] = Paramter[1];
                    }

                }
            }
        }
        else
        {
            
            MoveToPos(player.CurrPos,0.1f);
            if(Distance_Player_CPU()>=0 && Distance_Player_CPU() <= 2)
                {
                
                    MoveToPos(player.CurrPos, 0.1f);
                }
                else
                {
                    MoveToPos((int)Paramter[0], 0.3f);
                }
              

            if (Distance_Player_CPU() >= 0 && Distance_Player_CPU() <= 2)
            {
                if (Paramter[2] >= 0)
                {
                    Paramter[2] -= Time.deltaTime;

                }
                else
                {
                    if (!player.isGround)
                    {

                        isJump = true;
                        Paramter[2] = Paramter[1];
                    }

                }
            }
        }
        textDebug.text = s;

    }

    private void MoveToPos(int posTarger,float space)
    {
        Debug.Log(CurrPos + "   " + posTarger + "  " + PosInit + posTarger * Amount);

        if ((Mathf.Abs(transform.position.x - (PosInit + (posTarger * Amount))) >= space  && !isStop()))
        {
            if ((Mathf.Abs(transform.position.x - (PosInit + (posTarger * Amount))) == 1))
            {
                Debug.Log("left");
                isMoveRight = false;
                isMoveLeft = true;
            }
            else
            {
                Debug.Log("right");

                isMoveRight = true;
                isMoveLeft = false;
            }
        }
        else
        {
            Debug.Log("None");
            isMoveRight = false;
            isMoveLeft = false;
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
        if (CurrPos == 0 || CurrPos == 12)
        {
            return true;
        }
        return false;
    }
   


}
