using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using UnityEngine.UI;
using System.IO;
using Spine;

public enum RelizeBall { Right,Left,Up,Down};
public enum IsSameDirectWithBall  { Yes, No };
public enum IsSameDirectWithPlayer { Yes, No};


public enum StatusWithPlayer { BlockByPlayer, NotBlock,None};
public enum StatusHaveBall { Yes,No}




public class AI : Character
{






    [Header("InforCPU")]
    #region InforAI

    public TextAsset textCPU;

    public float ForceHead;
    public float SpeedShoe;

    #endregion


    public bool isMoveRight;
    public bool isMoveLeft;
    public bool isJump;
    public bool isJump_x2;
    public bool isThrowBall;




    public float Left;
    public float Right;
    public float Up;
    public float ThrowBall;

    public float delayAI;

    [Header("STATUS")]
    // Status

    public RelizeBall RelizeBall;
    public IsSameDirectWithBall isSameWithBall;
    public IsSameDirectWithPlayer isSameWithPlayer;
    public StatusWithPlayer StatusWithPlayer;
    public StatusHaveBall StatusHaveBall;
    public LayerMask LayerPlayer;
    public float DistanceToPlayer;

    public float PrecJump;
    public float DistanceToJump;
    public float PosCurr;
    public int[] MatrixPositonAi;



    public int CountSperateDistance;

    public float ForceBack;
    public float timeStun;

    public bool isLimit = false;

    private float Amount;
    private float PosInit;


    public int CurrPos;
    public int PreviousPos;
    public bool ChangeStatus;


    public Transform PosHand;



    public Transform TriggerBallForward;
    [Header("CPU")]
    // Key Action


    public const string Key_Jump_Right = "OnJumpRight";
    public const string Key_Jump_Left = "OnJumpleft";
    public const string Key_Jump_Straight = "OnActionJump";

    public const string Key_Move_To_Ball = "MoveToBall";
    public const string Key_Move_To_Hoop = "MoveToHoop";

    public const string Key_Catch_Ball = "On_Catch_Ball";
    public const string Key_Move_Random = "MoveRandom";
    public const string Key_Move_Ide = "Ide";


    public const string Key_Slamp_Dunk = "SlampDunk";
    public const string Key_Stun = "Key_Stun";

    // Action Trigger
    public const string Key_Action_Trigger_Attack = "Action_Attack";

    public const string Key_Action_Trigger_CatchBall = "Action_Catch_Ball";




    // Key

    public const string Key_Trigger_Have_Ball = "Trigger_Ball";
    public const string Key_Trigger_Jump = "Trigger_Jump";
    public const string Key_Trigger_ThrowBall = "Trigger_Throw_Ball";
    public const string Key_Trigger_Front = "Trigger_Front";
    public const string Key_Trigger_Back = "Trigger_Back";

    public const string Key_Trigger_Slamp_Dunk = "Trigger_Slamp_Dunk";

    public const string Key_Trigger_With_Ball = "Trigger_Ball";









    Dictionary<string, ActionGame> Directory_OnActionGame = new Dictionary<string, ActionGame>();

    Dictionary<string, int> Directory_StatusCpu = new Dictionary<string, int>();

    Dictionary<string, ActionGame[]> Directory_Key_Status = new Dictionary<string, ActionGame[]>();

    Dictionary<string, ActionGame[]> Directory_Key_Status_Have_Ball = new Dictionary<string, ActionGame[]>();

    Dictionary<string, RegistryItem> registry;

    public event System.Action<float> OnAction;
    public event System.Action OnMove;
    public event System.Action<float> OnActionEFF;
    public event System.Action OnActionTrigger;


    public Stack<System.Action> Stack_Active_Action = new Stack<System.Action>();

    public string KeyActionCurr;
    public string KeyActionPrevious;

    //

    public string KeyActionTriggerCurr;
    public string KeyActionTriggerPrevious;



   

    [Header("Debug")]



    public Text TextStatus;
    public bool test = false;

    public Transform BoxCatchBall;

    public enum TypeRandomMove { RandomDistance, RandomInGround1, RandomInGround2 }
    [Header("Move")]

    public Vector3 TargetHoop;



