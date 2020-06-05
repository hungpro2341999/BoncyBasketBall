using Spine.Unity.Examples;
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
    public static int CountPlay;
    public static int CountPlayQuickMatch = 0; 

    public static CtrlGamePlay Ins;

    [Header("StatusGame")]
    public bool isPlaying = false;
    public bool WaitForEndMatch = false;

    public Character Player;
    public Character AI;
    public Character Ball;
    public Character PerviousAI;
    public float WidthScreen;
    public float HeightScreen;
    public float speedBackScreen;


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
    public AI[] AI_CPU;
    
    
    


    [Header("Render")]

    public Text TextTime;
    public Text TextPlayer;
    public Text TextCPU;
    public Text TextStatus;
   


    [Header("TimeGame")]

    public float timeResetGamePlay;
    public float timeGamePlay;
    public float timeWaitForPerSecond;
    public float timeWaiForEndMatch;
    public float timeWattingMatch;
    public float timeOut;
    


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
    public static bool firstPlay = false;
    public  bool isWattingStart = false;

    [Header("StatusGame")]

    public string[] StatusForWatting;
    public string[] TimeOut;
    
  
    
    

    #endregion


    #region Private Variable

    private float m_timeGame;
    public int delayTime;
    public float timePopPupText;
    private int delay_Time;


    #endregion
    [Header("Transform")]
    public Image BlackScreen;
    public Transform TransGamePlay;
    public Transform TransScore;
    public Transform RightBox;
    public Transform LeftBox;
    public Transform UpBox;


   
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

       
        


        // Time.timeScale = 0.4f;


    }

    public void FirstCommit()
    {
        WidthScreen = (Camera.main.ScreenToWorldPoint(new Vector3(UnityEngine.Screen.width, UnityEngine.Screen.height, 0)).x) * 2;
        HeightScreen = (Camera.main.ScreenToWorldPoint(new Vector3(UnityEngine.Screen.width, UnityEngine.Screen.height, 0)).y) * 2;
        //  WidthScreen = Camera.main.ScreenToWorldPoint()

        //  WidthScreen = (float)Camera.main.pixelWidth/100;
        PosInitPlayer = GameObject.FindGameObjectWithTag("PosInitPlayer").transform.position;
        PosInitCPU = GameObject.FindGameObjectWithTag("PosInitCPU").transform.position;
        PosInitBall = GameObject.FindGameObjectWithTag("PosInitBall").transform.position;
        Application.targetFrameRate = 120;
        Physics2D.gravity = Vector3.up * graviry;


        /// Resize Screen

        var a = (Player)Player;
        a.isInputMove = true;
        //    eventResetGame();
        Vector2 pos = Board[0].transform.position;
        pos.x = WidthScreen / 2;
        Board[0].transform.position = pos;

        Vector2 pos1 = Board[1].transform.position;
        pos1.x = -WidthScreen / 2;
        Board[1].transform.position = pos1;

        Vector2 pos2 = RightBox.transform.position;

        pos2.x = WidthScreen / 2;
        RightBox.transform.position = pos2;

        Vector2 pos3 = LeftBox.transform.position;

        pos3.x = -WidthScreen / 2;
        LeftBox.transform.position = pos3;


    }



    // Update is called once per frame
    void Update()
    {
        Application.targetFrameRate = 120;

        if (timePopPupText > 0)
        {
            PopPupStatus();
            timePopPupText -= Time.deltaTime;
            if (timePopPupText < 0)
            {
                EndPopPup();
            }
        }


        if (Input.GetKeyDown(KeyCode.W))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(Rest_Game_Play());
        }


        if (GameMananger.Ins.isGameOver || GameMananger.Ins.isGamePause)

            return;

        if (isWattingStart == true)
        {
            if (timeWattingMatch >= 0)
            {
             
                timeWattingMatch -= Time.deltaTime;
                return;
            }
            else
            {

                ActiveRigidBody(true);
                isWattingStart = false;
                isPlaying = true;
                m_timeGame = timeGamePlay;
            }
            return;
        }
          

      



        if (isPlaying)
        {
            if (m_timeGame >= 0)
            {
                TextTime.text = ((int)m_timeGame).ToString();
                m_timeGame -= Time.deltaTime;

            }
            else
            {
                WaitForEndMatch = true;
                timeWaiForEndMatch = 2;
                ActivePopPupText(1, 20);
                AudioCtrl.Ins.Play("OverTime");
                isPlaying = false;

            }

        }
        if (WaitForEndMatch)
        {
            
            if (timeWaiForEndMatch <= 0)
            {
                if(ScoreAI == ScorePlayer)
                {
                    ActivePopPupText(timeOut, 50);

                    TextStatus.text = TimeOut[Random.Range(0, TimeOut.Length)];

                    m_timeGame = timeOut;
                    ResetRound();

                    WaitForEndMatch = false;
                }
                else
                {
                   
                    GameMananger.Ins.OpenSingle(TypeScreen.ResultWindown);
                    WaitForEndMatch = false;
                }

               
            }
            else
            {
                TextStatus.text = "OUT OFF TIME !!! ";
                timeWaiForEndMatch -= Time.deltaTime;
              
            }

        }
        

    }
   

  
    public void StartWatting()
    {
        ScorePlayer = 0;
        ScoreAI = 0;
        TextTime.text = timeGamePlay.ToString();

        RenderScore();
        StartCoroutine(IE_WaitForGameStart());
        Player.transform.position = PosInitPlayer;
        AI.transform.position = PosInitCPU;
        Ball.transform.position = PosInitBall;
        eventRestGamePlay();
        ActiveRigidBody(false);
        WaitForEndMatch = false;
        
      
    }

    public void CpuThrowBall()
    {
        AudioCtrl.Ins.Play("Shoot");
        var CPU = (AI)AI;

        CPU.type = CPU.GetTypeScore();
        int currPos = (int)Mathf.Abs((CtrlGamePlay.Ins.WidthScreen / 2 - CPU.transform.position.x) / CPU.Amount);
        float PercentageX =  CPU.TargetHoop.x - Random.Range(0,CPU.PercentageThrowBall)*CPU.PercentageDistance *Mathf.Clamp((currPos), 1, 12) * 0.05f;
        float PercentageY = CPU.TargetHoop.y + Random.Range(0, CPU.PercentageThrowBall) * CPU.PercentageDistance * Mathf.Clamp((currPos), 1, 12) * 0.05f;
        float yJump = Mathf.Abs(CPU.TargetHoop.y - Ball.transform.position.y);
        
        yJump += Random.Range(0.5f, 1f);
        if (yJump < PercentageY)
        {
            yJump = PercentageY + 0.5f;
        }

        Debug.Log(yJump + " High Real");
        if(AI.name.StartsWith("CPU Final"))
        {
            if (yJump >= 2)
            {
                yJump = 1.6f;
            }
            Debug.Log(yJump + " High");
           
        }
        Launch(yJump, new Vector3(PercentageX, PercentageY, 0));
    }
    public void PlayerThrowBall()
    {
        AudioCtrl.Ins.Play("Shoot");
        var player = (Player)Player;
        player.type = player.GetTypeScore();
        int currPos = (int)Mathf.Abs((CtrlGamePlay.Ins.WidthScreen / 2 - player.transform.position.x) / player.Amount);
        float PercentageX = player.TargetHoop.x + Random.Range(0, player.PercentageThrowBall) * Mathf.Clamp((currPos), 1, 12) * 0.3f;
        float PercentageY = player.TargetHoop.y + ((Random.Range(0, player.PercentageThrowBall) * player.PercentageDistance * Mathf.Clamp((currPos), 1, 12)* 0.05f));
        float yJump = Mathf.Abs(player.TargetHoop.y - Ball.transform.position.y);
        yJump += Random.Range(0.5f, 1);
       
        Launch(yJump, new Vector3(PercentageX, PercentageY, 0));
    }
    public void Launch(float height,Vector3 Target)
    {
       
        var a = (Player)Player;
        var b = (AI)AI;
       
        a.isBall = false;
        b.isBall = false;
        Ball.transform.transform.parent = TransGamePlay;
        Ball.GetComponent<CircleCollider2D>().isTrigger = false;
        Ball.Body.isKinematic = false;
        Ball.Body.simulated = true;
       // Debug.Log("Velocity : "+CaculateVelocity(height, Target).InitVelocity);

      
        Vector3 vec = CaculateVelocity(height, Target).InitVelocity;
        Debug.Log(vec.ToString());
        Ball.Body.velocity = vec;
     
       
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
    public void GlobalCPU(int Score)
    {
        ScorePlayer += Score;
        RenderScore();

       
    }



    public void GlobalPlayer(int Score)
    {
        ScoreAI+=Score;
       
        RenderScore();


      
    }

    public void ResetRound()
    {
        StartCoroutine(Rest_Game_Play());
    }


    IEnumerator Rest_Game_Play()
    {

        CountPlay++;
        isPlaying = false;
        yield return new WaitForSeconds(2);
        var color = BlackScreen.color;
        Debug.Log("Color : " + color.a);
        while(color.a <= 1)
        {
            color.a += Time.deltaTime * speedBackScreen;
           // Debug.Log("Color Black : " + color.a);
            BlackScreen.color = color;
            yield return new WaitForSeconds(0);
        }
        eventRestGamePlay();
        while (color.a >= 0)
        {
            color.a -= Time.deltaTime * speedBackScreen;

            BlackScreen.color = color;
            yield return new WaitForSeconds(0);
        }
        isPlaying = true;







    }
  

    public void RestGame()
    {
        for (int i = 0; i < Board.Length; i++)
        {
            Board[i].RestorCheckBoard();
        }
        ScoreAI = 0;
        ScorePlayer = 0;
        RenderScore();
        isPlaying = true;
        m_timeGame = timeGamePlay;
        Player.transform.position = PosInitPlayer;
        AI.transform.position = PosInitCPU;
        Ball.transform.position = PosInitBall;
       
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
      
    }


    

    public void SetScore(TypeScore score)
    {
        var a = Instantiate(objScore, TransScore);
        switch (score)
        {
            case TypeScore.Point_2:
                AudioCtrl.Ins.Play("2Point");
                a.ChangeImg(Scoure_Img_Score[0]);
                break;
            case TypeScore.Point_3:
                AudioCtrl.Ins.Play("3Point");
                a.ChangeImg(Scoure_Img_Score[1]);
                break;
            case TypeScore.Clean_Shoot:
                AudioCtrl.Ins.Play("3Point");
                a.ChangeImg(Scoure_Img_Score[2]);
                break;
            case TypeScore.JumpBall:
                AudioCtrl.Ins.Play("SlampDunk");
                a.ChangeImg(Scoure_Img_Score[3]);
                break;
        }
        
    }

    IEnumerator IE_WaitForGameStart()
    {
        //for (int i = 0; i < WaitForStart.Length; i++)
        //{
        //    WaitForStart[i].gameObject.SetActive(false);
        //}
        //yield return new WaitForSeconds(0);

        //for (int i = 0; i < WaitForStart.Length; i++)
        //{

        //    yield return new WaitForSeconds(timeWaitForPerSecond);
        //    WaitForStart[i].gameObject.SetActive(true);
        //}
        TextStatus.text = "";
        yield return new WaitForSeconds(2);
        AudioCtrl.Ins.Play("Ready");
        TextStatus.text = StatusForWatting[0];
        ActivePopPupText(2,25);
        yield return new WaitForSeconds(2);
        TextStatus.text = "";
        yield return new WaitForSeconds(1);
        ActivePopPupText(1,40);
        AudioCtrl.Ins.Play("Voice_Jump");
        TextStatus.text = StatusForWatting[1];
        yield return new WaitForSeconds(2);
        TextStatus.text = "";




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

    public void ActiveRigidBody(bool active) 
    {
        Player.Body.simulated = active;
        AI.Body.simulated = active;
        Ball.Body.simulated = active;
    }

    public void PopPupStatus()
    {
        delayTime++;
        if (delayTime % delay_Time == 0)
        {

            bool active = TextStatus.gameObject.activeSelf;
            TextStatus.gameObject.SetActive(!active);
        }
      
       
    }

    public void EndPopPup()
    {
        TextStatus.gameObject.SetActive(true);
    }
    public void ActivePopPupText(float time,int delay_Time)
    {
        timePopPupText = time;
        this.delay_Time = delay_Time;
    }

    public void SelectAI()
    {
        Ball.transform.parent = TransGamePlay;
        if (AI != null)
        {
            
            AI = (AI)AI;
            AI.Destroy();
        }
     
        int r = Random.Range(0, AI_CPU.Length);
        var a =  Instantiate(AI_CPU[r], TransGamePlay);
        AI = (Character)a;
        AI.gameObject.SetActive(true);
        CtrlDataGame.Ins.SkinCPU = a.AnimationHandle.GetComponent<EquipsVisualsComponentExample>();
        
    
    }

    public void PlayAgain()
    {
        GameMananger.Ins.CloseSingle(TypeScreen.ResultWindown);
        CtrlGamePlay.firstPlay = false;
        GameMananger.Ins.isGameOver = false;
        GameMananger.Ins.isGamePause = false;
        CtrlGamePlay.Ins.isWattingStart = true;
        CtrlGamePlay.Ins.timeWattingMatch = 5;
        CtrlGamePlay.Ins.StartWatting();
        AudioCtrl.Ins.Play("eff");
      
    }
  
}
