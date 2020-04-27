using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RelizeBall { Right,Left,Up,Down};
public enum IsSameDirectWithBall  { Yes, No };
public enum IsSameDirectWithPlayer { Yes, No};


public enum StatusWithPlayer { BlockByPlayer, NotBlock,None};
public enum StatusHaveBall { Yes,No}




public class AI : Character
{

    #region InforAI

    public float ForceHead;
    public float SpeedShoe;

    #endregion


    public bool isMoveRight;
    public bool isMoveLeft;
    public bool isJump;
    public bool isJump_x2;
    public bool isThrowBall;


    public bool isBall;

    public float Left;
    public float Right;
    public float Up;
    public float ThrowBall;



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

    public float TargetX;
    public float TargetX_1;
    public bool isInTarget_1;
    public float timeJumpX2;


    public Transform PosHand;
    // Action
    string[] Jump = { "jumpRight", "jumpLeft", "jumpStraght", "Area", "JumpFoward", "ProtectBasket", "CatchBall" };

    private const string Key_Catch_Ball = "CatchBall";


    Dictionary<string, int> ActionJump = new Dictionary<string, int>();


    public override void Start()
    {
        Amount = CtrlGamePlay.Ins.WidthScreen / CountSperateDistance;

        MatrixPositonAi = new int[CountSperateDistance];

        PosInit = CtrlGamePlay.Ins.WidthScreen / 2;

        Setup();

        base.Start();

        TargetX = transform.position.x;

    }

