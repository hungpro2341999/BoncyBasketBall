﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PercitionGround { Ground_1,Ground_2,None};
public enum BallFlying { Yes,No};
public class Ball : Character
{
    public BallFlying Flying;
    public StatusBall Status;
    public PrecitionBall Percition;
    public GameObject[] Point;
    public GameObject[] Point_01;
    public Vector3 PrecitionBallGround;
    public PercitionGround PercitionGround;

    public GameObject[] ColliderIngore;
    public GameObject[] ColliderIngore_01;
    public int[] RestoreLayer;
    public int[] RestoreLayer_01;
    public int posPercition;

    public bool isHand;

    public static string KeyBall = "";
    public Character LastHand;
    public static float VelocityBall = 1.5f;
    public Text keyBall;
    public bool isPercitonWithBoard;

    public Vector3 PosPercitionHoop;

    public event System.Action PercitionBall;

    public delegate string GetStatusBall();
    public GetStatusBall event_getStatus;


    //
    private float Amount;
    private float PosInit;
    public int CountSperateDistance;
    public override void Start()
    {
        Amount = CtrlGamePlay.Ins.WidthScreen / CountSperateDistance;
        PosInit = CtrlGamePlay.Ins.WidthScreen / 2;

        base.Start();
        CtrlGamePlay.Ins.eventResetGame += Event_Reset;
        CtrlGamePlay.Ins.eventRestGamePlay += Event_Reset;

        RestoreLayer_01 = new int[ColliderIngore_01.Length];
       
    }
    // Start is called before the first frame update
    private void Update()
    {
        PredictionFall();
        Debug.Log(isCollJump().ToString());
    }

  
    public override void GetStatus()
    {
        LoadCurrPosionPlayer();
      

        //if (PercitionBall != null)
        //{
        //   // PercitionBall();
        //}
         //   PredictionFall();

        ////   PercitionForProtectBall();

        ////   PercitionForProtectBall();
        ////  PredictionFall();

      //  PredictionFall();

        if (KeyBall.ToString() != null)
            keyBall.text = "KeyBall : " + KeyBall.ToString();
    }
    public void LoadCurrPosionPlayer()
    {

        int point = (int)((PosInit - transform.position.x) / Amount);

        CurrPos = Mathf.Abs(point);

    }
    public void OffPercitionBall()
    {
        TurnOffSimulate();
        PercitionBall = null;

    }
    public void OnPercitionBall_0()
    {
        PercitionBall = null;

        PercitionBall += PredictionFall;
    }
    public void OnPercitionBall_1()
    {

        PercitionBall = null;
        PercitionBall += PercitionBall;
    }

    public void OnPercitionBall_2()
    {
        PercitionBall = null;
        PercitionBall += PredictionFall;
        PercitionBall += PercitionForProtectBall;
    }

    public string ControllerBy()
    {
        return event_getStatus();

    }

    public void NullKeyBall()
    {
        event_getStatus = null;
    }

    public void AddKeyBall_1()
    {
        event_getStatus = null;
        event_getStatus += SelectKey;
    }
    public void AddKeyBall_2()
    {

      
        event_getStatus = null;
        event_getStatus += SelectKey2;
    }

    public string SelectKey()
    {
        string s = "";
        string bitAI = "";
        string bitPlayer = "";
        if (CtrlGamePlay.Ins.AI.isBall)
        {
            bitAI = "1";
        }
        else
        {
            bitAI = "0";
        }

        if (CtrlGamePlay.Ins.Player.isBall)
        {
            bitPlayer = "1";
        }
        else
        {
            bitPlayer = "0";
        }

        s += bitAI + bitPlayer;



        KeyBall = s;
        return s;
    }

    public string SelectKey2()
    {
        string s = "";
        string bitAI = "";
        string bitPlayer = "";
        string bitBoardCpu = "";
        if (CtrlGamePlay.Ins.AI.isBall)
        {
            bitAI = "1";
        }
        else
        {
            bitAI = "0";
        }

        if (CtrlGamePlay.Ins.Player.isBall)
        {
            bitPlayer = "1";
        }
        else
        {
            bitPlayer = "0";
        }

        if (CtrlGamePlay.Ins.GetBall().CurrPos <= 3)
        {
            bitBoardCpu = "1";
        }
        else
        {
            bitBoardCpu = "0";
        }
        s += bitAI + bitPlayer + bitBoardCpu;



        KeyBall = s;
        return s;

    }

    public string KeyController_CPU_X_1()
    {
        string s = "";
        string bitAI = "";
        string bitPlayer = "";
        string bitBoardCpu = "";
        if (CtrlGamePlay.Ins.AI.isBall)
        {
            bitAI = "1";
        }
        else
        {
            bitAI = "0";
        }

        if (CtrlGamePlay.Ins.Player.isBall)
        {
            bitPlayer = "1";
        }
        else
        {
            bitPlayer = "0";
        }

        if (CtrlGamePlay.Ins.GetBall().isPercitonWithBoard)
        {
            bitBoardCpu = "1";
        }
        else
        {
            bitBoardCpu = "0";
        }
        s += bitAI + bitPlayer + bitBoardCpu;



        KeyBall = s;
        return s;

    }


