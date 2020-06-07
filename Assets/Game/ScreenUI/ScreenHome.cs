using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenHome : Screens
{
    public const string Key_Get_Reward_Day = "Key_reward_Day";
    public Transform RewardDay;
    public override void EventOpen()
    {
      //  PlayerPrefs.DeleteKey(Key_Get_Reward_Day);
        if (!PlayerPrefs.HasKey(Key_Get_Reward_Day))
        {
            
            TimeSave timeSave = new TimeSave(System.DateTime.Today.Day, System.DateTime.Today.Month, System.DateTime.Today.Year);
            string json = JsonUtility.ToJson(timeSave);
            PlayerPrefs.SetString(Key_Get_Reward_Day, json);
            PlayerPrefs.Save();
        }
        if (isOpenReward())
        {
            RewardDay.gameObject.SetActive(true);
        }
        else
        {
            RewardDay.gameObject.SetActive(false);
        }

        

        GameMananger.Ins.TransSetting.gameObject.SetActive(true);
        GameMananger.Ins.DemoCharacter.gameObject.SetActive(true);
        GameMananger.Ins.TrasUIGenrate.gameObject.SetActive(true);
    }

    public void Play()
    {
        CtrlGamePlay.Ins.SelectAI();
        VsScreen.isMatchRandom = true;
        GameMananger.Ins.OpenScreen(TypeScreen.Vs);
    }

    public void SaveNextDay()
    {
        TimeSave timeSave = new TimeSave(System.DateTime.Today.Day+1, System.DateTime.Today.Month, System.DateTime.Today.Year);
        string json = JsonUtility.ToJson(timeSave);
        PlayerPrefs.SetString(Key_Get_Reward_Day, json);
        PlayerPrefs.Save();

        
    }

    public bool isOpenReward()
    {
        TimeSave timeSave = new TimeSave(System.DateTime.Today.Day, System.DateTime.Today.Month, System.DateTime.Today.Year);
        if (TotalTime(timeSave) >= TotalTime(GetTimeSave()))
        {
            return true;
        }
        else
        {
            return false;
        }
    
    }
    public  int TotalTime(TimeSave time)
    {
        return time.Years * 365 * 30 + time.Month * 30 + time.day;
    }


    public TimeSave GetTimeSave()
    {
        return JsonUtility.FromJson<TimeSave>(PlayerPrefs.GetString(Key_Get_Reward_Day));
    }

   
   public void GetRewardDay()
    {
        GameMananger.Ins.OpenSingle(TypeScreen.FreeReward);
        RewardDay.gameObject.SetActive(false);
        SaveNextDay();
    }
   
}

[System.Serializable]
public class TimeSave 
{
    public int day;
    public int Month;
    public int Years;
    
   public TimeSave(int day,int Month,int Year)
    {
        this.day = day;
        this.Month = Month;
        this.Years = Year;
    }

}

