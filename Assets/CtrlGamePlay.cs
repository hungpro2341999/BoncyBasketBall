using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum StatusAI {DirectToBall,HaveBall,NotHaveBall,};

public enum StatusBall {DirectToGround,DirectNotToGround,DirectToPlayer,DirectToAI,DirectToSky,None};
public enum PrecitionBall { Player,AI,Ground,Left,Right,Up,HeadPlayer_1,HeadPlayer_2, HeadPlayer_3};
public enum StatusAction { };

public enum TypeScore {Point_2,Point_3,Clean_Shoot,JumpBall}

public class CtrlGamePlay : MonoBehaviour
{
   
    public static CtrlGamePlay Ins;

    [Header("StatusGame")]
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

    [Header("Perb")]

    public Score_Game objScore;
    
    


    [Header("Render")]

    public Text TextTime;
    public Text TextPlayer;
    public Text TextCPU;
   


    [Header("TimeGame")]

    public float timeResetGamePlay;
    public float timeGamePlay;
    public float timeWaitForPerSecond;


    [Header("SCore")]
    public int ScorePlayer = 0;
    public int ScoreAI = 0;
    public Sprite[] ImgScore;
    public GameObject[] WaitForStart;

    [Header("Anim_Game_Play")]
   
    public Animator Anim_Score;

    [Header("Source")]

    public Sprite[] Scoure_Img_Score;
    
    #region Private Static Variable

    public static bool isGlobal = false;
    

    #endregion


    #region Private Variable

    private float m_timeGame;

    #endregion
    [Header("Transform")]

    public Transform TransGamePlay;
    public Transform TransScore;


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

        

        eventRestGamePlay += ResetGamePlay;
        eventResetGame += RestGame;


    }
    // Start is called before the first frame update
    void Start()
    {

        PosInitPlayer = GameObject.FindGameObjectWithTag("PosInitPlayer").transform.position;
        PosInitCPU = GameObject.FindGameObjectWithTag("PosInitCPU").transform.position;
        PosInitBall = GameObject.FindGameObjectWithTag("PosInitBall").transform.position;
        Application.targetFrameRate = 120;
        Physics2D.gravity = Vector3.up * graviry;
        var a = (Player)Player;
        a.isInputMove = true;
        //    eventResetGame();

       // Time.timeScale = 0.4f;
     

    }


    

    // Update is called once per frame
    void Update()
    {
        Application.targetFrameRate = 120;
        if (Input.GetKeyDown(KeyCode.W))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(IE_WaitForGameStart());
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

    public void CpuThrowBall()
    {
        var CPU = (AI)AI;

        CPU.type = CPU.GetTypeScore();
        int currPos = (int)Mathf.Abs((CtrlGamePlay.Ins.WidthScreen / 2 - CPU.transform.position.x) / CPU.Amount);
        float PercentageX =  CPU.TargetHoop.x + Random.Range(-CPU.PercentageThrowBall,CPU.PercentageThrowBall)*CPU.PercentageDistance *Mathf.Clamp((currPos), 1, 12) * 0.5f;
        float PercentageY = CPU.TargetHoop.y + Random.Range(0, CPU.PercentageThrowBall) * CPU.PercentageDistance * Mathf.Clamp((currPos), 1, 12) * 0.2f;
        float yJump = Mathf.Abs(transform.position.y - Ball.transform.position.y);
        yJump += Random.Range(2, 4);
        yJump = Mathf.Clamp(yJump, 0.5f, 5);
        Launch(yJump, new Vector3(PercentageX, PercentageY, 0));
    }
    public void PlayerThrowBall()
    {
        var player = (Player)Player;
        player.type = player.GetTypeScore();
        int currPos = (int)Mathf.Abs((CtrlGamePlay.Ins.WidthScreen / 2 - player.transform.position.x) / player.Amount);
        float PercentageX = player.TargetHoop.x + Random.Range(-player.PercentageThrowBall, player.PercentageThrowBall) * Mathf.Clamp((currPos), 1, 12) * 0.5f;
        float PercentageY = player.TargetHoop.y + ((Random.Range(0, player.PercentageThrowBall) * player.PercentageDistance * Mathf.Clamp((currPos), 1, 12)*0.2f));
        float yJump = Mathf.Abs(transform.position.y - Ball.transform.position.y);
        yJump += Random.Range(2, 4);
        yJump = Mathf.Clamp(yJump,0.5f, 5);
        Launch(yJump, new Vector3(PercentageX, PercentageY, 0));
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
       // Debug.Log("Velocity : "+CaculateVelocity(height, Target).InitVelocity);
        try
        {
            
            Ball.Body.velocity = CaculateVelocity(height, Target).InitVelocity;
        }
        catch(System.Exception e)
        {
            Ball.Body.velocity = Vector3.zero;
        }
       
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
        if (h < 1)
        {
            h = Random.Range(2, 4);
        }
        float displacementY = Target.y - Ball.transform.position.y;
        Vector3 displacementX = new Vector3((Target.x - Ball.transform.position.x),0,0);

        float time = Mathf.Sqrt(-2 * h / graviry) + Mathf.Sqrt(2 * (displacementY - h) / graviry);

        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * graviry * h);
        Vector3 velocityX = displacementX / Mathf.Clamp(time,0.5f,100);

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


    

    public void SetScore(TypeScore score)
    {
        var a = Instantiate(objScore, TransScore);
        switch (score)
        {
            case TypeScore.Point_2:
                a.ChangeImg(Scoure_Img_Score[0]);
                break;
            case TypeScore.Point_3:
                a.ChangeImg(Scoure_Img_Score[1]);
                break;
            case TypeScore.Clean_Shoot:
                a.ChangeImg(Scoure_Img_Score[2]);
                break;
            case TypeScore.JumpBall:
                a.ChangeImg(Scoure_Img_Score[3]);
                break;
        }
        
    }

    IEnumerator IE_WaitForGameStart()
    {
        for (int i = 0; i < WaitForStart.Length; i++)
        {
            WaitForStart[i].gameObject.SetActive(false);
        }
        yield return new WaitForSeconds(0);

        for (int i = 0; i < WaitForStart.Length; i++)
        {

            yield return new WaitForSeconds(timeWaitForPerSecond);
            WaitForStart[i].gameObject.SetActive(true);
        }
    }
    

    #endregion

    #region Animation

    public void OpenAnimScore()
    {

        Anim_Score.SetBool("Open",true);

    }
    public void EndAnimScore()
    {
        Anim_Score.SetBool("Open", false);
    }


    #endregion


    #region Method Priate

    private void RenderScore()
    {
        TextPlayer.text = ScorePlayer.ToString();
        TextCPU.text = ScoreAI.ToString();

    }

 


    #endregion

    public Player GetPlayer()
    {
        return (Player)Player;
    }
    public AI GetCPU()
    {
        return (AI)AI;
    }
    public Ball GetBall()
    {
        return (Ball)Ball;
    }
}
