using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BoardGame { Player,CPU }
public class CheckInBall : MonoBehaviour
{
   
    public BoardGame BoardOf;
    // Start is called before the first frame update
    public Dictionary<string, int> Global = new Dictionary<string, int>();

    public const string Key_Baset_1 = "Basket_1";
    public const string Key_Baset_2 = "Basket_2";
    public const string Key_Global = "Global";
    public const string Key_Coll_2 = "Coll_2";
    public bool isCanGlobal = false;
    public bool isGlobal = false;
    public Transform Hool;
    public Transform TargetHool;

    private void Update()
    {
        if(BoardOf == BoardGame.Player)
        {
            Vector2 rotation = TargetHool.position - Hool.position;
            float angle = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
            Hool.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        }
        else
        {
            Vector2 rotation =   Hool.position- TargetHool.position;
            float angle = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
            Hool.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }


    }
    private void Awake()
    {
        Init();
    }
    private void Start()
    {
       
    }

    public void Init()
    {
        Global.Add(Key_Baset_1, 0);
        Global.Add(Key_Baset_2, 0);
        Global.Add(Key_Global, 0);
        Global.Add(Key_Coll_2, 0);

    }

    public void RestorCheckBoard()
    {
        isGlobal = false;
        isCanGlobal = true;
        Global[Key_Global] = 0;
        Global[Key_Baset_1] = 0;
        Global[Key_Baset_2] = 0;
        Global[Key_Coll_2] = 0;
    }

    public void SetKey(string key)
    {
        if (isGlobal)

            return;

        Global[key] = 1;

        if (Global[Key_Global] != 0)
        {
            if (isCanGlobal)
            {
                if(Global[Key_Baset_2]==1 && Global[Key_Baset_1] == 0)
                {
                  
                    isCanGlobal = false;
                    return;
                }
                else
                {
                    Global[key] = 1;
                }

                if ((Global[Key_Baset_1] + Global[Key_Baset_2]) == 2)
                {
                    

                    if (!CtrlGamePlay.Ins.isPlaying)
                        return;

                 
                    CtrlGamePlay.Ins.ResetRound();
                    var ball = (Ball)CtrlGamePlay.Ins.Ball;
                    var player = (Player)CtrlGamePlay.Ins.Player;
                    var cpu = (AI)CtrlGamePlay.Ins.AI;
                    AudioCtrl.Ins.Play("Goal");
                    switch (BoardOf)
                    {
                        case BoardGame.Player:
                            int score = 0;
                            if (ball.LastHand is AI)
                            {
                                if(cpu.type == TypeScore.Point_2)
                                {
                                    if (player.isInAction())
                                    {
                                        cpu.type = TypeScore.JumpBall;
                                        score = 2;
                                        CtrlGamePlay.Ins.GlobalPlayer(score);
                                        AudioCtrl.Ins.Play("SlampDunk");
                                    }
                                    else
                                    {
                                        AudioCtrl.Ins.Play("2Point");
                                        score = 2;
                                    }
                                }
                                else
                                {
                                    if(Global[Key_Coll_2] == 0)
                                    {
                                        cpu.type = TypeScore.Clean_Shoot;
                                        score = 3;
                                        CtrlGamePlay.Ins.GlobalPlayer(score);
                                    }
                                    else
                                    {
                                       
                                        score = 3;
                                    }

                                    AudioCtrl.Ins.Play("3Point");
                                }
                                CtrlGamePlay.Ins.SetScore(cpu.type);

                            }
                            else
                            {
                                if (player.type == TypeScore.Point_2)
                                {
                                    if (player.isInAction())
                                    {
                                        player.type = TypeScore.JumpBall;
                                        score = 2;
                                        CtrlGamePlay.Ins.GlobalPlayer(score);
                                        AudioCtrl.Ins.Play("SlampDunk");
                                    }
                                    else
                                    {
                                        AudioCtrl.Ins.Play("2Point");
                                        
                                        score = 2;
                                    }
                                }
                                else
                                {
                                    if (Global[Key_Coll_2] == 0)
                                    {
                                        player.type = TypeScore.Clean_Shoot;
                                        score = 3;
                                        CtrlGamePlay.Ins.GlobalPlayer(score);
                                    }
                                    else
                                    {
                                       
                                        score = 3;
                                    }
                                    AudioCtrl.Ins.Play("3Point");
                                }
                                CtrlGamePlay.Ins.SetScore(player.type);

                            }

                            Debug.Log("Score : " + score);
                          
                               
                            break;
                        case BoardGame.CPU:
                            int score1 = 0;
                            if (ball.LastHand is Player)
                            {
                                if (player.type == TypeScore.Point_2)
                                {
                                    if (player.isInAction())
                                    {
                                        AudioCtrl.Ins.Play("SlampDunk");
                                        player.type = TypeScore.JumpBall;
                                        score1 = 2;

                                    }
                                    else
                                    {
                                        AudioCtrl.Ins.Play("2Point");
                                        score1 = 2;
                                    }
                                }
                                else
                                {
                                    if (Global[Key_Coll_2] == 0)
                                    {
                                        player.type = TypeScore.Clean_Shoot;
                                        score1 = 3;
                                        AudioCtrl.Ins.Play("3Point");

                                    }
                                    else
                                    {
                                        AudioCtrl.Ins.Play("3Point");
                                        score1 = 3;
                                    }
                                }
                                CtrlGamePlay.Ins.GlobalCPU(score1);
                                CtrlGamePlay.Ins.SetScore(player.type);

                            }
                            else
                            {
                                if (cpu.type == TypeScore.Point_2)
                                {
                                    if (cpu.isInAction())
                                    {
                                        cpu.type = TypeScore.JumpBall;
                                        score1 = 2;
                                        CtrlGamePlay.Ins.GlobalCPU(score1);
                                        AudioCtrl.Ins.Play("SlampDunk");
                                    }
                                    else
                                    {
                                        AudioCtrl.Ins.Play("2Point");
                                        score1 = 2;
                                    }
                                }
                                else
                                {
                                    AudioCtrl.Ins.Play("3Point");
                                    if (Global[Key_Coll_2] == 0)
                                    {
                                        cpu.type = TypeScore.Clean_Shoot;
                                        score1 = 3;

                                    }
                                    else
                                    {
                                        score1 = 3;
                                    }
                                }
                                CtrlGamePlay.Ins.GlobalCPU(score1);
                                CtrlGamePlay.Ins.SetScore(cpu.type);

                            }



                          
                        
                            break;
                    }

                    isGlobal = true;
                }

               
            }
            
           
        }

        

    }

    


    public void RestoreKey(string key)
    {
        if(key == "Global")
        {

            isCanGlobal = true;
            Global[Key_Global] = 0;
            Global[Key_Baset_1] = 0;
            Global[Key_Baset_2] = 0;
            Global[Key_Coll_2] = 0;
        }

    }
}
