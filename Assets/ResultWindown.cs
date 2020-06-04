using Spine.Unity.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultWindown : Screen
{

    public Image[] VisibleVictory;
    public MixAndMatchGraphic P1;
    public MixAndMatchGraphic P2;
    public Text t_Match;
    public Text t_Reward;
    public Image Win;
    public Image Lose;
    public int reward;
    public Button X2_Coins;

    public override void EventOpen()
    {
        
        X2_Coins.interactable = true;
        GameMananger.Ins.isGameOver = true;
      
        t_Match.text = CtrlGamePlay.Ins.ScoreAI + "-" + CtrlGamePlay.Ins.ScorePlayer;
        if(CtrlGamePlay.Ins.ScoreAI > CtrlGamePlay.Ins.ScorePlayer)
        {
            AudioCtrl.Ins.Play("win");
            VisibleVictory[0].enabled = true;
            VisibleVictory[1].enabled = false;
            Win.enabled = true;
            Lose.enabled = false;

            if (VsScreen.isMatchRandom)
            {
                reward = 100;
                t_Reward.text = "100";
                CtrlDataGame.Ins.AddCoin(100);

                if(CtrlGamePlay.Ins.ScoreAI == 0)
                {
                    MissonCtrl.Ins.UpdateMission(3);
                }
                
            }
            else
            {
                AudioCtrl.Ins.Play("win");
                if (TourmentCtrl.Ins.isFinalMatchTour())
                {
                    reward = 500;
                    MissonCtrl.Ins.UpdateMission(2);
                    t_Reward.text = "500";
                    CtrlDataGame.Ins.AddCoin(500);
                  
                }
                else
                {
                  
                    reward = 0;
                    t_Reward.text = "0";
                }


               
               
                TourmentCtrl.Ins.LoadTour();
            }
        }
        else
        {
            AudioCtrl.Ins.Play("lose");
            MissonCtrl.Ins.UpdateMission(7);

            reward = 0;
                t_Reward.text = "0";
            VisibleVictory[0].enabled = false;
            VisibleVictory[1].enabled = true;
            Win.enabled = false;
            Lose.enabled = true;

            //t_Reward.text = "500";
            //CtrlDataGame.Ins.AddCoin(500);
            //TourmentCtrl.Ins.SetMatchPlayer();
            //TourmentCtrl.Ins.CompleteMatch();

        }

        if (VsScreen.isMatchRandom)
        {
            MissonCtrl.Ins.UpdateMission(0);
        }
        else
        {
            MissonCtrl.Ins.UpdateMission(1);
        }
        

        int[] Player = new int[] { CtrlDataGame.Ins.GetIdHead(), CtrlDataGame.Ins.GetIdHand(), CtrlDataGame.Ins.GetIdHand(), CtrlDataGame.Ins.GetIdLeg(), CtrlDataGame.Ins.GetIdItemLeg() };
        Load(Player, VsScreen.SkinUse);
    }

    public void Load(int[] P1,int[] P2)
    {

        this.P1.ApplySkin(P1);
        this.P2.ApplySkin(P2);
    }

    public void NextMatch()
    {
        if (!VsScreen.isMatchRandom)
        {
          
            GameMananger.Ins.OpenScreen(TypeScreen.Tourment);
           
          
        }
        else
        {
            CtrlGamePlay.Ins.SelectAI();
            GameMananger.Ins.OpenScreen(TypeScreen.Vs);
            VsScreen.isMatchRandom = true;
        }
      
        
    }
    private void OnEnable()
    {
        MissonCtrl.Ins.UpdateMission(0);
     
    }

    public void X2_Coins_Ads()
    {
        GameMananger.Ins.RewardVideo(X2_Coin);
    }
    public void X2_Coin()
    {
        CtrlDataGame.Ins.AddCoin(reward);
        X2_Coins.interactable = false;
    }

}
