using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum StatusAI {DirectToBall,HaveBall,NotHaveBall,};

public enum StatusBall {DirectToGround,DirectNotToGround,DirectToPlayer,DirectToAI,DirectToSky,None};
public enum PrecitionBall { Player,AI,Ground,Left,Right,Up,HeadPlayer_1,HeadPlayer_2, HeadPlayer_3};
public enum StatusAction { };



public class CtrlGamePlay : MonoBehaviour
{
   
    public static CtrlGamePlay Ins;

    [Header("STatusGame")]
    public bool isPlaying = false;

    public Character Player;
    public Character AI;
    public Character Ball;

    public float WidthScreen;
    public float HeightScreen;

   

    //

    public float h = 3;
    public float graviry = -9.8f;

    //  Delegate

    public CheckInBall[] Board;


    public delegate void OnResetGamePlay();
    public delegate void OnGlobal();
    public delegate void OnResetGame();

    //   Event

    public event OnResetGamePlay eventRestGamePlay;
    public event OnGlobal event_Global_Game;
    public event OnResetGame eventResetGame;


    public Vector3 PosInitPlayer;
    public Vector3 PosInitCPU;
    public Vector3 PosInitBall;

    

    [Header("Render")]

    public Text TextTime;
    public Text TextPlayer;
    public Text TextCPU;
    public int ScorePlayer = 0;
    public int ScoreAI = 0;


    [Header("TimeGame")]

    public float timeResetGamePlay;
    public float timeGamePlay;

    #region Private Static Variable

    public static bool isGlobal = false;


    #endregion


    #region Private Variable

    private float m_timeGame;

    #endregion
    [Header("Transform")]

    public Transform TransGamePlay;


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

        PosInitPlayer = GameObject.FindGameObjectWithTag("PosInitPlayer").transform.position;
        PosInitCPU = GameObject.FindGameObjectWithTag("PosInitCPU").transform.position;
        PosInitBall = GameObject.FindGameObjectWithTag("PosInitBall").transform.position;

        eventRestGamePlay += ResetGamePlay;
        eventResetGame += RestGame;


    }
    // Start is called before the first frame update
    void Start()
    {
        Physics2D.gravity = Vector3.up * graviry;
        var a = (Player)Player;
        a.isInputMove = true;
        eventResetGame();


     

    }


    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            ResetGamePlay();
        }
           


        if (m_timeGame >= 0)
        {
            TextTime.text = ((int)m_timeGame).ToString();
            m_timeGame -= Time.deltaTime;
        }
        else
        {
            isPlaying = false;
            
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

    public void PushBall(Vector3 direct)
    {
       // Debug.Log("PushBall : "+direct);
        var a = (Player)Player;
        var b = (AI)AI;

        a.isBall = false;
        b.isBall = false;
        Ball.transform.transform.parent = null;
        Ball.GetComponent<CircleCollider2D>().isTrigger = false;
        Ball.Body.isKinematic = false;
        Ball.Body.simulated = true;
        Ball.Body.AddForce(direct, ForceMode2D.Force);


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

    #region  EventGame
    public void GlobalCPU()
    {
        ScorePlayer++;
        RenderScore();

        StartCoroutine(Rest_Game_Play(timeResetGamePlay));
    }

    public void GlobalPlayer()
    {
        ScoreAI++;
       
        RenderScore();

        StartCoroutine(Rest_Game_Play(timeResetGamePlay));
    }


    IEnumerator Rest_Game_Play(float time)
    {
        
        yield return new WaitForSeconds(time);
        eventRestGamePlay();
    }

    public void RestGame()
    {
        for (int i = 0; i < Board.Length; i++)
        {
            Board[i].RestorCheckBoard();
        }
        isPlaying = true;
        m_timeGame = timeGamePlay;
        Player.transform.position = PosInitPlayer;
        AI.transform.position = PosInitCPU;
        Ball.transform.position = PosInitBall;
        RenderScore();
    }
    



    public void ResetGamePlay()
    {
        for (int i = 0; i < Board.Length; i++)
        {
            Board[i].RestorCheckBoard();
        }
        Player.transform.position = PosInitPlayer;
        AI.transform.position = PosInitCPU;
        Ball.transform.position = PosInitBall;
        RenderScore();
    }


    #endregion

    #region Method Priate

    private void RenderScore()
    {
        TextPlayer.text = ScorePlayer.ToString();
        TextCPU.text = ScoreAI.ToString();

    }


    #endregion



}
