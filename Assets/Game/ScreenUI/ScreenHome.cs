using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenHome : Screen
{
    public override void EventOpen()
    {
        GameMananger.Ins.TransSetting.gameObject.SetActive(true);
        GameMananger.Ins.DemoCharacter.gameObject.SetActive(true);
        GameMananger.Ins.TrasUIGenrate.gameObject.SetActive(true);
    }

    public void Play()
    {
        VsScreen.isMatchRandom = true;
        GameMananger.Ins.OpenScreen(TypeScreen.Vs);
    }
}
