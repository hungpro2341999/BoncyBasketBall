﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum TypeScreen { Home,Shop,Tourment,Mission,Vs,Loading,Pause,ResultWindown,SpinWindown,Gift,Play,ShowReward,WinTourment,FreeReward}
public class GameMananger : MonoBehaviour
{
  
    public List<Screens> Screens;
    public static GameMananger Ins;

    public bool isGamePause = true; 
    public bool isGameOver = true;

    public delegate void eventStartGame();
    public event eventStartGame event_Start_Game;
    [Header("UI")]
    public Transform DemoCharacter;
    public Transform TransSetting;
    public Transform TrasUIGenrate;
    public Transform TrasUI;
    public Animator Anim_Setting;
    public bool open = false;
    public bool isTest = false;

    public Status StatusPreb; 
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
    }
    // Start is called before the first frame update
    void Start()
    {
        if (isTest)
        {
           OpenScreen(TypeScreen.Loading);
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            ShowFullAds();
        }
    }

    public void StartGame()
    {
        isGamePause = false;
        isGamePause = false;
    }

    public void OverGame()
    {
        isGamePause = true;
        isGamePause = true;
    }


    public void OpenScreen(TypeScreen type)
    {
        foreach(Screens s in Screens)
        {
            if(s.typeSceen == type)
            {
                s.Open();
            }
            else
            {
                s.Close();
            }
        }
    }

   

    public void OpenScreen(Screens screen)
    {
        foreach(Screens s in Screens)
        {
            if(s.typeSceen == screen.typeSceen)
            {
                s.Open();
                
            }
            else
            {
                s.Close();
            }
        }
    }

    public void CloseScreen(Screens s)
    {

    }

    public void OpenSingle(TypeScreen type)
    {
        foreach(Screens s in Screens)
        {
            if(s.typeSceen == type)
            {
                s.Open();
                break;
            }
        }
    }

    public void OpenSingle(Screens screen)
    {
        screen.Open();
    }
    public void CloseSingle(Screens screen)
    {
        screen.Close();
    }

    public void CloseSingle(TypeScreen type)
    {
        foreach (Screens s in Screens)
        {
            if (s.typeSceen == type)
            {
                s.Close();
                break;
            }
        }
    }

    public void ChangeSetting()
    {
        open = !open;
        if (open)
        {
            Anim_Setting.SetBool("Open", true);
        }
        else
        {
            Anim_Setting.SetBool("Open",false);
        }
    }

    public void CloseSetting()
    {
        open = false;
        Anim_Setting.SetBool("Open", open);
    }
    public void ShowFullAds()
    {
        if (!CtrlDataGame.Ins.IsActiveAds())
        {
            Debug.Log("Show");
            ManagerAds.Ins.ShowInterstitial();
        }
        else
        {
            Debug.Log("Khong Show Ads");
        }
    }
    

    public void Rate()
    {
        ManagerAds.Ins.RateApp();
    }

    public void RewardVideo(System.Action action)
    {
        if (ManagerAds.Ins.CanShowAds())
        {

            ManagerAds.Ins.ShowRewardedVideo((done) =>
            {
                action();
            });


        }
    }

    public void ShowStatus(string text)
    {
        var a = Instantiate(StatusPreb, TrasUI);
        a.Staus.text = text;
    }


    

}
