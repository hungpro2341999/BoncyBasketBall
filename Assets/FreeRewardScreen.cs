using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeRewardScreen : Screens 
{
    public override void EventOpen()
    {
        CtrlDataGame.Ins.AddCoin(500);
    }
    public void GetReward()
    {
        
        GameMananger.Ins.CloseSingle(TypeScreen.FreeReward);
    }
}