    public string KeyController_CPU_X_0()
        {
        string s = "";
        string bitAI = "";
        string bitPlayer = "";
        if (CtrlGamePlay.Ins.AI.isBall)
        {
            bitAI = "1";
        }
        else
        {
            bitAI = "0";
        }

        if (CtrlGamePlay.Ins.Player.isBall)
        {
            bitPlayer = "1";
        }
        else
        {
            bitPlayer = "0";
        }

        s += bitAI + bitPlayer;



        KeyBall = s;
        return s;


    }





    public bool isHandByPlayer()
    {
        if (LastHand != null)
        {
            if(LastHand is AI)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            return false;
        }
        return false;

    }
    public override void Move()
    {
       
    }
    private void OnDrawGizmos()
    {
      //  Gizmos.DrawRay(new Ray(transform.position, Body.velocity.normalized));
       // Gizmos.DrawSphere(transform.position, 0.8f);
    }

    public void ChangeDirect(Vector3 Surface,float Force)
    {
        Body.velocity = Body.velocity * 2;
      //  Body.velocity = Vector3.Reflect(Body.velocity.normalized, Surface) *(1+ Force);

       
    }

    
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.layer == 13)
        {
          
            Body.isKinematic = false;
          
            Body.simulated = true;

            gameObject.GetComponent<SphereCollider>().isTrigger = false;
        }  
    }

    public override void CaculateStatus()
    {
        if (Body.isKinematic)
        {
            isHand = true;
        }
        else
        {
            isHand = false;
        }
    }

    
    public void PredictionFall()
    {
        
     

        Vector2 vec = Body.velocity;
        float x1 = transform.position.x;
        float y1 = transform.position.y;
        float InitX = transform.position.x;
        float InitY = transform.position.y;
          Vector2 InitPoint = new Vector2(InitX, InitY);
          RaycastHit2D hits;
        //  TurnOffSimulate();
          gameObject.layer = 2;
        int a = 0;
            for(int i=0;i<15;i++)
            {
            a = i;
                x1 = InitX + vec.x * (Time.fixedDeltaTime * i*1.2f);
                y1 = InitY + vec.y * (Time.fixedDeltaTime * i* 1.2f) - (0.5f * -Physics.gravity.y * Time.fixedDeltaTime * Time.fixedDeltaTime * i * i * 1.2f* 1.2f);
                Point[i].SetActive(true);
                Point[i].transform.position = new Vector2(x1, y1);
            hits = Physics2D.CircleCast(new Vector2(x1,y1), 0.3f,Vector2.zero);

           
               if (hits.collider!=null)
              {


                Debug.Log("Coll " + hits.collider.name);

                var AI = (AI)CtrlGamePlay.Ins.AI;
                PosPercitionHoop = Point[i].transform.position;



                break;
              }
            if (i == 14)
            {
                PosPercitionHoop = Point[i].transform.position;
            }

            }
        for (int i = a; i < 15; i++)
        {
          Point[i].SetActive(false);
            Point[i].GetComponent<CheckPoint>().Coll = false;
        }
            gameObject.layer = 16;

     



        



    }

    public bool isCollJump()
    {
        for(int i = 0; i < 15; i++)
        {
            if (Point[i].gameObject.activeSelf && Point[i].GetComponent<CheckPoint>().Coll)
            {
                return true;
            }

          
        }
        return false;
    }

    public void PercitionForProtectBall()
    {
     

    }

    public void TurnOffSimulate()
    {
        for (int i = 0; i < Point.Length; i++)
        {
            Point[i].gameObject.SetActive(false);
            Point[i].gameObject.GetComponent<CheckPoint>().Coll = false;
        }
    }
    public bool IsCollWithActionJump()
    {
        for (int i = 0; i < 15; i++)
        {
            if (Point[i].GetComponent<CheckPoint>().Coll)
            {
                return true;
            }
        }
        return false;
    }
  
    private void OnTriggerEnter2D(Collider2D collision)
    {
       

      

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 13)
        {
           
           LastHand = collision.gameObject.GetComponent<Character>();

            
        }

        if (collision.gameObject.name == "Circle")
        {

            AudioCtrl.Ins.Play("ColumnColl");
      
        }
        else
        {


            AudioCtrl.Ins.Play("BallColl");


        }

        if (collision.gameObject.layer == 11 || collision.gameObject.layer ==13)
        {
            if(LastHand is Player)
            {
                CtrlGamePlay.Ins.Player.SetUpTypeScore();
            }
            else
            {
                CtrlGamePlay.Ins.AI.SetUpTypeScore();
            }
        }
    }
  
  
   

    public void Event_Reset()
    {
        GetComponent<CircleCollider2D>().isTrigger = false;
        Body.isKinematic = false;
        Body.simulated = true;
        Body.velocity = Vector3.zero;
        transform.parent = CtrlGamePlay.Ins.TransGamePlay;

    }

  
}

