using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//public enum typeMisson  {QuickMatch, JoinTournament, Top1Tounament,PlayerNoPoint,UnclockHead,UnclockShoe,UnclockHand,Lose,WatchVideo,}
public class MissonCtrl : MonoBehaviour
{
    public const string Key_Misson = "Key_Mission";
    public static MissonCtrl Ins;
 
    public List<ItemMission> ListItemMission;
    public Transform TranMisson;
  
    
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
    private void Start()
    {
        Load();
    }
    public void Load()
    {
        Init();

    }

    public void Init()
    {
     //   PlayerPrefs.DeleteKey(Key_Misson);
        if (!PlayerPrefs.HasKey(Key_Misson))
        {
            List<SaveMisson> L_Save = new List<SaveMisson>();
            for(int i = 0; i < ListItemMission.Count; i++)
            {

                SaveMisson sMis = new SaveMisson(i, 0, new int[3] ,0);
                L_Save.Add(sMis);


            }
            ListSaveMission L_Save_Missison = new ListSaveMission(L_Save);
            var json = JsonUtility.ToJson(L_Save_Missison);
            PlayerPrefs.SetString(Key_Misson, json);
            PlayerPrefs.Save();
            Debug.Log("Curr Misson : " + JsonUtility.FromJson<ListSaveMission>(PlayerPrefs.GetString(Key_Misson)).List.Count);

        }

        LoadMisson(GetMissonSave());



    }

    public List<SaveMisson> GetMissonSave()
    {

        return JsonUtility.FromJson<ListSaveMission>(PlayerPrefs.GetString(Key_Misson)).List;
    }    
    public void SaveMission()
    {
        List<SaveMisson> L_Save = new List<SaveMisson>();
        for (int i = 0; i < ListItemMission.Count; i++)
        {
            
            SaveMisson sMis = new SaveMisson(ListItemMission[i].id, ListItemMission[i].ProcessCurr, ListItemMission[i].LoopMisisonCurr, ListItemMission[i].Curr);
            L_Save.Add(sMis);
        }

        ListSaveMission L_Save_Missison = new ListSaveMission(L_Save);
        var json = JsonUtility.ToJson(L_Save_Missison);
        PlayerPrefs.SetString(Key_Misson, json);
        PlayerPrefs.Save();
        Debug.Log("Curr Misson : " + JsonUtility.FromJson<ListSaveMission>(PlayerPrefs.GetString(Key_Misson)).List.Count);
    }

    public void LoadMisson(List<SaveMisson> ListSaveMission)
    {
        for(int i = 0; i < ListSaveMission.Count; i++)
        {
            ListItemMission[i].LoadMisson(i, ListSaveMission[i].CurrMisson, ListSaveMission[i].ProcessCurr, ListSaveMission[i].LoopCurr);
            
        }
    }

    public void UpdateMission(int id)
    {
        ListItemMission[9].UpdateMission(true);
        foreach (var Misson in ListItemMission)
        {
           if(Misson.id == id)
            {
                Misson.UpdateMission(true);
                SaveMission();

                break;
            }

        }
       
    }



}


[System.Serializable]
public class ListSaveMission
{
    public List<SaveMisson> List = new List<SaveMisson>();
   public ListSaveMission(List<SaveMisson> List)
    {
        this.List = List;
    } 
}

[System.Serializable]
public class SaveMisson
{
    
    public int idMisson;
    public int CurrMisson;
  
    public int ProcessCurr;
    public int[] LoopCurr;
    public SaveMisson(int idMisson,int ProcessCurr,int[] LoopCurr,int CurrMisson)
    {
        this.idMisson = idMisson;
        this.CurrMisson = CurrMisson;
     
        this.ProcessCurr = ProcessCurr;
        this.LoopCurr = LoopCurr;
    }
     
}