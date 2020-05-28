using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceenLoading : Screen
{
    public float Time_Loading;
    public Image Img_Loading;
    
    private float time=0;
    public float speed;

    public override void OnEnableScreen()
    {

        time = 0;
        Img_Loading.fillAmount = 0;

    }
    public override void EventOpen()
    {
        GameMananger.Ins.isGamePause = true;
        GameMananger.Ins.isGameOver = true;
        GameMananger.Ins.TrasUIGenrate.gameObject.SetActive(false);
        GameMananger.Ins.TransSetting.gameObject.SetActive(false);
        GameMananger.Ins.DemoCharacter.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (time >= Time_Loading)
        {
            GameMananger.Ins.OpenScreen(TypeScreen.Home);
           
        }
        else
        {
            time += Time.deltaTime*speed;
            float per = (time / Time_Loading);
            Img_Loading.fillAmount = per; 

        }
    }


}
