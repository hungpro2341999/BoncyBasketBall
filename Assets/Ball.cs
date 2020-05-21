using System.Collections;
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
    public Vector3 PrecitionPosFall;
    public PercitionGround PercitionGround;

    public GameObject[] ColliderIngore;
    public GameObject[] ColliderIngore_01;
    public int[] RestoreLayer;
    public int[] RestoreLayer_01;
    public int posPercition;

    public bool isHand;

    public static string KeyBall ="";
    public  Character LastHand;
    public static float VelocityBall = 1.5f;
    public Text keyBall;
    public bool isPercitonWithBoard;

    public Vector3 PosPercitionHoop;

    public event System.Action  PercitionBall;


    //
    private float Amount;
    private float PosInit;
    public  int CountSperateDistance;
    public override void Start()
    {
        Amount = CtrlGamePlay.Ins.WidthScreen / CountSperateDistance;
        PosInit = CtrlGamePlay.Ins.WidthScreen / 2;

        base.Start();
        CtrlGamePlay.Ins.eventResetGame += Event_Reset;
        CtrlGamePlay.Ins.eventRestGamePlay += Event_Reset;

        RestoreLayer_01 = new int[ColliderIngore_01.Length];
        for(int i = 0; i < ColliderIngore_01.Length; i++)
        {
            RestoreLayer_01[i] = ColliderIngore_01[i].gameObject.layer;
        }
    }
    // Start is called before the first frame update
    
    public override void GetStatus()
    {
     
        RaycastHit2D[] ray = Physics2D.RaycastAll(transform.position, Body.velocity.normalized);

        if (PercitionBall != null)
        {
            PercitionBall();
        }
        //  PredictionFall();

        //   PercitionForProtectBall();
        LoadCurrPosionPlayer();
        PercitionForProtectBall();
        if (KeyBall.ToString()!=null)
        keyBall.text = "KeyBall : "+KeyBall.ToString();
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



        //string s = "";
        //string bitAI = "";
        //string bitPlayer = "";
        //string bitBoardCpu = "";
        //if (CtrlGamePlay.Ins.AI.isBall)
        //{
        //    bitAI = "1";
        //}
        //else
        //{
        //    bitAI = "0";
        //}

        //if (CtrlGamePlay.Ins.Player.isBall)
        //{
        //    bitPlayer = "1";
        //}
        //else
        //{
        //    bitPlayer = "0";
        //}

        //if (CtrlGamePlay.Ins.GetBall().isPercitonWithBoard)
        //{
        //    bitBoardCpu = "1";
        //}
        //else
        //{
        //    bitBoardCpu = "0";
        //}
        //s += bitAI + bitPlayer + bitBoardCpu;



        //KeyBall = s;
        //return s;



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

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.layer == 11)
        {
           
                    string tag = collision.gameObject.tag;
                    if (tag == "Bottom")
                    {

                    }

                    if (tag == "Up")
                    {

                    }

                    if (tag == "Left")
                    {
                        GetComponent<Rigidbody>().AddForce(new Vector3(5, 0, 0), ForceMode.Force);
                    }

                    if (tag == "Rightt")
                    {
                        GetComponent<Rigidbody>().AddForce(new Vector3(-5, 0, 0), ForceMode.Force);
                    }
        }
               
            
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
        
        for(int i = 0; i < ColliderIngore.Length; i++)
        {
            ColliderIngore[i].gameObject.layer = 2;
        }   

        Vector2 vec = Body.velocity;
        float x1 = transform.position.x;
        float y1 = transform.position.y;
        float InitX = transform.position.x;
        float InitY = transform.position.y;
          Vector2 InitPoint = new Vector2(InitX, InitY);
          RaycastHit2D hits;
          TurnOffSimulate();
          gameObject.layer = 2;
            for(int i=0;i<20;i++)
            {
                x1 = InitX + vec.x * (Time.fixedDeltaTime * i*1.8f);
                y1 = InitY + vec.y * (Time.fixedDeltaTime * i * 1.8f) - (0.5f * -Physics.gravity.y * Time.fixedDeltaTime * Time.fixedDeltaTime * i * i * 1.8f * 1.8f);
                Point[i].SetActive(true);
                Point[i].transform.position = new Vector2(x1, y1);
            hits = Physics2D.CircleCast(new Vector2(x1,y1), 0.1f,Vector2.zero);
            
           
               if (hits.collider!=null)
             {
               
                


                var AI = (AI)CtrlGamePlay.Ins.AI;
                 posPercition = i - 2;
                posPercition = Mathf.Clamp(posPercition, 0, Point.Length);
                break;
             }

            }

        gameObject.layer = 16;

        for (int i = 0; i < ColliderIngore.Length; i++)
        {
            ColliderIngore[i].gameObject.layer = RestoreLayer[i];
        }

        



    }

    public void PercitionForProtectBall()
    {
        gameObject.layer = 2;
        for (int i = 0; i < ColliderIngore.Length; i++)
        {
            ColliderIngore[i].gameObject.layer = 2;
        }

        for (int i = 0; i < ColliderIngore_01.Length; i++)
        {
            ColliderIngore_01[i].gameObject.layer = 25;
        }

        Vector2 vec = Body.velocity;
        float x1 = transform.position.x;
        float y1 = transform.position.y;
        float InitX = transform.position.x;
        float InitY = transform.position.y;
        Vector2 InitPoint = new Vector2(InitX, InitY);
        RaycastHit2D hits;
        TurnOffSimulate_01();
       
        for (int i = 0; i < 20; i++)
        {
            x1 = InitX + vec.x * (Time.fixedDeltaTime * i * 3f);
            y1 = InitY + vec.y * (Time.fixedDeltaTime * i * 3f) - (0.5f * -Physics.gravity.y * Time.fixedDeltaTime * Time.fixedDeltaTime * i * i * 3 * 3);
            Point_01[i].SetActive(true);
            Point_01[i].transform.position = new Vector2(x1, y1);
            hits = Physics2D.CircleCast(new Vector2(x1, y1), 0.1f, Vector2.zero);

            if (i > 2)
            {
                if (Vector3.Distance(Point_01[i - 1].transform.position, Point_01[1].transform.position) < 0.2f)
                {
                    break;
                }
            }
            if (hits.collider != null)
            {

                if (hits.collider.gameObject.layer == 25)
                {
                    PosPercitionHoop = Point_01[i].transform.position;
                    isPercitonWithBoard = true;
                }
                else
                {
                    isPercitonWithBoard = false;
                }
                break;
              
               
            }

        }

      

        for (int i = 0; i < ColliderIngore.Length; i++)
        {
            ColliderIngore[i].gameObject.layer = RestoreLayer[i];
        }
        for (int i = 0; i < ColliderIngore_01.Length; i++)
        {
            ColliderIngore_01[i].gameObject.layer = RestoreLayer_01[i];
        }
        gameObject.layer = 16;

    }

    public void TurnOffSimulate()
    {
        for (int i = 0; i < Point.Length; i++)
        {
            Point[i].gameObject.SetActive(false);
        }
    }
    public void TurnOffSimulate_01()
    {
        for (int i = 0; i < Point_01.Length; i++)
        {
            Point_01[i].gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 17)
        {
            Flying = BallFlying.Yes;  
        }
        else
        {
            Flying = BallFlying.No; 
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 13)
        {
            LastHand = collision.gameObject.GetComponent<Character>();


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