    #region MainAi
    public override void CaculateStatus()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            CtrlGamePlay.Ins.Launch(Random.Range(4, 6));
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartMove();
        }
        if (!isBall)
        {
            if (OnAction != null)
            {
                OnAction(Time.deltaTime);
            }
            else
            {

                base.CaculateStatus();

                var ball = (Ball)CtrlGamePlay.Ins.Ball;
                var player = (Player)CtrlGamePlay.Ins.Player;

                if (ChangeStatus)
                {
                    Debug.Log("Change");
                    ChangeStatus = false;

                    //  SetUpRandomStatus();
                }
            }


            MoveToWardBall();
        }
        else
        {
         
            isMoveLeft = false;
            isMoveRight = false;
        }
        


    }
    public void Load_Matrix()
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
            Velocity = speed * Vector2.right;
        }

        if (isMoveLeft)
        {
            Velocity = speed * Vector2.left;
        }

        if (isJump)
        {
            isJump = false;
            if (isGround)
            {
                Body.velocity = new Vector2(Body.velocity.x, 0);
                Body.AddForce(Force * Vector2.up, ForceMode2D.Impulse);


            }
        }
        else if (isJump_x2)
        {
            timeJumpX2 -= Time.deltaTime;

            if (timeJumpX2 <= 0)
            {
                isJump_x2 = false;
                Body.AddForce(Force * Vector2.up * 1.4f, ForceMode2D.Impulse);
            }

        }
        if (isThrowBall)
        {
            isThrowBall = false;
            CtrlGamePlay.Ins.ThrowBall();
        }




    }

    public void CatchBall()
    {
        Debug.Log("CatchBall");
        var collision = (Ball)CtrlGamePlay.Ins.Ball;
        collision.GetComponent<Ball>().Velocity = Vector3.zero;
        collision.GetComponent<Ball>().Body.isKinematic = true;
        collision.GetComponent<CircleCollider2D>().isTrigger = true;
     
        collision.transform.parent = PosHand;
        collision.transform.localPosition = Vector3.zero;
        isBall = true;

    }


    public override void GetStatus()
    {
        base.GetStatus();


        var b = Physics2D.RaycastAll(transform.position, Vector2.left, 100, LayerPlayer);

        // Debug.Log(b.Length);
        for (int i = 0; i < b.Length; i++)
        {
            //   Debug.Log(b[i].collider.gameObject.name);
            if (b[i].collider.gameObject.tag == "Player")
            {
                DistanceToPlayer = Mathf.Abs(transform.position.x - b[i].point.x);
                //   Debug.Log(DistanceToPlayer);
                break;

            }
            else
            {
                DistanceToPlayer = -1;
            }
        }


        var c = (Ball)CtrlGamePlay.Ins.Ball;

        //  Move_Catch_Ball(c.transform.position);

        var d = (Player)CtrlGamePlay.Ins.Player;

        if (Mathf.Sign(transform.position.x - CtrlGamePlay.Ins.Player.transform.position.x) == 1)
        {
            isSameWithPlayer = IsSameDirectWithPlayer.Yes;
        }
        else
        {
            isSameWithPlayer = IsSameDirectWithPlayer.No;
        }
        if (Mathf.Sign(transform.position.x - CtrlGamePlay.Ins.Ball.transform.position.x) == 1)
        {
            isSameWithBall = IsSameDirectWithBall.Yes;
        }
        else
        {
            isSameWithBall = IsSameDirectWithBall.No;
        }

        if (DistanceToPlayer <= 2 && isSameWithPlayer == IsSameDirectWithPlayer.Yes && d.Status_Player != StatusPlayer.Jump)
        {
            StatusWithPlayer = StatusWithPlayer.BlockByPlayer;
        }
        else
        {
            StatusWithPlayer = StatusWithPlayer.NotBlock;
        }
        if (isBall)
        {
            StatusHaveBall = StatusHaveBall.Yes;
        }
        else
        {
            StatusHaveBall = StatusHaveBall.No;
        }

        Load_Matrix();
    }
    private void LateUpdate()
    {
        Body.velocity = new Vector2(((isMoveRight ? 1 : 0) + (isMoveLeft ? -1 : 0)) * speed, Body.velocity.y);
        //if (isLimit)
        //{
        //    Body.velocity = new Vector2(0, Body.velocity.y);
        //}
        if (Input.GetKeyDown(KeyCode.A))
        {
            StartX2Jump();
        }
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
    public void Setup()
    {

        ActionJump.Add(Jump[0], 0);
        ActionJump.Add(Jump[1], 0);
        ActionJump.Add(Jump[2], 0);
        ActionJump.Add(Jump[3], 0);
        ActionJump.Add(Jump[4], 0);
        ActionJump.Add(Jump[5], 0);


    }
    public void SetValue(string key)
    {
        ActionJump[key] = 1;
        if (ActionJump[Jump[4]] == 1)
        {
            StartAcion(OnJumpStraight, ResetJump, 0.8f);
        }
      


        //if (ActionJump[Jump[4]] == 1)
        //{
        //    StartAcion(OnJumpStraight, ResetJump, 0.8f);
        //}
        //else if (ActionJump[Jump[5]] == 1)
        //{
        //    StartX2Jump();
        //}

        //if (ActionJump[Jump[3]] == 1)
        //{
        //    if (isCheckValue())
        //    {
        //        var b = (Ball)CtrlGamePlay.Ins.Ball;

        //        if (ActionJump[Jump[0]] == 1)
        //        {
        //            StartAcion(OnJumpRight, ResetJump, 0.8f);

        //        }
        //        else if (ActionJump[Jump[1]] == 1)
        //        {
        //            StartAcion(OnJumpLeft, ResetJump, 0.8f);
        //        }
        //        else
        //        {
        //            StartAcion(OnJumpStraight, ResetJump, 0.8f);
        //        }


        //    }
        //}
    }

    public bool isCheckValue()
    {
        if ((ActionJump[Jump[0]] + ActionJump[Jump[1]] + ActionJump[Jump[2]]) == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
        return false;
    }
    public void ResetActionJump(string key)
    {
        if (key == Jump[3])
        {
            ActionJump[Jump[0]] = 0;
            ActionJump[Jump[1]] = 0;
            ActionJump[Jump[2]] = 0;
            ActionJump[Jump[3]] = 0;
        }
        if (key == Jump[4])
        {
            ActionJump[Jump[4]] = 0;

        }
        if (key == Jump[5])
        {
            ActionJump[Jump[5]] = 0;

        }


    }
    #endregion

    #region ActionPullBall
    
    public void OnPullNormal()
    {
        CtrlGamePlay.Ins.Launch(Random.Range(4,6));

    }

    #endregion

    public void SetUpRandomStatus()
    {

        int r = Random.Range(0, 100);
        if (0 <= r && r < Left)
        {
            isMoveLeft = true;
            isMoveRight = false;
        }
        if (Left <= r && r <= Right)
        {
            isMoveLeft = false;
            isMoveRight = true;
        }
        if (Right < r && r < Up)
        {
            isJump = true;
        }
        if (Up <= r && r <= ThrowBall)
        {
            isThrowBall = true;
        }






    }
    public void SetStatus(int left, int right, int up, int throwBall)
    {
        this.Left = left;
        if (right != 0)
        {
            this.Right = right + Left;
        }

        if (up != 0)
        {
            this.Up = right + left;
        }
        if (throwBall != 0)
        {
            this.ThrowBall = up + right + left;
        }
    }






    // Action:
    // Jump _ 1:
    public void JumpPushBall()
    {
        if (DistanceToPlayer <= 2)
        {

            isJump = true;
        }

    }
    // Jump_2
    public void PrecJumpPushBall()
    {
        if (DistanceToPlayer <= 2)
        {
            int r = Random.Range(0, 30);
            if (r % 3 == 0)
            {
                isJump = true;
            }

        }

    }

    public void StartX2Jump()
    {
        if (!isJump_x2)
        {
            timeJumpX2 = 0.5f;
            isJump = true;
            isJump_x2 = true;
        }


    }

    public void Start_Jump_x2()
    {
        isJump_x2 = true;
    }
    // Jump_3

    // Move_1:
    public void StartMove()
    {
        var ball = (Ball)CtrlGamePlay.Ins.Ball;

        int x = ball.posPercition;

        TargetX_1 = ball.Point[x].transform.position.x;
        TargetX_1 = 0;


    }

    public void MoveToTarget_1()
    {
        if (Mathf.Abs(TargetX_1 - transform.position.x) >= 0.1f)
        {
            if (Mathf.Sign(TargetX_1 - transform.position.x) == 1)
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

    public void Move_Catch_Ball(Vector3 PosTarrget)
    {



    }

    public void Move_Random()
    {

    }
    public void Active_Jump()
    {

    }



    public void StartAcion(System.Action ActionUpdate, System.Action ActionRemove, float time)
    {
        isJump = true;
        System.Action expireAction = () =>
        {
            ActionRemove();
        };

        System.Action<float> UpdateAction = (t) =>
        {
            ActionUpdate();

        };


        SetupTimer(time, UpdateAction, expireAction);

    }

    

    // Move
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



    public void MoveRandom()
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
        }
        else
        {
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

    public void MoveToWardBall()
    {
        var a = (Ball)CtrlGamePlay.Ins.Ball;
        if (Mathf.Abs(transform.position.x - a.transform.position.x) >= 0.1f)
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

    public event System.Action<float> OnAction;
    Dictionary<string, RegistryItem> registry;
    string[] Action = { "JumpLeft", "JimpRight" };

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
    void RemoveListener(string name, System.Action action)
    {
        if (registry.ContainsKey(name) == false)
        {
            return;
        }

        registry[name].listeners -= action;
    }

    void AddListener(string name, System.Action action)
    {
        if (registry.ContainsKey(name) == false)
        {
            registry[name] = new RegistryItem();
        }

        registry[name].listeners += action;
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


}


