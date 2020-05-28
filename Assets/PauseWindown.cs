using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseWindown : Screen
{
    public List<Button> Sound;
    public List<Button> Music;
    // Start is called before the first frame update
    void Start()
    {
        ApplyMusic();
        ApplySound();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void EventOpen()
    {
        ApplyMusic();
        ApplySound();
        GameMananger.Ins.isGamePause = true;
       

    }

    public void ResumeGame() 
    {
        GameMananger.Ins.isGamePause = false;
        GameMananger.Ins.CloseSingle(this);
       
    }

    public override void EventClose()
    {
        Time.timeScale = 1;
    }


    public void ChangeMusic()
    {
        if (SettingCtrl.Ins.isMusic())
        {
            SettingCtrl.Ins.SetMusic(0);
        }
        else
        {
            SettingCtrl.Ins.SetMusic(1);
        }
        ApplyMusic();
        SettingCtrl.Ins.ApplySetting();
    }

    public void ChangeSound()
    {
        if (SettingCtrl.Ins.isSound())
        {
            SettingCtrl.Ins.SetSound(0);
        }
        else
        {
            SettingCtrl.Ins.SetSound(1);
        }
        ApplySound();
        SettingCtrl.Ins.ApplySetting();
    }

    public void ApplySound()
    {
        if (SettingCtrl.Ins.isSound())
        {
            Sound[0].gameObject.SetActive(true);
            Sound[1].gameObject.SetActive(false);
        }
        else
        {
            Sound[0].gameObject.SetActive(false);
            Sound[1].gameObject.SetActive(true);
        }


    }
    public void ApplyMusic()
    {
        if (SettingCtrl.Ins.isMusic())
        {
            Music[0].gameObject.SetActive(true);
            Music[1].gameObject.SetActive(false);
        }
        else
        {
            Music[0].gameObject.SetActive(false);
            Music[1].gameObject.SetActive(true);
        }

    }
}
