using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using UnityEngine.UI;
using System.IO;

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



    public const string Key_Trigger_Have_Ball = "Trigger_Ball";
    public const string Key_Trigger_Jump = "Trigger_Jump";
    public const string Key_Trigger_ThrowBall = "Trigger_Throw_Ball";
    public const string Key_Trigger_Front = "Trigger_Front";
    public const string Key_Trigger_Back = "Trigger_Back";
   







 
    
    Dictionary<string, ActionGame> Directory_OnActionGame = new Dictionary<string,ActionGame>();

    Dictionary<string, int> Directory_StatusCpu = new Dictionary<string, int>();

    Dictionary<string, ActionGame[]> Directory_Key_Status = new Dictionary<string, ActionGame[]>();

    Dictionary<string, RegistryItem> registry;

    public event System.Action<float> OnAction;
    public event System.Action OnMove;



    public Stack<System.Action> Stack_Active_Action = new Stack<System.Action>();

    public string KeyActionCurr;
    public string KeyActionPrevious;

    [Header("Debug")]

    public Text TextStatus;
    public bool test = false;

    public Transform BoxCatchBall;
   
    public enum TypeRandomMove {RandomDistance,RandomInGround1,RandomInGround2}
    [Header("Move")]

    public Vector3 TargetHoop;

     

    // Move Random
    public float TargetX;

    public float DistanceRandom;

    public float TargetRandom;
    




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

        // bla bla
        Amount = CtrlGamePlay.Ins.WidthScreen / CountSperateDistance;

        MatrixPositonAi = new int[CountSperateDistance];

        PosInit = CtrlGamePlay.Ins.WidthScreen / 2;
        TargetX = transform.position.x;

        TargetHoop = GameObject.FindGameObjectWithTag("TargetCPU").transform.position;

        base.Start();

        
        Init();
    }

    
   
    #region Core
    public override void CaculateStatus()
    {
        
        if(OnAction == null)
        {
            ProcessStatus(KeyCurr());
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

       
      
        if (isMoveRight)
        {
            if (isGround)
            {
                StatusCurr = CharacterState.move1;
            }
           
            Velocity = speed * Vector2.right;
        }

        if (isMoveLeft)
        {
            if (isGround)
            {
                StatusCurr = CharacterState.move2;
            }
           
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
                if (isGround)
                {
                   
                    isStartJump = true;
                    isGround = false;
                    StatusCurr = CharacterState.jumb1;
                    Body.velocity = new Vector2(Body.velocity.x, 0);
                    Body.AddForce(Force * Vector2.up, ForceMode2D.Impulse);


                }
            }
        }
        else
        {
            isJump = false;
        }
     
       
        //if (isThrowBall)
        //{
        //    isThrowBall = false;
        //    CtrlGamePlay.Ins.ThrowBall();
        //}

        if (isStartJump)
        {
            if (!isGround)
            {
                BoxCatchBall.gameObject.SetActive(false);
                if (m_timeStartJump > 0)
                {
                    m_timeStartJump -= Time.deltaTime;
                }
                else
                {
                    TriggerBallForward.gameObject.SetActive(false);
                    if (Mathf.Sign(Body.velocity.y) == 1)
                    {
                        StatusCurr = CharacterState.jumb2;

                    }
                    else
                    {
                        if (isPullBall)
                        {
                            if (isBall)
                            {
                                isBall = false;
                                CtrlGamePlay.Ins.Launch(Random.Range(2, 3),TargetHoop);
                                isPullBall = false;

                            }



                        }
                        StatusCurr = CharacterState.jumb3;

                    }

                }
            }
            else
            {
                BoxCatchBall.gameObject.SetActive(true);
                TriggerBallForward.gameObject.SetActive(true);
                isPullBall = false;
                isStartJump = false;
                StatusCurr = CharacterState.idle;
            }



        }




        if (!(isMoveRight || isMoveLeft || isStartJump))
        {
            StatusCurr = CharacterState.idle;
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

    #region ActionPullBall
    
 

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
   

    private void OnTriggerMoveRandom()
    {

    }


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
        if (Mathf.Abs(transform.position.x - a.transform.position.x) >= 0.4f)
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

    // Trigger
    public void ActiveTriggerProctectBall()
    {

    }

    public void Add_Action_AI()
    {

    }
    /// <summary>
    /// 
    /// </summary>
    #region ActionKey
    public void Init()
    {
        // Initialize Action With Key


        ActionGame lc_OnActionMove = new ActionGame(null,OnMoveToBall);
        ActionGame lc_OnJump = new ActionGame(OnJump,OnJumpStraight, null, 0.8f);
        ActionGame lc_OnDie = new ActionGame(null,OnMoveIde);
        ActionGame lc_OnMoveRandom = new ActionGame(null,OnMoveRandom);
        ActionGame lc_onMoveToHoop = new ActionGame(null,OnMoveToHoop);
        ActionGame lc_OnJump_left = new ActionGame(OnJump,OnJumpLeft, null, 1.7f);
        ActionGame lc_OnJump_right = new ActionGame(OnJump,OnJumpRight, null, 1.7f);
        
        // Update To Directory

        //  Update
        Directory_OnActionGame.Add(Key_Move_To_Ball, lc_OnActionMove);
        Directory_OnActionGame.Add(Key_Move_Ide, lc_OnDie);
        Directory_OnActionGame.Add(Key_Move_Random, lc_OnMoveRandom);
        Directory_OnActionGame.Add(Key_Move_To_Hoop, lc_onMoveToHoop);

      

        //  Action

        Directory_OnActionGame.Add(Key_Jump_Straight,lc_OnJump);
        Directory_OnActionGame.Add(Key_Jump_Left, lc_OnJump_left);
        Directory_OnActionGame.Add(Key_Jump_Right, lc_OnJump_right);
        

        // Status

        Directory_StatusCpu.Add(Key_Trigger_Have_Ball, 0);
        Directory_StatusCpu.Add(Key_Trigger_Front, 0);
        Directory_StatusCpu.Add(Key_Trigger_Back, 0);
        Directory_StatusCpu.Add(Key_Trigger_Jump, 0);
        Directory_StatusCpu.Add(Key_Trigger_ThrowBall, 0);

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

    public void ReadFileCPU()
    {
        string s = textCPU.text;

        StringReader strRead = new StringReader(s);
        while (true)
        {

            string line = strRead.ReadLine();
            if (line != null)
            {
                if (line.StartsWith("//") || line=="")
                {
                    continue;
                }
                
                FormatString(line);
             //   Debug.Log(line);

            }
            else
            {

                break;
            }
        }

    }
    private void FormatString(string line)
    {
        string ss = "";
        Debug.Log("Line Format : " + line);
        string[] w = new string[1] { "." };
        string[] w1 = new string[1] { "=" };
        string[] w2 = new string[1] { "||" };
        string[] word = line.Split(w, System.StringSplitOptions.None);
      
        for(int i = 0; i < word.Length; i++)
        {
        //    Debug.Log(word[i]);
        }
        string[] word2 = word[1].Split(w1, System.StringSplitOptions.None);
        string key = word2[0].Trim();
        string[] value = word2[1].Split(w2, System.StringSplitOptions.None);
        string[] ArrayValue = new string[value.Length];
        for(int i = 0; i < value.Length; i++)
        {
            ArrayValue[i] = value[i].Trim();
            ss += " " + ArrayValue[i];
           
        }

        ActionGame[] ArrayAction = new ActionGame[value.Length];

        for(int i=0; i < ArrayValue.Length; i++)
        {
            Debug.Log(ArrayValue[i]);
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

                StartActionUpdate(a.ActionStart , a.ActionUpdate);
                break;
        }
    }



    public void StartAcionWithTime(System.Action ActionTrigger,System.Action ActionUpdate, System.Action ActionRemove, float time)
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


    public void StartActionUpdate(System.Action ActionTrigger,System.Action ActionUpdate)
    {
        if (ActionTrigger != null)
        {
            ActionTrigger();
        }
        OnMove = null;
        OnMove += ActionUpdate;
       
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

  


}

public class ActionGame
{
    public enum TypeAction { Update, Action };
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