    // Move Random
    public float TargetX;

    public float DistanceRandom;

    public float TargetRandom;

    [Header("ActionEFF")]

    public Action[] ArrayAction;
    public bool isComplete = false;
    private int i = 0;
    public bool isAction = false;




    public void PushAction(System.Action Action)
    {
        Stack_Active_Action.Push(Action);
    }
    public System.Action PoPAction()
    {

        return Stack_Active_Action.Pop();
    }



    public Stack<System.Action> Stack_Action_Move = new Stack<System.Action>();







    public override void Start()
    {
        base.Start();
        AnimStatus.Complete += OnComplete;
        AnimStatus.Start += OnStartAnim;
        AnimStatus.Interrupt += OnInterrupt;
        AnimStatus.Dispose += OnDispose;
        AnimStatus.Event += OnEvent;


        CtrlGamePlay.Ins.eventRestGamePlay += Reset;
        // bla bla
        // AnimStatus.Complete += OnComplete;

        Amount = CtrlGamePlay.Ins.WidthScreen / CountSperateDistance;

        MatrixPositonAi = new int[CountSperateDistance];

        PosInit = CtrlGamePlay.Ins.WidthScreen / 2;
        TargetX = transform.position.x;

        TargetHoop = GameObject.FindGameObjectWithTag("TargetCPU").transform.position;

        base.Start();


        Init();

        CtrlGamePlay.Ins.eventRestGamePlay += Event_Reset;
        CtrlGamePlay.Ins.eventResetGame += Event_Reset;
    }



    #region Core
    public override void CaculateStatus()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {

            OnAttack();
        }

        
       

        if (OnActionEFF != null)
        {
            Debug.Log("EFF_Action");
            OnActionEFF(Time.deltaTime);
        }

