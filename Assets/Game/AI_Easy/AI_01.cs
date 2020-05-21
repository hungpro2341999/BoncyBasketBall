using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AI_01 : AI 

{
    protected float delayMove;
    protected float delayTimeMove = 0.2f;

    public const string Key_Trigger_Jump_Protect_Ball = "Key_Trigger_Protect_Ball";
    private int PosPrecition;
    public Dictionary<string, ProcessKey> Dictory_Protect_Board = new Dictionary<string, ProcessKey>();
    public Dictionary<string, ActionGame[]> Directory_Protect_Board = new Dictionary<string, ActionGame[]>();
  
    public override void OnTriggerMoveProtectHoop()
    {
        Debug.Log("ONProtect");
        Ball ball = (Ball)CtrlGamePlay.Ins.Ball;
        PosPrecition = PostionToPos(ball.PosPercitionHoop.x);
        
    }

   

    public override void ActiveActionKey()
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
        Debug.Log("Active");
        var action = Directory_Action_Process_Key[KeyCurrBall].StartAction;
        if (action != null)
        {
            Debug.Log("Deo Null");
            action.Invoke();
        }
        else
        {
            Debug.Log("Null");
        }


        


    }

    // add Process Key:
    public void OnTriggerStatusMoveProtectBall()
    {
       
        var ball = (Ball)CtrlGamePlay.Ins.Ball;
        ball.OnPercitionBall_0();
        BoxAttack.gameObject.SetActive(false);
        BoxCatchBall.gameObject.SetActive(true);
        NullOnActionTrigger();
    }

        public void ProcessStatusProtectBall()
    {
        
            string key = KeyCurr();
            KeyActionCurr = key;
            if (changeStatus)
            {
                Dictory_Protect_Board[KeyCurrBall].TriggerAction();
               
                KeyActionCurr = "";
                changeStatus = false;
            }


            if (KeyActionCurr != KeyActionPrevious)
            {
                OnActionWithKey(key);
            }

            KeyActionPrevious = KeyActionCurr;


       
    }

    public void ProcessKey_Protect_Board()
    {
        
        string key = KeyCurrProtectBall();
        KeyActionCurr = key;

        if (changeStatus)
        {
            Dictory_Protect_Board[KeyCurrBall].TriggerAction();
            OnAtionWithKey_Status_Have_Ball(key);
            KeyActionCurr = "";
            changeStatus = false;
        }
        if (KeyActionCurr != KeyActionPrevious)
        {
            OnActionWithKey(key, Directory_Protect_Board);
      //      OnAtionWithKey_Status_Have_Ball(key);
        }
        KeyActionPrevious = KeyActionCurr;
    }

    public void OnActionWithKey(string key,Dictionary<string,ActionGame[]> ActionGames)
    {
        Debug.Log("Protect Move");
        int r = Random.Range(0, ActionGames[key].Length);
        ActionGame a = ActionGames[key][r];
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

 
    public override void OnTriggerMoveToPlayer()
    {
        delayMove = Random.Range(0, delayTimeMove);
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

        if (Mathf.Abs(a.CurrPos - b.CurrPos) <= 3)
        {
            if (!a.isGround || a.StatusCurr == CharacterState.throw1)
            {
                if (delayMove >= 0)
                {
                    delayMove -= Time.deltaTime;
                }
                else
                {
                    delayMove = Random.Range(0, delayTimeMove);
                    isJump = true;
                }
                
            }
        }
    }

    public override void OnMoveProtectHoop()
    {
        Debug.Log("Move ONProtect");
        if ((PosPrecition - CurrPos)==0)
        {
            if (Mathf.Sign(PosPrecition - CurrPos) == 1)
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
        else
        {

            isMoveLeft = false;
            isMoveRight = false;
        }
       

    }
    public override void OnMoveBackHaveBall()
    {
        Player a = CtrlGamePlay.Ins.GetPlayer();
        AI b = CtrlGamePlay.Ins.GetCPU();
        if (isBall)
        {
          

            if (isComplte_move_Back_throw_ball)
                return;
            if (delay_move_Back_throw_ball > 0)
            {
                delay_move_Back_throw_ball -=
                    Time.deltaTime;
                        
                if((a.CurrPos - b.CurrPos)>=0 && a.CurrPos >= 2)
                {
                    isComplte_move_Back_throw_ball = true;
                    isJump = true;
                    int i = Random.Range(0, 1);
                    if (i == 0)
                    {
                        hasPullBall = true;
                    }
                    else
                    {
                        hasPullBall = false;
                    }
                   

                }
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
            NullAction();
            EndActionMoveBackHaveBall();
        }
    }

    public override void ReadFileCPU()
    {

        string text = textCPU.text;
        string[] w = new string[1] { "--Staus--" };
        string[] sss = text.Split(w, System.StringSplitOptions.None);

        for (int i = 0; i < sss.Length; i++)
        {
            //Debug.Log("String : "+sss[i]);
        }

        string s = sss[1];
        string s1 = sss[2];
        string s2 = sss[3];
        string s3 = sss[0];
        string s4 = sss[4];

        StringReader strRead = new StringReader(s);
        StringReader strRead_1 = new StringReader(s1);
        StringReader strRead_2 = new StringReader(s2);
       
        StringReader strRead_3 = new StringReader(s3);
        StringReader strRead_4 = new StringReader(s4);
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
                   Debug.Log(line);

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
                   Debug.Log(line);

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
                 Debug.Log(line);

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
                   Debug.Log(line);

            }
            else
            {

                break;
            }
        }
        while (true)
        {

            string line = strRead_4.ReadLine();
            if (line != null)
            {
                if (line.StartsWith("//") || line == "")
                {
                    continue;
                }

                FormatString_Aciton_Protect_Ball(line);
                   Debug.Log(line);

            }
            else
            {

                break;
            }
        }



    }

    protected void FormatString_Aciton_Protect_Ball(string line)
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


        Directory_Protect_Board .Add(key, ArrayAction);

        //  Debug.Log("Key : "+key + " " + ss);

    }
    public override void Init()
    {

        ProcessKey ProcessProtectBoard = new ProcessKey(ProcessKey_Protect_Board, OnMoveProtectHoop);
        ListProcessKey.Add(ProcessProtectBoard);
        ActionGame lc_OnActionMoveToPlayer = new ActionGame(OnTriggerMoveToPlayer, OnMoveToPlayer);
        ActionGame lc_OnActionMove = new ActionGame(null, OnMoveToBall);
        ActionGame lc_OnJump = new ActionGame(OnJump, OnJumpStraight, Endjump, 0.75f);
        ActionGame lc_OnIdle = new ActionGame(null, OnMoveIde, EndAction, 0.5f);
        ActionGame lc_OnMoveRandom = new ActionGame(null, OnMoveRandom);
        ActionGame lc_onMoveToHoop = new ActionGame(null, OnMoveToHoop);
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
    public string KeyCurrProtectBall()
    {
        string s = Directory_Key_Status[Key_Trigger_Jump_Protect_Ball].ToString();
      
        return s;

        
    }
    public override void OnTriggerStatusMoveCatchBall()
    {
       
        var ball = (Ball)CtrlGamePlay.Ins.Ball;
        ball.OnPercitionBall_2();
        BoxAttack.gameObject.SetActive(false);
        BoxCatchBall.gameObject.SetActive(true);
        NullOnActionTrigger();
      
       
    }


}
