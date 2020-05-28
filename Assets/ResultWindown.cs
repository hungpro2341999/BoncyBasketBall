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


    public override void EventOpen()
    {

        GameMananger.Ins.isGameOver = true;
        t_Match.text = CtrlGamePlay.Ins.ScoreAI + "-" + CtrlGamePlay.Ins.ScorePlayer;
        if(CtrlGamePlay.Ins.ScoreAI > CtrlGamePlay.Ins.ScorePlayer)
        {
            VisibleVictory[0].enabled = true;
            VisibleVictory[1].enabled = false;
            Win.enabled = true;
            Lose.enabled = false;

            if (VsScreen.isMatchRandom)
            {
                t_Reward.text = "100";
                CtrlDataGame.Ins.AddCoin(100);
                
            }
            else
            {
                
                t_Reward.text = "500";
                CtrlDataGame.Ins.AddCoin(500);
                TourmentCtrl.Ins.SetMatchPlayer();
                TourmentCtrl.Ins.CompleteMatch();

            }
        }
        else
        {
           
            t_Reward.text = "0";
            VisibleVictory[0].enabled = false;
            VisibleVictory[1].enabled = true;
            Win.enabled = false;
            Lose.enabled = true;

            t_Reward.text = "500";
            CtrlDataGame.Ins.AddCoin(500);
            TourmentCtrl.Ins.SetMatchPlayer();
            TourmentCtrl.Ins.CompleteMatch();

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
            GameMananger.Ins.OpenScreen(TypeScreen.Vs);
            VsScreen.isMatchRandom = true;
        }
      

    }


}
