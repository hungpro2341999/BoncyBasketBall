using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public enum StatusAI {DirectToBall,HaveBall,NotHaveBall,};

public enum StatusBall {DirectToGround,DirectNotToGround,DirectToPlayer,DirectToAI,DirectToSky,None};
public enum PrecitionBall { Player,AI,Ground,Left,Right,Up,HeadPlayer_1,HeadPlayer_2, HeadPlayer_3};
public enum StatusAction { };



public class CtrlGamePlay : MonoBehaviour
{
   
    public static CtrlGamePlay Ins;

    public Character Player;
    public Character AI;
    public Character Ball;

    public float WidthScreen;
    public float HeightScreen;

    //

    public float h = 3;
    public float graviry = -9.8f;

   


    private void Awake()
    {
        if (Ins != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Ins = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Physics2D.gravity = Vector3.up * graviry;
        var a = (Player)Player;
        a.isInputMove = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
     
    }
   

  
    public void ThrowBall()
    {
       
       
     
    }

    public void Launch(float height,Vector3 Target)
    {
       
        var a = (Player)Player;
        var b = (AI)AI;

        a.isBall = false;
        b.isBall = false;
        Ball.transform.transform.parent = null;
        Ball.GetComponent<CircleCollider2D>().isTrigger = false;
        Ball.Body.isKinematic = false;
        Ball.Body.simulated = true;
        Ball.Body.velocity = CaculateVelocity(height,Target).InitVelocity;
    }
    LauchData CaculateVelocity(float height, Vector3 TargetTo)
    {
       Vector3 Target = TargetTo;
        float h = height;
        float displacementY = Target.y - Ball.transform.position.y;
        Vector3 displacementX = new Vector3((Target.x - Ball.transform.position.x),0,0);

        float time = Mathf.Sqrt(-2 * h / graviry) + Mathf.Sqrt(2 * (displacementY - h) / graviry);

        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * graviry * h);
        Vector3 velocityX = displacementX / time;

        return new LauchData(velocityX + velocityY * -Mathf.Sign(graviry), time);

    }
    struct LauchData
    {
        public readonly Vector3 InitVelocity;
        public readonly float timeToTarget;
        public LauchData(Vector3 InitVelocity, float timeToTarget)
        {
            this.InitVelocity = InitVelocity;
            this.timeToTarget = timeToTarget;
        }
    }
    
}
