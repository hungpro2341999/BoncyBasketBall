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
    public const string Key_Reset = "Reset";
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
                            TypeScore type1 = cpu.GetTypeScore();
                            if (ball.LastHand is AI)
                            {
                                if(type1 == TypeScore.Point_2)
                                {
                                    if(!cpu.isNullEffAction())
                                    {
                                        type1 = TypeScore.JumpBall;
                                        score = 2;
                                       

                                    }
                                    else
                                    {
                                        type1 = TypeScore.Point_2;
                                        score = 2;
                                    }

                                 
                                }
                                else
                                {
                                    if (!cpu.isNullEffAction())
                                    {
                                        type1 = TypeScore.JumpBall;
                                        score = 2;
                                    }
                                    else
                                    {
                                        if (Global[Key_Coll_2] == 0)
                                        {
                                            type1 = TypeScore.Clean_Shoot;
                                            score = 3;

                                        }
                                        else
                                        {
                                            type1 = TypeScore.Point_3;
                                            score = 3;
                                        }
                                    }
                                       

                                   
                                }
                                CtrlGamePlay.Ins.SetScore(type1);

                            }
                            else
                            {
                                score = 2;
                                CtrlGamePlay.Ins.SetScore(TypeScore.Point_2);

                            }

                            CtrlGamePlay.Ins.GlobalCPU(score);


                            break;
                        case BoardGame.CPU:
                            int score1 = 0;
                            if (ball.LastHand is Player)
                            {
                                TypeScore type = player.GetTypeScore();
                                if (type == TypeScore.Point_2)
                                {
                                    if (!player.isInAction())
                                    {

                                        type = TypeScore.JumpBall;


                                    }
                                    else
                                    {
                                        type = TypeScore.Point_2;
                                    }
                                  
                                    score1 = 2;
                                }
                                else
                                {
                                    if (!player.isInAction())
                                    {

                                        type = TypeScore.JumpBall;
                                        score1 = 2;

                                    }
                                    else
                                    {
                                        if (Global[Key_Coll_2] == 0)
                                        {
                                            type = TypeScore.Clean_Shoot;

                                            score1 = 3;

                                        }
                                        else
                                        {
                                            type = TypeScore.Point_3;
                                            score1 = 3;
                                        }

                                    }


                                }
                            
                                CtrlGamePlay.Ins.SetScore(type);

                            }
                            else
                            {
                                score1 = 2;
                               
                                CtrlGamePlay.Ins.SetScore(TypeScore.Point_2);

                            }

                            CtrlGamePlay.Ins.GlobalPlayer(score1);



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
        }else if(key == "Reset")
        {
            isCanGlobal = true;
           
            Global[Key_Baset_1] = 0;
            Global[Key_Baset_2] = 0;
            Global[Key_Coll_2] = 0;
        }

    }
}
