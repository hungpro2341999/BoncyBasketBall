using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PercitionGround { Ground_1,Ground_2,None};
public enum BallFlying { Yes,No};
public class Ball : Character
{
    public BallFlying Flying;
    public StatusBall Status;
    public PrecitionBall Percition;
    public GameObject[] Point;
    public Vector3 PrecitionPosFall;
    public PercitionGround PercitionGround;

    public GameObject[] ColliderIngore;
    public int[] RestoreLayer;
    public int posPercition;

    
    public override void Start()
    {
        base.Start();
    }
    // Start is called before the first frame update
   
    public override void GetStatus()
    {
     
        RaycastHit2D[] ray = Physics2D.RaycastAll(transform.position, Body.velocity.normalized);


        PredictionFall();




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
            for(int i=0;i<5;i++)
            {
                x1 = InitX + vec.x * (Time.fixedDeltaTime * i*3);
                y1 = InitY + vec.y * (Time.fixedDeltaTime * i*3) - (0.5f * -Physics.gravity.y * Time.fixedDeltaTime * Time.fixedDeltaTime * i*3 * i*3 );
                Point[i].SetActive(true);
                Point[i].transform.position = new Vector2(x1, y1);
            hits = Physics2D.CircleCast(new Vector2(x1,y1), 0.3f,Vector2.zero);
            
            if (hits.collider != null)
            {
                Debug.Log(hits.collider.gameObject.name);
            }
               if (hits.collider!=null)
            {
                if (hits.collider.gameObject.layer == 13 && hits.collider.gameObject.tag == "Box_1")
                {
                    Percition = PrecitionBall.HeadPlayer_1;
                }else if (hits.collider.gameObject.layer == 13 && hits.collider.gameObject.tag == "Box_2")
                {
                    Percition = PrecitionBall.HeadPlayer_2;
                    
                }else if (hits.collider.gameObject.layer == 13 && hits.collider.gameObject.tag == "Box_3")
                {
                    Percition = PrecitionBall.HeadPlayer_3;

                }
                else if (hits.collider.gameObject.layer == 13 && hits.collider.gameObject.tag =="Player")
                {
                    Percition = PrecitionBall.Player;
                }
                else if(hits.collider.gameObject.layer == 13 && hits.collider.gameObject.tag == "AI")
                {
                    Percition = PrecitionBall.AI;
                }
                else if(hits.collider.gameObject.layer == 11 && hits.collider.gameObject.tag == "Left")
                {
                    Percition = PrecitionBall.Left;
                }
                else if(hits.collider.gameObject.layer == 11 && hits.collider.gameObject.tag == "Right")
                {
                    Percition = PrecitionBall.Right;

                }
                else if(hits.collider.gameObject.layer == 11 && hits.collider.gameObject.tag == "Up")
                {
                    Percition = PrecitionBall.Up;
                }
                else if(hits.collider.gameObject.layer == 11 && hits.collider.gameObject.tag == "Bottom")
                {
                    Percition = PrecitionBall.Ground;
                }

                PrecitionPosFall = Point[i].transform.position;

                if (hits.collider.gameObject.layer == 11 && hits.collider.gameObject.tag == "Bottom"
                    && hits.collider.gameObject.transform.position.x >= CtrlGamePlay.Ins.WidthScreen / 2 && hits.collider.gameObject.transform.position.x <= 0)
                {
                    PercitionGround = PercitionGround.Ground_1;
                }
                else
                {
                    PercitionGround = PercitionGround.Ground_2;
                }

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
    public void TurnOffSimulate()
    {
        for (int i = 0; i < Point.Length; i++)
        {
            Point[i].gameObject.SetActive(false);
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

}