        else
        {



            if (OnAction == null)
            {
                if (Ball.KeyBall == "10")
                {

                    ProcessStatusPlayerHaveBall(KeyCurr_Status_Have_Ball());
                }
                else
                {
                    ProcessStatus(KeyCurr());
                }
               
            }

            if (!test)
            {
                if (OnAction != null)
                {
                    OnAction(Time.deltaTime);



                }
                else if (OnMove != null)
                {

                    OnMove();


                }
                else
                {
                    isMoveLeft = false;
                    isMoveRight = false;

                }

            }


            if (OnActionTrigger != null)
            {
                OnActionTrigger();
            }


        }




    }
    public void LoadCurrPosionPlayer()
    {

        int point = (int)((PosInit - transform.position.x) / Amount);

        for (int i = 0; i < MatrixPositonAi.Length; i++)
        {
            if (i == point)
            {
                if (CurrPos != point)
                {
                    ChangeStatus = true;
                }
                CurrPos = point;

                MatrixPositonAi[i] = 1;
            }
            else
            {
                MatrixPositonAi[i] = 0;
            }

        }


    }


    

    public override void Move()
    {



        if (isAction)
            return;




        if (isMoveRight)
        {
           

            Velocity = speed * Vector2.right;
        }

        if (isMoveLeft)
        {
           

            Velocity = speed * Vector2.left;
        }
        if (isGround)
        {

            if (isJump)
            {

                m_timeStartJump = timeStartJump;
                if (isBall)
                {
                    isPullBall = true;
                }
                isJump = false;

                isGround = false;
                isStartJump = true;



                Body.velocity = new Vector2(Body.velocity.x, Force);
                




            }
        }



        //if (isThrowBall)
        //{
        //    isThrowBall = false;
        //    CtrlGamePlay.Ins.ThrowBall();
        //}

        if (isGround)
        {
            if (!isAttack)
            {
                Debug.Log(Body.velocity.x);
                if (Body.velocity.x == 0)
                {
                    StatusCurr = CharacterState.idle;
                }
                else if (Mathf.Sign(Body.velocity.x) == 1)
                {
                    StatusCurr = CharacterState.move1;
                }
                else if (Mathf.Sign(Body.velocity.x) == -1)
                {
                    StatusCurr = CharacterState.move2;
                   
                }
            }
            else
            {

                StatusCurr = CharacterState.swing;
            }


        }
        else
        {
           
                    // BoxCatchBall.gameObject.SetActive(false);
                    if (m_timeStartJump >=0)
                    {
                      StatusCurr = CharacterState.jumb1;
                      m_timeStartJump -= Time.deltaTime;

                    }
                    else
                    {
                        // TriggerBallForward.gameObject.SetActive(false);
                        if (Mathf.Sign(Body.velocity.y) == 1 || Mathf.Sign(Body.velocity.y) == 0)
                        {
                            StatusCurr = CharacterState.jumb2;

                        }
                        else if (Mathf.Sign(Body.velocity.y) == -1)
                        {
                            if (isPullBall)
                            {
                                if (isBall)
                                {
                                    isBall = false;
                                    CtrlGamePlay.Ins.Launch(Random.Range(2, 3), TargetHoop);
                                    isPullBall = false;

                                }

                            }
                            StatusCurr = CharacterState.jumb3;

                        }

                    }
        }
        if (isAttack)
        {
            StatusCurr = CharacterState.swing;
        }

    }


    public override void CatchBall()
    {
        //   Debug.Log("CatchBall");
        var collision = (Ball)CtrlGamePlay.Ins.Ball;
        collision.GetComponent<Ball>().Velocity = Vector3.zero;
        collision.GetComponent<Ball>().Body.isKinematic = true;
        collision.GetComponent<CircleCollider2D>().isTrigger = true;

        collision.transform.parent = PosHand;
        collision.transform.localPosition = Vector3.zero;
        isBall = true;
        CtrlGamePlay.Ins.Player.isBall = false;

    }


    public override void GetStatus()
    {
        base.GetStatus();

        LoadCurrPosionPlayer();



        //1.

        if (isBall)
        {
            Directory_StatusCpu[Key_Trigger_Have_Ball] = 1;
           
        }
        else
        {
            Directory_StatusCpu[Key_Trigger_Have_Ball] = 0;
           

        }

        //2.


    }


    private void LateUpdate()
    {

        Body.velocity = new Vector2(((isMoveRight ? 1 : 0) + (isMoveLeft ? -1 : 0)) * speed, Body.velocity.y);

    }



    #endregion

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 16)
        {
            if (!isBall)
            {
                Vector3 bounceDir = collision.gameObject.transform.position - gameObject.transform.position;
                Vector3 shootForce;
                bounceDir.Normalize();
                shootForce = Vector3.Scale(bounceDir, new Vector3(ForceHead, ForceHead, ForceHead)) * Bounch * (1 + high);
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(shootForce, ForceMode2D.Impulse);
            }

        }
    }


    #region SetUpKeyAction



    public void ActionWithKey(string key)
    {
        if (Directory_OnActionGame.ContainsKey(key))
        {

        }
    }



    #endregion

    #region AnimationPullBall



    #endregion



    #region Animation



    #endregion










    // Move
    #region Move
    public void MoveToPostion(float x)
    {
        if (Mathf.Abs(x - transform.position.x) <= 0.2f)
        {
            isMoveLeft = false;
            isMoveRight = false;
        }
        else
        {
            if (Mathf.Sign(x - transform.position.x) == 1)
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

    public void OnMoveToHoop()
    {
        isMoveLeft = true;
        isMoveRight = false;
    }


    public void OnMoveIde()
    {
        OnAction = null;
        OnMove = null;
        isMoveLeft = false;
        isMoveRight = false;

    }



    //private void  OnStun          

    private void OnMoveRandom()
    {



        if (isChange())
        {
            while (Mathf.Abs(TargetX - transform.position.x) <= 0.2f)
            {
                int r = Random.Range(CurrPos - 2, CurrPos + 2);
                r = Mathf.Clamp(r, 1, MatrixPositonAi.Length - 1);
                TargetX = r * Amount;
            }
            Debug.Log("Target : " + TargetX);
            Debug.Log("Not_Move");
        }
        else
        {
            Debug.Log("Move");
            if (Mathf.Sign(TargetX - transform.position.x) == -1)
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

    #endregion
    public bool isChange()
    {
        if (Mathf.Abs(TargetX - transform.position.x) <= 0.2f)
        {

            return true;


        }
        else
        {
            return false;

        }
        return false;
    }

    public void OnMoveToBall()
    {
        var a = (Ball)CtrlGamePlay.Ins.Ball;
        if (Mathf.Abs(transform.position.x - a.transform.position.x) >= 0.55f)
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

    }

    public void OnJumpLeft()
    {

        isMoveRight = false;
        isMoveLeft = true;

    }
    public void OnJumpRight()
    {

        isMoveRight = true;
        isMoveLeft = false;

    }
    public void OnJumpStraight()
    {

        isMoveRight = false;
        isMoveLeft = false;

    }


    public void OnJump()
    {
        Debug.Log("Jump");
        isJump = true;
    }
    public void ResetJump()
    {
        isMoveRight = false;
        isMoveLeft = false;
        isJump = false;
    }


    public void OnStun()
    {

        isAction = true;
        StatusCurr = CharacterState.stun;
        isStun = true;
        isMoveLeft = false;
        isMoveRight = false;
        isJump = false;


    }

    public void OnTriggerStun()
    {
        Force_Back();
    }

    public void OnEndTriggerStun()

    {
        isStun = false;
        isAction = false;

    }

    public void OnAttack()
    {
        if (isGround && !isStartJump)
        {
            Debug.Log("Attack");
            StatusCurr = CharacterState.swing;
            isAttack = true;
        }

    }





    public override void Force_Back()
    {
        Debug.Log("ForceBack");
        Body.AddForce(Vector2.right * ForceBack, ForceMode2D.Force);
        if (isBall)
        {
            var Ball = CtrlGamePlay.Ins.Ball;

            isBall = false;

            Ball.transform.transform.parent = null;
            Ball.GetComponent<CircleCollider2D>().isTrigger = false;
            Ball.Body.isKinematic = false;
            Ball.Body.simulated = true;
            Ball.Body.AddForce(Vector2.up * 10, ForceMode2D.Force);
        }

    }



    public void OnActtionTriggerCatchBall()
    {
        if (isGround && !isStartJump)
        {
            if (!isAttack)
            {
                isAttack = true;
                Debug.Log("Attack");
                StatusCurr = CharacterState.swing;
                isAttack = true;
            }

        }
    }

    public void OnActtionTriggerAttack()
    {
        if (isGround && !isStartJump)
        {
            if (!isAttack)
            {
                Debug.Log("Attack");
                StatusCurr = CharacterState.swing;
                isAttack = true;
            }

        }
    }

    public void OnStartActionAttack()
    {
        isAttack = false;
    }
    public void OnStartActionCatchBall()
    {
        isAttack = false;
    }



    public void Endjump()
    {
        int i = Random.Range(0, 2);
        if (i == 0)
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

    /// <summary>
    /// 
    /// </summary>
    /// 
    #region AnimtionJump





    public void OnActionSlampDunk()
    {
        if (i < ArrayAction.Length)
        {
            if (Vector3.Distance(ArrayAction[i].transform.position, transform.position) == 0)
            {

                ArrayAction[i].SetAction();
                i++;
            }
            else
            {

                transform.position = Vector3.MoveTowards(transform.position, ArrayAction[i].transform.position, Time.deltaTime * ArrayAction[i].time);
            }

        }

    }

    public void OnTriggerSlampDunk()
    {
        isAction = true;
        StatusCurr = CharacterState.jumb2_slamdunk;
        isComplete = false;
        i = 0;
        Body.simulated = false;
        isMoveLeft = false;
        isMoveRight = false;

        Body.constraints = RigidbodyConstraints2D.FreezePositionY;
    }





    #endregion




    #region ActionKey
    public void Init()
    {
        // Initialize Action With Key


        ActionGame lc_OnActionMove = new ActionGame(null, OnMoveToBall);
        ActionGame lc_OnJump = new ActionGame(OnJump, OnJumpStraight, Endjump, 0.8f);
        ActionGame lc_OnDie = new ActionGame(null, OnMoveIde);
        ActionGame lc_OnMoveRandom = new ActionGame(null, OnMoveRandom);
        ActionGame lc_onMoveToHoop = new ActionGame(null, OnMoveToHoop);
        ActionGame lc_OnJump_left = new ActionGame(OnJump, OnJumpLeft, null, 1.7f);
        ActionGame lc_OnJump_right = new ActionGame(OnJump, OnJumpRight, null, 1.7f);
        ActionGame lc_OnSlampDunk = new ActionGame(OnTriggerSlampDunk, OnActionSlampDunk, null, 100f, true);
        ActionGame lc_OnStun = new ActionGame(OnTriggerStun, OnStun, OnEndTriggerStun, 2);

        ActionGame lc_OnAttack = new ActionGame(OnStartActionAttack, OnActtionTriggerAttack, null);
        ActionGame lc_OnCatchBall = new ActionGame(OnStartActionCatchBall, OnActtionTriggerCatchBall, null);



        // Update To Directory



        //  Update
        Directory_OnActionGame.Add(Key_Move_To_Ball, lc_OnActionMove);
        Directory_OnActionGame.Add(Key_Move_Ide, lc_OnDie);
        Directory_OnActionGame.Add(Key_Move_Random, lc_OnMoveRandom);
        Directory_OnActionGame.Add(Key_Move_To_Hoop, lc_onMoveToHoop);
        Directory_OnActionGame.Add(Key_Stun, lc_OnStun);




        //  Action

        Directory_OnActionGame.Add(Key_Jump_Straight, lc_OnJump);
        Directory_OnActionGame.Add(Key_Jump_Left, lc_OnJump_left);
        Directory_OnActionGame.Add(Key_Jump_Right, lc_OnJump_right);

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

        ReadFileCPU();


    }

    public string KeyCurr()
    {
        
        string key = Directory_StatusCpu[Key_Trigger_Have_Ball].ToString() + Directory_StatusCpu[Key_Trigger_Jump].ToString() + Directory_StatusCpu[Key_Trigger_Front].ToString()
               + Directory_StatusCpu[Key_Trigger_Back].ToString() + Directory_StatusCpu[Key_Trigger_ThrowBall];
        TextStatus.text = key;

        return key;
    }



    public string KeyCurr_Status_Have_Ball()
    {
       
        string key = Directory_StatusCpu[Key_Trigger_Front].ToString() + Directory_StatusCpu[Key_Trigger_Back].ToString() + Directory_StatusCpu[Key_Trigger_ThrowBall].ToString();
        TextStatus.text = key;
        return key;
    }


    public void ProcessStatus(string key)
    {

        KeyActionCurr = key;

        if (KeyActionCurr != KeyActionPrevious)
        {
            OnActionWithKey(key);
        }




        //if (key == "00000")
        //{
        //    StartActionUpdate(null, OnMoveToBall);
        //}
        //else if(key == "10000")
        //{
        //    StartActionUpdate(null,OnMoveIde);
        //}
        KeyActionPrevious = KeyActionCurr;


    }
    public void ProcessStatusPlayerHaveBall(string key)
    {
        KeyActionCurr = key;
        if (KeyActionCurr != KeyActionPrevious)
        {
            OnAtionWithKey_Status_Have_Ball(key);
        }
        KeyActionPrevious = KeyActionCurr;
    }




    public void ReadFileCPU()
    {
      
        string text = textCPU.text;
        string[] w = new string[1] { "--Staus--" };
        string[] sss = text.Split(w,System.StringSplitOptions.None);

         for(int i = 0; i < sss.Length; i++)
        {
            Debug.Log("String : "+sss[i]);
        }

        string s = sss[0];
        string s1 = sss[1];
        StringReader strRead = new StringReader(s);
        StringReader strRead_1 = new StringReader(s1);
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
                //   Debug.Log(line);

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
                //   Debug.Log(line);

            }
            else
            {

                break;
            }
        }

       

    }


    private void FormatString_Status_Move_To_Ball(string line)
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


        Directory_Key_Status.Add(key, ArrayAction);

        //  Debug.Log("Key : "+key + " " + ss);

    }

    private void FormatString_Status_Have_Ball(string line)
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


        Directory_Key_Status_Have_Ball.Add(key, ArrayAction);

        //  Debug.Log("Key : "+key + " " + ss);

    }

    private void FormatString_Status_Player_Have_Ball(string line)
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


        Directory_Key_Status.Add(key, ArrayAction);

        //  Debug.Log("Key : "+key + " " + ss);

    }


    public void OnActionWithKey(string key)
    {
       
        Debug.Log(key);

        int r = Random.Range(0, Directory_Key_Status[key].Length);
        ActionGame a = Directory_Key_Status[key][r];
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
   
    public void OnAtionWithKey_Status_Have_Ball(string key)
    {
        Debug.Log(key);

        int r = Random.Range(0, Directory_Key_Status_Have_Ball[key].Length);
        ActionGame a = Directory_Key_Status_Have_Ball[key][r];
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

    public void RunActionTriiger(string key)
    {
        ActionGame a = Directory_OnActionGame[key];
        StartActionTrigger(a.ActionStart, a.ActionUpdate);

    }


    public void OnActionEFFWithKey(string key)
    {
        ActionGame a = Directory_OnActionGame[key];
        StartAcionEFFWithTime(a.ActionStart, a.ActionUpdate, a.ActionRemove, a.time);

    }



    public void RemoveActionTriggerWithKey(string key)
    {
        ActionGame a = Directory_OnActionGame[key];
        switch (a.typeAction)
        {
            case ActionGame.TypeAction.Action:



                break;
            case ActionGame.TypeAction.Update:


                break;

            case ActionGame.TypeAction.ActionEFF:


                break;
            case ActionGame.TypeAction.ActionTrigger:

                RemoveActionTrigger(a.ActionUpdate);

                break;


        }

    }

    public void RemoveActionTrigger(System.Action eventUpdate)
    {
        OnActionTrigger -= eventUpdate;
    }


    public void StartAcionWithTime(System.Action ActionTrigger, System.Action ActionUpdate, System.Action ActionRemove, float time)
    {
        if (ActionTrigger != null)
        {
            ActionTrigger();
        }

        System.Action expireAction = () =>
        {
            if (ActionRemove != null)
            {
                ActionRemove();
            }

        };

        System.Action<float> UpdateAction = (t) =>
        {
            ActionUpdate();

        };


        SetupTimer(time, UpdateAction, expireAction);

    }




    public void StartAcionEFFWithTime(System.Action ActionTrigger, System.Action ActionUpdate, System.Action ActionRemove, float time)
    {
        if (ActionTrigger != null)
        {
            ActionTrigger();
        }

        System.Action expireAction = () =>
        {
            if (ActionRemove != null)
            {
                ActionRemove();
            }

        };

        System.Action<float> UpdateAction = (t) =>
        {
            ActionUpdate();

        };


        SetupTimerActionEFF(time, UpdateAction, expireAction);

    }
    public void StartActionUpdate(System.Action ActionTrigger, System.Action ActionUpdate)
    {
        if (ActionTrigger != null)
        {
            ActionTrigger();
        }
        OnMove = null;
        OnMove += ActionUpdate;

    }

    public void StartActionTrigger(System.Action ActionTrigger, System.Action ActionUpdate)
    {
        if (ActionTrigger != null)
        {
            ActionTrigger();
        }
        OnActionTrigger += ActionUpdate;

    }




    public ActionGame GetAction(string key)
    {
        if (Directory_OnActionGame.ContainsKey(key))
        {
            return Directory_OnActionGame[key];
        }
        else
        {
            return null;
        }

        return null;
    }




    System.Action SetupTimer(float seconds, System.Action<float> updateAction, System.Action expireAction)
    {

        float timer = 0;

        System.Action expireWrapper = null;

        System.Action<float> updateWrapper = null;
        updateWrapper = (dt) =>
        {
            timer += dt;

            if (updateAction != null)
            {
                updateAction(dt);
            }

            if (timer >= seconds)
            {
                expireWrapper();
            }
        };

        OnAction += updateWrapper;



        expireWrapper = () =>
        {
            OnAction -= updateWrapper;



            if (expireAction != null)
            {
                expireAction();
            }
        };

        return expireWrapper;
    }


    System.Action SetupTimerActionEFF(float seconds, System.Action<float> updateAction, System.Action expireAction)
    {

        float timer = 0;

        System.Action expireWrapper = null;

        System.Action<float> updateWrapper = null;
        updateWrapper = (dt) =>
        {
            timer += dt;

            if (updateAction != null)
            {
                updateAction(dt);
            }

            if (timer >= seconds)
            {
                expireWrapper();
            }
        };

        OnActionEFF += updateWrapper;



        expireWrapper = () =>
        {
            OnActionEFF -= updateWrapper;



            if (expireAction != null)
            {
                expireAction();
            }
        };

        return expireWrapper;
    }



    public void SetKeyTrigger(string key)
    {
        Directory_StatusCpu[key] = 1;
    }

    public void SetRestoreTrigger(string key)
    {
        Directory_StatusCpu[key] = 0;
    }

    public void RestoreAll()
    {
        Directory_StatusCpu[Key_Trigger_Back] = 0;
        Directory_StatusCpu[Key_Trigger_Front] = 0;
        Directory_StatusCpu[Key_Trigger_Have_Ball] = 0;
        Directory_StatusCpu[Key_Trigger_Jump] = 0;
        Directory_StatusCpu[Key_Trigger_ThrowBall] = 0;
    }



    #endregion
    //void RemoveListener(string name, System.Action action)
    //{
    //    if (registry.ContainsKey(name) == false)
    //    {
    //        return;
    //    }

    //    registry[name].listeners -= action;
    //}

    //void AddListener(string name, System.Action action)
    //{
    //    if (registry.ContainsKey(name) == false)
    //    {
    //        registry[name] = new RegistryItem();
    //    }

    //    registry[name].listeners += action;
    //}


    public override void OnEvent(TrackEntry trackEntry, Spine.Event e)
    {
        if (trackEntry.Animation.Name == "swing")
        {
            var evenName = e.Data.Name;
            if (evenName == "atk")
            {
                BoxHurt.gameObject.SetActive(true);
                canHurt = true;
            }

        }



    }
    public override void OnComplete(TrackEntry trackEntry)
    {
        if (trackEntry.Animation.Name == "swing")
        {
            isAttack = false;
        }
    }



    #region EVENT
    public void Event_Reset()
    {

        StatusCurr = CharacterState.idle;
        OnActionEFF = null;
        OnAction = null;
        OnMove = null;
        isMoveLeft = false;
        isMoveRight = false;
        isJump = false;
        isAction = false;
        isBall = false;
        isPullBall = false;
        isStun = false;
        Body.constraints = RigidbodyConstraints2D.None;
        Body.constraints = RigidbodyConstraints2D.FreezeRotation;
       
    }
    #endregion
}

public class ActionGame
{
    public enum TypeAction { Update, Action ,ActionEFF,ActionTrigger};
    public TypeAction typeAction;
    public bool isAction;
    public System.Action ActionUpdate;
    public System.Action ActionRemove;
    public System.Action ActionStart;
    public float time;

    public ActionGame(System.Action ActionStart, System.Action UpdateAction)
    {
        typeAction = TypeAction.Update;
        ActionUpdate = UpdateAction;
        this.ActionStart = ActionStart;

    }
    public ActionGame(System.Action ActionStart, System.Action UpdateAction, System.Action RemoveAction, float time)
    {
        this.ActionStart = ActionStart;
        typeAction = TypeAction.Action;
        ActionUpdate = UpdateAction;
        ActionRemove = RemoveAction;
        this.time = time;
    }
    public ActionGame(System.Action ActionStart, System.Action UpdateAction, System.Action RemoveAction, float time,bool isActionEff)
    {
        this.ActionStart = ActionStart;
        typeAction = TypeAction.ActionEFF;
        ActionUpdate = UpdateAction;
        ActionRemove = RemoveAction;
        this.time = time;
    }

    public ActionGame(System.Action ActionStart, System.Action UpdateAction,System.Action RemoveAction)
    {
        typeAction = TypeAction.ActionTrigger;
        this.ActionStart = ActionStart;
        this.ActionUpdate = UpdateAction;
        this.ActionRemove = RemoveAction;
    }



   
}
class RegistryItem
{
    int _value;
    public int Value
    {
        get { return _value; }
        set
        {
            _value = value;
            if (listeners != null)
            {
                listeners();
            }
        }
    }

    public event System.Action listeners;
}