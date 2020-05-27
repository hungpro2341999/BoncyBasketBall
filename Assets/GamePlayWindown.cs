using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayWindown : Screen
{
    // Start is called before the first frame update


    public override void EventOpen()
    {
        GameMananger.Ins.TrasUIGenrate.gameObject.SetActive(false);
        GameMananger.Ins.TransSetting.gameObject.SetActive(false);
        GameMananger.Ins.DemoCharacter.gameObject.SetActive(false);
        CtrlDataGame.Ins.ApplyPlayer();
    }

   
}
